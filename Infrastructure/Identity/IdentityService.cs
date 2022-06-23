using Application.Common.Interfaces;
using Application.Common.Models.Dtos;
using Domain.Entities.CustomeIdentity;
using Infrastructure.Identity.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfigurationSection _jwtSettings;
        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
            , SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = configuration.GetSection("JwtSettings");
        }
        public async Task<IdentityResult> AddUserToRoleAsync(string userId, List<UserRolesDto> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId);
            foreach (var role in roleNames)
            {
                var userInRole = await _userManager.IsInRoleAsync(user, role.RoleName);
                if (userInRole)
                {
                    return IdentityResult.Failed();
                }
                var result = await _userManager.AddToRoleAsync(user, role.RoleName);
            }
            return IdentityResult.Success;

        }

        public async Task<IdentityResult> CreaRoleAsync(string roleName)
        {
            if (roleName is null)
            {
                return IdentityResult.Failed();
            }
            var identityrole = new IdentityRole
            {
                Name = roleName
            };
            return await _roleManager.CreateAsync(identityrole);
        }
        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var roleWithId = await _roleManager.FindByIdAsync(roleId);
            if (roleWithId is null)
            {
                return IdentityResult.Failed();
            }
            return await _roleManager.DeleteAsync(roleWithId);
        }
        public IQueryable<IdentityRole> ListRoles()
        {
            return _roleManager.Roles.AsNoTracking();
        }

        public IQueryable<ApplicationUser> ListUsers()
        {
            return _userManager.Users.Where(a => a.IsActive == true).AsNoTracking();
        }
        public async Task<string> LoginAsync(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            var passvalid = _userManager.CheckPasswordAsync(user, login.Password);
            if (user is null)
            {
                return null;
            }
            if (passvalid.Result == false)
            {
                return null;
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            var signingCredentials = GetSigningCredentials();
            var claims = GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, await claims);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            if (result.Succeeded)
            {
                return token;
            }
            return null;
        }

        public async Task<IdentityResult> RemoveUserFromRolesAsync(string userId, List<UserRolesDto> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return IdentityResult.Failed();
            }
            var roles = roleNames.Select(a => a.RoleName);
            var deleteRoles = await _userManager.RemoveFromRolesAsync(user, roles);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto model)
        {

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return IdentityResult.Failed();
            }
            return await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
        }

        public async Task<IdentityResult> UpdateRoleAsync(EditRoleDto role)
        {
            var roleWithId = await _roleManager.FindByIdAsync(role.RoleId);
            if (roleWithId is null)
            {
                return IdentityResult.Failed();
            }
            roleWithId.Name = role.RoleName;
            return await _roleManager.UpdateAsync(roleWithId);
        }

        public async Task<IdentityResult> EditUserAsync(string email, EditUserDto model)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return IdentityResult.Failed();
            }
            user.Email = model.Email;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<bool> UserIsInRoleAsync(ApplicationUser user, string roleName)
        {
            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto userRegister)
        {
            var user = new ApplicationUser
            {
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                IsActive = true
            };
            return await _userManager.CreateAsync(user, userRegister.Password);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return IdentityResult.Failed();
            }
            user.IsActive = false;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<string> ForgetPasswordtokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return null;
            }
            var toke = await _userManager.GeneratePasswordResetTokenAsync(user);
            return toke;
        }

        public async Task<IEnumerable<Claim>> GetUserClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return null;
            }
            var claims = await _userManager.GetClaimsAsync(user);
            return claims;
        }

        public async Task<IdentityResult> ManageUserClaims(string userId, List<UserClaims> claim)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return IdentityResult.Failed();
            }
            var claims = await _userManager.GetClaimsAsync(user);
            foreach (var cm in claims)
            {
                foreach (var ca in claim)
                {
                    if (ca.ClaimType == cm.Type)
                    {
                        return IdentityResult.Failed();
                    }
                }
            }
            foreach (var ca in claim)
            {
                if (ca.ClaimType == "Create Role" || ca.ClaimType == "Edit Role" || ca.ClaimType == "Delete Role")
                {
                    var result = await _userManager.AddClaimsAsync(user, claim.Select(a => new Claim(a.ClaimType, a.IsSelected ? "true" : "false")));
                    return result;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteUserClaims(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return IdentityResult.Failed();
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);
            return result;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings.GetSection("validIssuer").Value,
            audience: _jwtSettings.GetSection("validAudience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
            signingCredentials: signingCredentials);
            return tokenOptions;
        }
        private async Task<List<Claim>> GetClaims(ApplicationUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims.ToList();

        }

    }
}
