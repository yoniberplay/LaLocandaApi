using LaLocanda.Core.Application.DTOs.Account;
using LaLocanda.Core.Application.Interfaces.Services;
using LaLocanda.Core.Domain.Settings;
using LaLocanda.Infrastructure.Identity.Contexts;
using LaLocanda.Infrastructure.Identity.Entities;
using LaLocanda.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;

namespace LaLocanda.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultTokenProviders();

           

            services.Configure<JWTSettings>(config.GetSection("JWTSettings"));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = config["JWTSettings:Issuer"],
                    ValidAudience = config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"])),
                };

                //EVENTOS QUE PUEDEN SUCEDER
                opt.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse() { HasError = true, Error = "No tiene autorizacion." });
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JwtResponse() { HasError = true, Error = "No tiene autorizacion para acceder a esta area" });
                        return c.Response.WriteAsync(result);
                    }
                };
            });
            #endregion

            #region Database
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options =>
                       options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(
                        config.GetConnectionString("IdentityConnection"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)
                    );
                });
            }
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}