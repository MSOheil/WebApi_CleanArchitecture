using Application.Common.Models.Dtos;
using Domain.Entities.CustomeIdentity;
using Infrastructure.Identity.Claims;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(RegisterDto user);
        Task<string> LoginAsync(LoginDto login);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto model);
        IQueryable<ApplicationUser> ListUsers();
        Task<IdentityResult> EditUserAsync(string email, EditUserDto model);
        Task<IdentityResult> DeleteUserAsync(string userId);
        IQueryable<IdentityRole> ListRoles();
        Task<IdentityResult> CreaRoleAsync(string roleName);
        Task<IdentityResult> UpdateRoleAsync(EditRoleDto role);
        Task<IdentityResult> DeleteRoleAsync(string roleId);
        Task<IdentityResult> AddUserToRoleAsync(string userId,List<UserRolesDto> roleNames);
        Task<IdentityResult> RemoveUserFromRolesAsync(string userId, List<UserRolesDto> roleNames);
        Task<string> ForgetPasswordtokenAsync(string email);
        Task<IEnumerable<Claim>> GetUserClaims(string userId);
        Task<IdentityResult> ManageUserClaims(string userId, List<UserClaims> claim);
        Task<IdentityResult> DeleteUserClaims(string userId);
    }
}
