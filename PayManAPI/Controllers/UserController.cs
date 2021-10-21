using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using PayManAPI.Repositories;
using PayManAPI.Models;
using PayManAPI.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using PayManAPI.Security;

namespace PayManAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repository;
        private readonly PasswordAuthentication passAuth;

        //Dependency injection to inject the UserRepository into the UserController.
        //This way UserRepository can depend on an abstraction, allowing us to register different dependencides.
        //All this is also done as a singleton so we dont need to construct a UserRepository every time we use the api (Look in Startup.cs).
        public UserController(IUserRepository repositroy)
        {
            this.repository = repositroy;
            //Should this be dependency injected?
            passAuth = new PasswordAuthentication();

        }

        //Get /users
        [HttpGet]
        public IEnumerable<UserDto> GetUsers()
        {
            var users = repository.GetUsers().Select(user => user.AsDto());
            return users;
        }

        //Get /users/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUser(Guid id)
        {
            var user = repository.Getuser(id);

            if (user is null) {
                return NotFound();
            }
            return user.AsDto();
        }

        //Put /users
        [HttpPut("{id}")]
        public ActionResult UpdateUser(Guid id, UpdateUserDto userDto)
        {
            var userToUpdate = repository.Getuser(id);

            if (userToUpdate is null)
            {
                return NotFound();
            }

            //Creating a copy of user with new updates
            User updateUser = userToUpdate with
            {
                UserName = userDto.UserName,
                Password = passAuth.generatePassword(userDto.Password)
            };

            repository.UpdateUser(updateUser);

            return NoContent();
        }

        //Delete users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(Guid id)
        {
            var userToDelete = repository.Getuser(id);

            if (userToDelete is null)
            {
                return NotFound();
            }

            repository.DeleteUser(id);

            return NoContent();
        }
    }
}
