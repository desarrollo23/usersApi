using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Common.Response.Security;
using User.Model.Interfaces.Engine;
using User.Model.Requests;

namespace User.Engine
{
    public class SecurityEngine : ISecurityEngine
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signingManager;

        public SecurityEngine(
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration,
            SignInManager<IdentityUser> signingManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signingManager = signingManager;
        }

        public async Task<AuthenticationResponse> Login(UserCredentials userCredentials)
        {
            var response = new AuthenticationResponse();

            var result =
                await _signingManager.PasswordSignInAsync(userCredentials.Email, userCredentials.Password,
                 isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                response = GetToken(userCredentials);
            else
                response.ErrorMessage = "Error trying to login, please try again";

            return response;
        }

        public async Task<AuthenticationResponse> RegisterUser(UserCredentials userCredentials)
        {
            var response = new AuthenticationResponse();

            var user = new IdentityUser { Email = userCredentials.Email, UserName = userCredentials.Email };
            var result = await _userManager.CreateAsync(user, userCredentials.Password);

            if (result.Succeeded)
            {
                response = GetToken(userCredentials);
            }
            else
                response.ErrorMessage = "An error has ocurred, please try again";

            return response;
        }

        protected AuthenticationResponse GetToken(UserCredentials userCredentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", userCredentials.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.Now.AddMinutes(15);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);

            return new AuthenticationResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiration = expiration
            };
        }
    }
}
