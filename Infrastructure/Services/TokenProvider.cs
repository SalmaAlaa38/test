using Application.Contracts;
using Domain.Entities;
using Domain.Utilties;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public sealed class TokenProvider
        (
        IOptions<JWT> _jwt
        ) : ITokenProvider
    {
        private readonly JWT jwt = _jwt.Value;
        public AccessToken GenerateAccessToken(User user)
        {
            try
            {
                var Claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: jwt.Issuer,
                    audience: jwt.Audience,
                    claims: Claims,
                    expires: DateTime.Now.AddMinutes(jwt.Expiration),
                    signingCredentials: signingCredentials);

                return new AccessToken
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Expiration = DateTime.Now.AddMinutes(jwt.Expiration),
                    Role = user.Role
                };
            }
            catch (Exception ex) { 
                throw;
            }
        }

        public RefreshToken GenerateRefreshToken()
        {
            var token = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(token);
            }
            return new RefreshToken { token = Convert.ToBase64String(token) };
        }
    }
}
