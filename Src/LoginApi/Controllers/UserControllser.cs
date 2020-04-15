using System;
using System.Threading.Tasks;
using LoginApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user")]
    public class UserControllser : ControllerBase
    {
        private readonly UserManager<MyUser> _userManager;

        public UserControllser(UserManager<MyUser> user)
        {
            _userManager = user;
        }

        [HttpPost("createUser", Name = nameof(CreateUser))]
        public async Task<IActionResult> CreateUser([FromForm] string username,
                                                    [FromForm] string lastName,
                                                    [FromForm] string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(new MyUser
                {
                    UserName = username,
                    LastName = lastName
                }, password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                var responseBody = $@"{$"{{\"username\": \"{username}\", \"lastName\": \"{lastName}\"}}"}";

                return Ok(responseBody);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }

        }
    }
}
