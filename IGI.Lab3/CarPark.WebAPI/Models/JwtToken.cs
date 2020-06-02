using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CarPark.WebAPI.Models
{
    public class JwtToken
    {
        public string Token { get; set; }
    }
}
