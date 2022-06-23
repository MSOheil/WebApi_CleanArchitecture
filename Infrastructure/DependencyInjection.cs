using Application.Common.Interfaces;
using Domain.Entities.CustomeIdentity;
using Infrastructure.Identity;
using Infrastructure.Identity.Security;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DB
            services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("ConnectionToSql")));
            #endregion
            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            }
            )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            #endregion
            #region JWTBearer
            var jwtSettings = configuration.GetSection("JWTSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtbeareOptions =>
            {
                jwtbeareOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    ClockSkew = TimeSpan.FromMinutes(0)
                };
            }
           );
            #endregion
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            #region Add Policy Authentication
            services.AddAuthorization(sd =>
            sd.AddPolicy("RolePolicy",
                   policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()))
            );

            #endregion
            //services.AddAuthorization(policy =>
            //policy.AddPolicy("Role", polic =>
            // polic.RequireClaim("Create Role", "true").RequireRole("Admin")));
            return services;
        }
    }
}
