using LoginApi.Models;
using LoginApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(SignInManager<MyUser> signInManager, UserManager<MyUser> userManager, JwtService jwtService) : ControllerBase
    {
        [HttpPost("token", Name = nameof(GetToken))]
        public async Task<IActionResult> GetToken([FromForm] string username, [FromForm] string password)
        {
            // Go To Identity Server

            try
            {
                var result = await signInManager.PasswordSignInAsync(username, password, false, false);

                if (!result.Succeeded)
                {
                    return Unauthorized(result.ToString());
                }

                var user = await userManager.Users.Where(u => u.UserName == username).FirstAsync();

                if (user == null)
                {
                    return NotFound(username);
                }

                var token = jwtService.GetToken(username);

                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest($"Wrong request '{e.Message}': {e.InnerException}; {e.InnerException?.Message}");
            }
        }
    }
}
