using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
namespace Api.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private IHttpContextAccessor _contextAccessor;

        public AuthenticationService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public TokenModel GetCurrentUser()
            => _contextAccessor.HttpContext?.Items?["CurrentCustomer"] != null ?
                    _contextAccessor.HttpContext.Items["CurrentCustomer"] as TokenModel : null;

        public string GetToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("SomeStaticAccessKey12345!");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "SomeCustomApp",
                Issuer = "wizlo.api.demo",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.Email),
                    new Claim("CustomerId", customer.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}