using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayManAPI.Dtos;
using PayManAPI.Models;
using PayManAPI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayManAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("auth")]
    public class LoginController : ControllerBase
    {
        private readonly IAuthService authService;

        public LoginController(IAuthService authService)
        {
            this.authService = authService;
        }

        //Post /login
        [HttpPost("login")]
        public ActionResult Login(CreateUserDto userDto)
        {
            var token = authService.Authentication(userDto.UserName, userDto.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            var authenticatedUser = authService.GetAuthoriceduser(userDto.UserName, userDto.Password).AsDto();

            return Ok(new { token, authenticatedUser });
        }

        //Post /users
        [HttpPost("create")]
        public ActionResult<UserDto> CreateUser(CreateUserDto userDto)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                UserName = userDto.UserName,
                Password = userDto.Password,
                Frikort = 46000,
                Hovedkort = 0,
                CreatedAt = DateTimeOffset.Now
            };

            authService.CreateUser(user);

            var token = authService.Authentication(user.UserName, user.Password);

            var userToReturn = user.AsDto();

            return CreatedAtAction(nameof(Login), new { id = userToReturn.Id }, new { token, userToReturn });
        }
    }
}
