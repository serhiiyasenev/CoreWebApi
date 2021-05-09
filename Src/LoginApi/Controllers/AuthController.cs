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
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<MyUser> _signInManger;
        private readonly UserManager<MyUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthController(SignInManager<MyUser> signInManager, UserManager<MyUser> user, JwtService jwtService)
        {
            _signInManger = signInManager;
            _userManager = user;
            _jwtService = jwtService;
        }

        [HttpPost("token", Name = nameof(GetToken))]
        public async Task<IActionResult> GetToken([FromForm] string username,
                                                  [FromForm] string password)
        {
            // Go To Identity Server

            try
            {
                var result = await _signInManger.PasswordSignInAsync(username, password, false, false);

                if (!result.Succeeded)
                {
                    return Unauthorized(result.ToString());
                }

                var user = await _userManager.Users.Where(u => u.UserName == username).FirstAsync();

                if (user == null)
                {
                    return NotFound(username);
                }

                var token = _jwtService.GetToken(username);


                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest($"Wrong request '{e.Message}': {e.InnerException}");
            }

        }
    }
}
