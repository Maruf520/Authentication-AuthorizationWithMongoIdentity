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
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<string> CreateAsync(ApplicationUser applicationUser, string password)
        {
            var userToCreate = await userManager.CreateAsync(applicationUser, password);
            return userToCreate.ToString();
            
        }
    }
}
