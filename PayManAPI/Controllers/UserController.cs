using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using PayManAPI.Repositories;
using PayManAPI.Models;
using PayManAPI.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using PayManAPI.Security;
using System.Threading.Tasks;

namespace PayManAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserRepository repository;
        private readonly IPasswordAuthentication passAuth;

        //Dependency injection to inject the UserRepository into the UserController.
        //This way UserRepository can depend on an abstraction, allowing us to register different dependencides.
        //All this is also done as a singleton so we dont need to construct a UserRepository every time we use the api (Look in Startup.cs).
        public UserController(IAuthService authService, IUserRepository repositroy, IPasswordAuthentication passAuth)
        {
            this.authService = authService;
            this.repository = repositroy;
            this.passAuth = passAuth;
        }

        //Get /users
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUserAsync()
        {
            var user = await repository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));

            if (user is null) {
                return NotFound();
            }
            return user.AsUserDto();
        }

        //Put /users
        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(UpdateUserDto userDto)
        {
            var userToUpdate = await repository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));

            if (userToUpdate is null)
            {
                return NotFound();
            }

            //Creating a copy of user with new updates
            UserModel updateUser = userToUpdate with
            {
                UserName = userDto.UserName,
                Password = passAuth.generatePassword(userDto.Password)
            };

            await repository.UpdateUserAsync(updateUser);

            return NoContent();
        }

        //Delete users
        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync()
        {
            var id = Guid.Parse(authService.GetUserIdFromToken(User));

            var userToDelete = await repository.GetuserAsync(id);

            if (userToDelete is null)
            {
                return NotFound();
            }

            await repository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
