using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FirstWebApplication.Authorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApplication.Authorization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<MyUser> _signInManger;
        private readonly UserManager<MyUser> _userManager;
        private readonly JwtSecurityToken _jwtSecurityToken;

        public LoginController(SignInManager<MyUser> signInManager, UserManager<MyUser> user, JwtSecurityToken jwtSecurityToken)
        {
            _signInManger = signInManager;
            _userManager = user;
            _jwtSecurityToken = jwtSecurityToken;
        }


        [HttpGet("token")]
        public async Task<IActionResult> GetToken(string username, string password)
        {
            // Go To Identity Server

            try
            {
                var result = await _signInManger.PasswordSignInAsync(username, password, false, false);

                if (!result.Succeeded)
                {
                    return Unauthorized();
                }

                var user = await _userManager.Users.Where(u => u.UserName == username)
                    .Select(u => u.LastName).FirstAsync();

                if (user == null)
                {
                    return NotFound();
                }


                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }

        }

        [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser([FromForm]string username, [FromForm]string lastName, [FromForm]string password)
        {
            // Go To Identity Server

            try
            {
                var result = await _userManager.CreateAsync(new MyUser
                    {
                        UserName = username,
                        LastName = lastName
                    },
                    password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(username);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }

        }
    }
}
