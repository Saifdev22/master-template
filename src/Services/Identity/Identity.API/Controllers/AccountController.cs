using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IConfiguration _config) : ControllerBase
    {
        private static ConcurrentDictionary<string, string> UserData { get; set; } = 
            new ConcurrentDictionary<string, string>();

        [HttpPost("login/{email}/{password}")]
        public async Task<IActionResult> Login(string email, string password)
        {
            await Task.Delay(500);
            var getEmail = UserData!.Keys.Where(e => e.Equals(email)).FirstOrDefault();

            if (!string.IsNullOrEmpty(getEmail))
            {
                UserData.TryGetValue(getEmail, out string? dbPassword);
                if (!Equals(dbPassword, password)) 
                    return BadRequest("Invalid Credentials");

                string jwtToken = GenerateToken(getEmail);
                return Ok(jwtToken);

            }
            return NotFound("Email not found");
        }

        [HttpPost("register/{email}/{password}")]
        public async Task<IActionResult> Register(string email, string password)
        {
            await Task.Delay(500);
            var getEmail = UserData!.Keys.Where(e => e.Equals(email)).FirstOrDefault();

            if (!string.IsNullOrEmpty(getEmail))
                return BadRequest("User already exist");

            UserData[email] = password;
            return Ok("User created successfully");
        }

        private string GenerateToken(string getEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, getEmail!),
            };

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;
        }
    }
}
