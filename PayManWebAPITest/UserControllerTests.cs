using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PayManAPI.Controllers;
using PayManAPI.Dtos;
using PayManAPI.Models;
using PayManAPI.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PayManWebAPITest
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> userRepositroyStub = new();

        [Fact]
        public async Task GetUserAsync_WithNoUser_ReturnsNotFound()
        {
            //Arrange
            userRepositroyStub.Setup(repository => repository.GetuserAsync(It.IsAny<Guid>())).ReturnsAsync((UserModel)null);

            var userController = new UserController(userRepositroyStub.Object);

            //Act
            var result = await userController.GetUserAsync(Guid.NewGuid());

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetUserAsync_WithUser_ReturnsUser()
        {
            //Arrange
            var expectedUser = CreateRandomUser();
            userRepositroyStub.Setup(repository => repository.GetuserAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);

            var userController = new UserController(userRepositroyStub.Object);

            //Act
            var result = await userController.GetUserAsync(expectedUser.Id);

            //Assert
            //With this method from FluentAssertions we dont need to check alle the object properties
            result.Value.Should().BeEquivalentTo(expectedUser, options => options.ComparingByMembers<UserDto>().ExcludingMissingMembers());
        }

        [Fact]
        public async Task UpdateUserAsync_WithExistingUser_ReturnsNoContent()
        {
            //Arrange
            var expectedUser = CreateRandomUser();
            userRepositroyStub.Setup(repository => repository.GetuserAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);

            var userToUpdate = new UpdateUserDto
            {
                UserName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var userController = new UserController(userRepositroyStub.Object);

            //Act
            var result = await userController.UpdateUserAsync(expectedUser.Id, userToUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteUserAsync_WithExistingUser_ReturnsNoContent()
        {
            //Arrange
            var expectedUser = CreateRandomUser();
            userRepositroyStub.Setup(repository => repository.GetuserAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);

            var userController = new UserController(userRepositroyStub.Object);

            //Act
            var result = await userController.DeleteUserAsync(expectedUser.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        private UserModel CreateRandomUser()
        {
            Random rand = new();
            return new()
            {
                Id = Guid.NewGuid(),
                UserName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Frikort = rand.Next(10000),
                Hovedkort = rand.Next(10000),
                CreatedAt = DateTimeOffset.UtcNow

            };
        }
    }
}
