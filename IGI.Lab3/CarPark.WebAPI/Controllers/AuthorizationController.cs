using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarPark.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CarPark.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly ILogger<AuthorizationController> _logger;


        public AuthorizationController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<AuthorizationController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: api/Autorization
        [HttpGet]
        public async Task<ActionResult<JwtToken>> GetToken([FromQuery] Login login)
        {
            try
            {
                var signInUser = await _userManager.FindByEmailAsync(login.Email);

                var signInResult = await _signInManager.CheckPasswordSignInAsync(signInUser, login.Password, false);

                var roleClaims = (await _userManager.GetRolesAsync(signInUser))
                    .Select(role => new Claim(ClaimTypes.Role, role));

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, login.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, login.Email)
                };

                claims = claims.Concat(roleClaims).ToArray();

                if (signInResult.Succeeded)
                {
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInformation.Key));

                    var credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(JwtInformation.Issuer,JwtInformation.Audience,claims,expires:DateTime.Now.AddHours(3),signingCredentials: credential);

                    var tokenResult = new JwtToken
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token)
                    };

                    return tokenResult;
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured during creating token. Exception {ex.Message}");
                return BadRequest();
            }
        }

    }
}
