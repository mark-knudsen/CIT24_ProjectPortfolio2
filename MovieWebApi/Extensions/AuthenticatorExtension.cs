using Microsoft.IdentityModel.Tokens;
using MovieDataLayer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

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

        public bool ValidUser(UserModel userModel)
        {
            if (userModel == null) return false;

            userModel.FirstName = userModel.FirstName.Trim();
            userModel.Email = userModel.Email.Trim();
            userModel.Password = userModel.Password.Trim();

            if(userModel.FirstName.Length < 2) return false;

            if (!ValidPassword(userModel.Password) || !ValidEmail(userModel.Email))
            {
                return false;
            }
            return true;
        }

        public bool ValidPassword(string password)
        {
            if (password == null) return false;

            // // match at least one digit, special character and upper cased character, minimum length of 8 characters
            Regex r = new Regex(@"^(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).{8,}$");  // did not make this

            return r.Match(password).Success;
        }
        public bool ValidEmail(string email)
        {
            if (email == null) return false;

            Regex r = new Regex(@"^.+@.*\.[a-z]{2,}$"); // did make this, don't know if it is fully encapsulating enough

            return r.Match(email).Success;
        }

    }
}
