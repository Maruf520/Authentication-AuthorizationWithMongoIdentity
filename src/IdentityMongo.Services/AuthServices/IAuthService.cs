using System;
using IdentityMongo.Models;
using IdentityMongo.Dtos.AuthDtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMongo.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ServiceResponse<TokenDto>> Login(LoginDto loginDto);
        Task<ServiceResponse<RoleDto>> CreateRole(RoleDto roleDto);
    }
}
