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
        private readonly IUserRepository userRepository;
        private readonly IPasswordAuthentication passAuth;

        //Dependency injection to inject the UserRepository into the UserController.
        //This way UserRepository can depend on an abstraction, allowing us to register different dependencides.
        //All this is also done as a singleton so we dont need to construct a UserRepository every time we use the api (Look in Startup.cs).
        public UserController(IAuthService authService, IUserRepository userRepositroy, IPasswordAuthentication passAuth)
        {
            this.authService = authService;
            this.userRepository = userRepositroy;
            this.passAuth = passAuth;
        }

        /// <summary>
        /// Get method returnning a User as JSON
        /// </summary>
        /// <returns>UserDto</returns>
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUserAsync()
        {
            var user = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));

            if (user is null) {
                return NotFound();
            }
            return user.AsUserDto();
        }

        /// <summary>
        /// Put method for updating a User
        /// </summary>
        /// <returns>ActionResult NoContent</returns>
        [HttpPut]
        public async Task<ActionResult> UpdateUserAsync(CreateUpdateUserDto userDto)
        {
            var userToUpdate = await userRepository.GetuserAsync(Guid.Parse(authService.GetUserIdFromToken(User)));

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

            await userRepository.UpdateUserAsync(updateUser);

            return NoContent();
        }

        /// <summary>
        /// Delete method for deleting a User
        /// </summary>
        /// <returns>ActionResult NoContent</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync()
        {
            var id = Guid.Parse(authService.GetUserIdFromToken(User));

            var userToDelete = await userRepository.GetuserAsync(id);

            if (userToDelete is null)
            {
                return NotFound();
            }

            await userRepository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
