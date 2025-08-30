using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AuthenticationService(UserManager<ApplicationUser> userManager) : IAuthenticationService
    {
        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await userManager.FindByNameAsync(request.Email) ?? throw new FileNotFoundException($"No user with the email : {request.Email} was found");
            var isvalid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!isvalid)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }
            return new(request.Email, user.DisplayName,await CreateTokenAsync(user));

        }

        private static async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            await Task.Delay(TimeSpan.FromSeconds(3)); // Simulate token creation delay
            return "Jwt Token";
        }

        public Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                DisplayName = request.DisplayName,
                PhoneNumber = request.PhoneNumber
            };
            var result = userManager.CreateAsync(user, request.Password).Result;
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            //adding the role "User" to the user
            var roleResult = userManager.AddToRoleAsync(user, "User").Result;
            if (!roleResult.Succeeded)
            {
                throw new Exception(string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            return Task.FromResult(new UserResponse(request.Email, user.DisplayName, CreateTokenAsync(user).Result));
        }
    }
}
