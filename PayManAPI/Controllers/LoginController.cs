using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayManAPI.Dtos;
using PayManAPI.Models;
using PayManAPI.Repositories;
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
        private readonly IUserRepository repositroy;
        private readonly PasswordAuthentication passAuth;

        public LoginController(IAuthService authService, IUserRepository repositroy)
        {
            this.authService = authService;
            this.repositroy = repositroy;
            //Should this be dependency injected?
            passAuth = new PasswordAuthentication();
        }

        //Post /login
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(CreateUserDto userDto)
        {
            (User userToReturn, string token) = await authService.AuthenticationAsync(userDto.UserName, userDto.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            var user = userToReturn.AsDto();

            return Ok(new { token, user });
        }

        //Post /users
        [HttpPost("create")]
        public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto userDto)
        {
            User newUser = new()
            {
                Id = Guid.NewGuid(),
                UserName = userDto.UserName,
                Password = passAuth.generatePassword(userDto.Password),
                Frikort = 46000,
                Hovedkort = 0,
                CreatedAt = DateTimeOffset.Now
            };

            await repositroy.CreateUserAsync(newUser);

            (User authUser, string token) = await authService.AuthenticationAsync(userDto.UserName, userDto.Password);

            var user = authUser.AsDto();

            return CreatedAtAction(nameof(LoginAsync), new { id = user.Id }, new { token, user });
        }
    }
}
