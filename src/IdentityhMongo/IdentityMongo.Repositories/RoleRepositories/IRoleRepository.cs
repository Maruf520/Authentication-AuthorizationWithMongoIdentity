using IdentityMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMongo.Repositories.RoleRepositories
{
    public interface IRoleRepository
    {
        Task<string> CreateAsync(ApplicationRole applicationRol);
    }
}
