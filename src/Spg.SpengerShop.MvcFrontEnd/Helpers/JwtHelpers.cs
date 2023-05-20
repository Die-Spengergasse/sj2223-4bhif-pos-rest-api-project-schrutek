using Microsoft.IdentityModel.Tokens;
using Spg.SpengerShop.Domain.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Spg.SpengerShop.MvcFrontEnd.Helpers
{
    public class JwtHelpers
    {
        public static string GenerateToken(UserDto dto, string salt)
        {
            byte[] secret = Convert.FromBase64String(salt);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, dto.UserName),
                    new Claim(ClaimTypes.Surname, dto.FirstName),
                    new Claim(ClaimTypes.GivenName, dto.LastName),
                    new Claim(ClaimTypes.Email, dto.EMail),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, dto.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static UserDto ReadToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken userInformation = tokenHandler.ReadJwtToken(token);

            //TODO: Checks auf NULL oder ungültigen Inhalt
            return new UserDto()
            {
                UserName = userInformation.Claims.SingleOrDefault(c => c.Type == "unique_name")?.Value ?? string.Empty,
                FirstName = userInformation.Claims.SingleOrDefault(c => c.Type == "family_name")?.Value ?? string.Empty,
                LastName = userInformation.Claims.SingleOrDefault(c => c.Type == "given_name")?.Value ?? string.Empty,
                EMail = userInformation.Claims.SingleOrDefault(c => c.Type == "email")?.Value ?? string.Empty,
                Role = userInformation.Claims.SingleOrDefault(c => c.Type == "role")?.Value ?? string.Empty,
                //Signature = userInformation.RawSignature
            };
        }
    }
}
