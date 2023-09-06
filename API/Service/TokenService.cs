using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interface;
using Microsoft.IdentityModel.Tokens;

namespace API.Service
{
    //Creating a JWT TOKEN
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        //constructor
        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                //our users are going to have a token that claims their username is what its set to inside the token
                    new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            //Signing Credentials   HmacSha512Signature - strongest alg
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //describing the token that we're going to return
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), //our token will expire after a week
                SigningCredentials = creds

            };

            //token handler to save our token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}