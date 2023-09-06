using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            /*this gives our server enough information to take a look at the token and validate it just based on the 
            issuer signing key which we have implemented.*/
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    //our server is going to check the token signing key and make sure it is valid based upon the issuer signing key, which will specify next.

                    ValidateIssuerSigningKey = true, //checking if our token is signed by the issuer
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
             });

             return services;
        }
    }
}