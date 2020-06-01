using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CarPark.WebAPI.Models
{
    public class JwtToken
    {
        public const string Issuer = "CarPark";
        public const string Audience = "ApiUser";
        public const string Key = "1234567890123456";
        public const string AuthSchemes = "Identity.Application" + "," + JwtBearerDefaults.AuthenticationScheme;

        public string Token { get; set; }
    }
}
