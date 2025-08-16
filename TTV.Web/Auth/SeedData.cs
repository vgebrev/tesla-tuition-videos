using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using TTV.Web.Auth.Data;
using TTV.Web.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Security.Claims;
using System.Runtime.CompilerServices;
using Duende.IdentityModel;

namespace TTV.Web.Auth;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        AddConfigurationAndOperationalData(scope);
        AddRolesAndUsers(scope);
    }

    private static void AddConfigurationAndOperationalData(IServiceScope scope)
    {
        scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<Config>();
        
        context.Database.Migrate();
        
        if (!context.Clients.Any())
        {
            foreach (var client in config.GetClients())
            {
                context.Clients.Add(client.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (var resource in config.GetIdentityResources())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }

        if (!context.ApiScopes.Any())
        {
            foreach (var resource in config.GetApiScopes())
            {
                context.ApiScopes.Add(resource.ToEntity());
            }
            context.SaveChanges();
        }
    }

    private static void AddRolesAndUsers(IServiceScope scope)
    {
        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        context.Database.Migrate();

        var config = scope.ServiceProvider.GetRequiredService<Config>();
        var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Create roles
        foreach (var roleConfig in config.GetRoles())
        {
            if (!roleMgr.RoleExistsAsync(roleConfig.Name).Result)
            {
                var role = new ApplicationRole { Name = roleConfig.Name };
                var result = roleMgr.CreateAsync(role).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                foreach (var claim in roleConfig.Claims)
                {
                    result = roleMgr.AddClaimAsync(role, new Claim(claim.Type, claim.Value)).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                Log.Debug($"'{roleConfig.Name}' role created");
            }
            else
            {
                Log.Debug($"'{roleConfig.Name}' role already exists");
            }
        }

        // Create users
        foreach (var userConfig in config.GetUsers())
        {
            var user = userMgr.FindByNameAsync(userConfig.UserName).Result;
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userConfig.UserName,
                    Email = userConfig.Email,
                    EmailConfirmed = userConfig.EmailConfirmed,
                };
                var result = userMgr.CreateAsync(user, userConfig.Password).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                if (userConfig.Claims.Length > 0)
                {
                    var claims = userConfig.Claims.Select(c => new Claim(c.Type, c.Value)).ToArray();
                    result = userMgr.AddClaimsAsync(user, claims).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                foreach (var roleName in userConfig.Roles)
                {
                    result = userMgr.AddToRoleAsync(user, roleName).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }

                Log.Debug($"'{userConfig.UserName}' user created");
            }
            else
            {
                Log.Debug($"'{userConfig.UserName}' user already exists");
            }
        }
    }
}
