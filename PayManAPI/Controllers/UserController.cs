using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using PayManAPI.DataFacades;
using PayManAPI.Models;
using PayManAPI.Dtos;
using System.Linq;

//See: https://youtu.be/ZXdFisA_hOY?t=2415
namespace PayManAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserFacadeInterface repository;

        //Dependency injection to inject the UserRepository into the UserController.
        //This way UserRepository can depend on an abstraction, allowing us to register different dependencides.
        //All this is also done as a singleton so we dont need to construct a UserRepository every time we use the api (Look in Startup.cs).
        public UserController(UserFacadeInterface repositroy)
        {
            this.repository = repositroy;
        }

        //Get /users
        [HttpGet]
        public IEnumerable<UserDto> GetUsers()
        {
            var users = repository.GetUsers().Select( user => user.AsDto());
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

        //Post /users
        [HttpPost]
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

            repository.CreateUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user.AsDto());
        }

        //Put /users
        [HttpPut]
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
                Password = userDto.Password
            };

            repository.UpdateUser(updateUser);

            return NoContent();
        }

        //Delete users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
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
