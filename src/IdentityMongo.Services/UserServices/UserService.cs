using IdentityMongo.Dtos;
using IdentityMongo.Models;
using IdentityMongo.Repositories.UserRepositories;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMongo.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public async Task<ServiceResponse<UserDto>> CreateUserAsync(UserDto userDto)
        {
            ServiceResponse <UserDto > response = new();
            var existsUser = await userManager.FindByNameAsync(userDto.UserName);
            if(existsUser != null)
            {
                response.Messages.Add("User Name Already Exists.");
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email
            };
            await userRepository.CreateAsync(user,userDto.Password);
            response.Messages.Add("Created");
            return response;
        }
    }
}
