using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PayManAPI.Controllers;
using PayManAPI.Dtos;
using PayManAPI.Models;
using PayManAPI.Repositories;
using PayManAPI.Security;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PayManWebAPITest
{
    public class LoginControllerTests
    {
        private readonly Mock<IUserRepository> userRepositroyStub = new();
        private readonly Mock<IAuthService> authServiceStub = new();

        [Fact]
        public async Task CreateUserAsync_WithUserToCreate_ReturnsUser()
        {
            //Arrange
            var userToCreate = new CreateUserDto()
            {
                UserName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var loginController = new LoginController(authServiceStub.Object, userRepositroyStub.Object);

            //Act
            var result = await loginController.CreateUserAsync(userToCreate);

            //Assert
            var actionResult = (result as CreatedAtActionResult).Value;
            var createdUser = actionResult.GetType().GetProperty("user").GetValue(actionResult, null) as UserDto;
            userToCreate.Should().BeEquivalentTo(createdUser, options => options.ComparingByMembers<UserDto>().ExcludingMissingMembers());
            createdUser.Id.Should().NotBeEmpty();
            createdUser.CreatedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(1000));
        }

        [Fact]
        public async Task LoginAsync_WithUserToLogin_ReturnsToken()
        {
            //Arrange
            var user = new UserModel()
            {
                UserName = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString()
            };

            var loginmodel = new LoginDto()
            {
                UserName = user.UserName,
                Password = user.Password
            };

            var token = Guid.NewGuid().ToString();
            
            authServiceStub.Setup(authService => authService.AuthenticationAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((user, token));

            var loginController = new LoginController(authServiceStub.Object, userRepositroyStub.Object);

            //Act
            var result = await loginController.LoginAsync(loginmodel);

            //Assert
            var actionResult = (result as OkObjectResult).Value;
            var createdUser = actionResult.GetType().GetProperty("token").GetValue(actionResult, null) as string;
            token.Should().BeEquivalentTo(createdUser);
        }
    }
}
