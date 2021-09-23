using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityMongo.Models;

namespace IdentityMongo.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<string> CreateAsync(ApplicationUser applicationUser, string password);
    }
}
