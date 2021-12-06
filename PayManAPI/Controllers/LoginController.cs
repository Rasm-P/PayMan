using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayManAPI.Dtos;
using PayManAPI.Models;
using PayManAPI.Repositories;
using PayManAPI.Security;
using System;
using System.Collections.Generic;
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
        private readonly IPasswordAuthentication passAuth;

        public LoginController(IAuthService authService, IUserRepository repositroy, IPasswordAuthentication passAuth)
        {
            this.authService = authService;
            this.repositroy = repositroy;
            this.passAuth = passAuth;
        }

        /// <summary>
        /// Post method for login in with credentials
        /// </summary>
        /// <returns>{JWTToken, UserDto}</returns>
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginDto loginDto)
        {
            (UserModel userToReturn, string token) = await authService.AuthenticationAsync(loginDto.UserName, loginDto.Password);

            if (token == null)
            {
                return Unauthorized("Wrong username or password!");
            }

            var user = userToReturn.AsUserDto();

            return Ok(new { token, user });
        }

        /// <summary>
        /// Post method for creating a user
        /// </summary>
        /// <returns>ActionResult CreatedAtAction(name, id, {JWTToken, UserDto})</returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateUserAsync(CreateUpdateUserDto userDto)
        {
            var isUsernameTaken = await repositroy.IsUsernameTaken(userDto.UserName);
            if (isUsernameTaken)
            {
                return Unauthorized("Username already taken!");
            }

            UserModel newUser = new()
            {
                Id = Guid.NewGuid(),
                UserName = userDto.UserName,
                Password = passAuth.generatePassword(userDto.Password),
                Jobs = new List<Guid>(),
                Frikort = 46000,
                Hovedkort = 0,
                CreatedAt = DateTimeOffset.Now
            };

            await repositroy.CreateUserAsync(newUser);

            (UserModel authUser, string token) = await authService.AuthenticationAsync(userDto.UserName, userDto.Password);

            var user = newUser.AsUserDto();

            return CreatedAtAction(nameof(LoginAsync), new { id = user.Id }, new { token, user });
        }
    }
}
