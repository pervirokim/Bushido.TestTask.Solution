using Bushido.TestTask.Cloud.Authentication.Models;
using Bushido.TestTask.Cloud.Authentication.Models.DBModel;
using Bushido.TestTask.Cloud.Authentication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;

[TestFixture]
public class AuthServiceTests
{
    [Test]
    public async Task SignUp_ReturnsTokenWhenUserDoesNotExist()
    {
        // Arrange
        var signUpModel = new SignUpModel
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            Password = "password123"
        };

        var dbContextMock = new Mock<AuthenticationDbContext>();
        dbContextMock.Setup(d => d.Users.FirstOrDefault(u => u.Email == signUpModel.Email)).Returns((User)null);

        var authService = new AuthService(Mock.Of<IConfiguration>(), Mock.Of<IHttpContextAccessor>(), dbContextMock.Object);

        // Act
        var result = await authService.SignUp(signUpModel);

        Xunit.Assert.NotNull(result);
    }

}