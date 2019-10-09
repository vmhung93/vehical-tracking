using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Common.Constants;
using VehicleTracking.Common.Exceptions;
using VehicleTracking.Domain.ApplicationUser.Models;
using VehicleTracking.Service.Models;

namespace VehicleTracking.Service.User
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signinManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signinManager = signinManager;
        }

        public async Task<string> SignIn(SignInModel signInModel)
        {
            var user = await _userManager.FindByEmailAsync(signInModel.Email);

            if (user == null)
            {
                throw new CustomException(ErrorCodes.EC_User_001, signInModel.Email);
            }

            var result = await _signinManager.CheckPasswordSignInAsync(user, signInModel.Password, false);

            if (!result.Succeeded)
            {
                throw new CustomException(ErrorCodes.EC_User_002);
            }

            var token = await GenerateJWT(user);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> SignUp(SignUpModel signUpModel)
        {
            var user = new ApplicationUser()
            {
                UserName = signUpModel.UserName,
                Email = signUpModel.Email
            };

            // Create user
            var result = await _userManager.CreateAsync(user, signUpModel.Password);

            if (!result.Succeeded)
            {
                throw new CustomException(ErrorCodes.EC_User_003, string.Join(" . ", result.Errors));
            }

            // Set default Role is vehicleTrackingUser
            await _userManager.AddToRoleAsync(user, UserRoles.User);

            // Generate token
            var token = await GenerateJWT(user);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<JwtSecurityToken> GenerateJWT(ApplicationUser vehicleTrackingUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Tokens:ExpiredTime"]));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, vehicleTrackingUser.Email),
                new Claim(ClaimTypes.NameIdentifier, vehicleTrackingUser.Id.ToString())
            };

            //Assign Roles
            var roles = await _userManager.GetRolesAsync(vehicleTrackingUser);
            foreach (var r in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, r));
            }

            return new JwtSecurityToken(_configuration["Tokens:Issuer"], _configuration["Tokens:Audience"],
                claims, expires: expires, signingCredentials: credentials);
        }
    }
}
