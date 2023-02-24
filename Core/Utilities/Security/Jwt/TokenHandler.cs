using Core.Extensions;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateToken(User user, List<OperationClaim> operationClaims)
        {
            Token token = new Token();

            var key = _configuration["TokenOptions:SecurityKey"];
            // security keyin simetriğini almak
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // şifrelenmiş kimliği oluşturmak
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // token ayarları
            token.ExpirationTime = DateTime.Now.AddMinutes(60);
            JwtSecurityToken securityToken = new JwtSecurityToken
            (
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.ExpirationTime,
                claims: SetClaims(user, operationClaims),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
            );

            // token oluşturucu sınıfından bir örnek alalım

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            // token üretmek
            token.AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            // refresh token üretmek

            token.RefreshToken = CreateRefreshToken();
            return token;

        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddName(user.Name);
            claims.AddRoles(operationClaims.Select(p => p.Name).ToArray());
            return claims;
        }
    }
}
