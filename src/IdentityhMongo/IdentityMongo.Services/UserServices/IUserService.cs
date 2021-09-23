using IdentityMongo.Dtos;
using IdentityMongo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMongo.Services.UserServices
{
    public interface IUserService
    {
        Task<ServiceResponse<UserDto>> CreateUserAsync(UserDto userDto);
    }
}
