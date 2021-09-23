using IdentityMongo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMongo.Repositories.RoleRepositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        public RoleRepository(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<string> CreateAsync(ApplicationRole applicationRol)
        {
           await roleManager.CreateAsync(applicationRol);
            return "";
        }
    }
}
