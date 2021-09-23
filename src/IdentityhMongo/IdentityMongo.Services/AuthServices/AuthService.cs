using IdentityMongo.Dtos.AuthDtos;
using IdentityMongo.Models;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityMongo.Repositories.RoleRepositories;

namespace IdentityMongo.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly IRoleRepository roleRepository;
        public AuthService(IRoleRepository roleRepository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.roleRepository = roleRepository;
        }

        public async Task<ServiceResponse<RoleDto>> CreateRole(RoleDto roleDto)
        {
            ServiceResponse<RoleDto> response = new();
            var role = await roleManager.FindByNameAsync(roleDto.rolename);
            if(role != null)
            {
                
                response.Messages.Add("Role Already Exists.");
                return response;
            }
            ApplicationRole applicationRole = new ApplicationRole
            {
                Name = roleDto.rolename
            };
           await roleRepository.CreateAsync(applicationRole);
            response.Messages.Add($"{roleDto.rolename} Role Created.");
            return response;


            

        }

        public async Task<ServiceResponse<TokenDto>> Login(LoginDto loginDto)
        {
            ServiceResponse<TokenDto> response = new();
            var user = await userManager.FindByNameAsync(loginDto.username);
            if(user != null && await userManager.CheckPasswordAsync(user, loginDto.password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(

                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                var Token = new JwtSecurityTokenHandler().WriteToken(token);
                TokenDto tokenDto = new TokenDto
                {
                    token = Token
                };
                response.Data = tokenDto;
                response.StatusCode = System.Net.HttpStatusCode.OK;
                return response;
            }

            response.Messages.Add("Credential Not Matched");
            response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return response;
        }


    }
}
