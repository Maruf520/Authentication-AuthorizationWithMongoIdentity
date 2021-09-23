using IdentityMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace IdentityMongo.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<string> CreateAdminAsync(ApplicationUser applicationUser, string password, ApplicationRole applicationRole)
        {
            await userManager.CreateAsync(applicationUser, password);
            await userManager.AddToRoleAsync(applicationUser, applicationRole.Name);
            return "";
        }

        public async Task<string> CreateAsync(ApplicationUser applicationUser, string password)
        {
            var userToCreate = await userManager.CreateAsync(applicationUser, password);
            return userToCreate.ToString();
            
        }
    }
}
