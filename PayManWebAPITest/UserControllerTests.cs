using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PayManAPI.Controllers;
using PayManAPI.Dtos;
using PayManAPI.Models;
using PayManAPI.Repositories;
using PayManAPI.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace PayManWebAPITest
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> userRepositroyStub = new();
        private readonly Mock<IAuthService> authServiceStub = new();
        private readonly Mock<IPasswordAuthentication> passwordAuthenticationStub = new();

        [Fact]
        public async Task GetUserAsync_WithNoUser_ReturnsNotFound()
        {
            //Arrange
            var userId = Guid.NewGuid();
            userRepositroyStub.Setup(repository => repository.GetuserAsync(It.IsAny<Guid>())).ReturnsAsync((UserModel)null);
            authServiceStub.Setup(authService => authService.GetUserIdFromToken(It.IsAny<ClaimsPrincipal>())).Returns(userId.ToString());

            var userController = new UserController(authServiceStub.Object, userRepositroyStub.Object, passwordAuthenticationStub.Object);

            //Act
            var result = await userController.GetUserAsync();

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetUserAsync_WithUser_ReturnsUser()
        {
            //Arrange
            var expectedUser = CreateRandomUser();
            userRepositroyStub.Setup(repository => repository.GetuserAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);
            authServiceStub.Setup(authService => authService.GetUserIdFromToken(It.IsAny<ClaimsPrincipal>())).Returns(expectedUser.Id.ToString());

            var userController = new UserController(authServiceStub.Object, userRepositroyStub.Object, passwordAuthenticationStub.Object);

            //Act
            var result = await userController.GetUserAsync();

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
            authServiceStub.Setup(authService => authService.GetUserIdFromToken(It.IsAny<ClaimsPrincipal>())).Returns(expectedUser.Id.ToString());

            var userToUpdate = new CreateUpdateUserDto
            {
                UserName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var userController = new UserController(authServiceStub.Object, userRepositroyStub.Object, passwordAuthenticationStub.Object);

            //Act
            var result = await userController.UpdateUserAsync(userToUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteUserAsync_WithExistingUser_ReturnsNoContent()
        {
            //Arrange
            var expectedUser = CreateRandomUser();
            userRepositroyStub.Setup(repository => repository.GetuserAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);
            authServiceStub.Setup(authService => authService.GetUserIdFromToken(It.IsAny<ClaimsPrincipal>())).Returns(expectedUser.Id.ToString());

            var userController = new UserController(authServiceStub.Object, userRepositroyStub.Object, passwordAuthenticationStub.Object);

            //Act
            var result = await userController.DeleteUserAsync();

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
