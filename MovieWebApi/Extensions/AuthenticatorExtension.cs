using Microsoft.IdentityModel.Tokens;
using MovieDataLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieWebApi.Extensions
{
    // Source for this helper class: https://medium.com/@sajadshafi/jwt-authentication-in-c-net-core-7-web-api-b825b3aee11d
    public class AuthenticatorExtension(IConfiguration configuration) //Class definition and Class constructor
    {
        public string GenerateJWTToken(UserModel user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"]!)
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public bool ValidateUser(string token, int userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = tokenHandler.ReadJwtToken(token.Substring(7)); //Removes 7 letter word "Bearer " from token

            bool isCorrectUserId = key.Claims.Any(claim => claim.Type == ClaimTypes.NameIdentifier && claim.Value.Equals(userId.ToString()));
            bool isCorrectEmail = key.Claims.Any(claim => claim.Type == ClaimTypes.Email && claim.Value.Equals(email));

            if (isCorrectUserId && isCorrectEmail)
            {
                return true;
            }
            return false;
        }

        public int ExtractUserID(string token) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = tokenHandler.ReadJwtToken(token.Substring(7)); //Removes 7 letter word "Bearer " from token

            int userId = Int32.Parse(key.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);        
            return userId;
        }

    }
}
