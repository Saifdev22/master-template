using Identity.API.Data;
using Identity.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AppDbContext dbContext) : ControllerBase
    {
        private async Task<bool> IsUserAlreadyAvailable(string email)
        {
            var findEmail = await dbContext.Users.FirstOrDefaultAsync(x => x.Email!.Equals(email));
            return findEmail != null;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            if (await IsUserAlreadyAvailable(user.Email!)) 
                return BadRequest("User already created");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (!await IsUserAlreadyAvailable(email!))
                return BadRequest("User not found!");

            if (!BCrypt.Net.BCrypt.Verify(password, (await dbContext.Users.FirstOrDefaultAsync(_ => _.Email == email))!.Password))
                return BadRequest("Invalid Credentials!");

            var getTokens = await dbContext.Tokens.ToListAsync();
            if (getTokens.Count == 0) return BadRequest("No Token available!");

            var random = new Random();
            var key = random.Next(getTokens.Min(_ => _.Id), getTokens.Max(_ => _.Id));
            var getUserToken = getTokens.FirstOrDefault(x => x.Id == key); 
           
            return Ok($"Add this to your request Header \n Api-Key:{getUserToken!.Value}");
        }
    }
}
