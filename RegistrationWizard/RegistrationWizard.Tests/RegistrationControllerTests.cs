using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationWizard.BLL.Commands;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.Controllers;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.Tests;

public class RegistrationControllerTests
{
    [Fact]
    public async Task Register_ReturnsBadRequest_When_InvalidData()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new RegistrationController(mediatorMock.Object);

        var invalidUserRequest = new UserRequestDTO
        {
            Email = "",
            Password = "",
            CountryId = 0,
            ProvinceId = 0
        };

        // Act
        var result = await controller.Register(invalidUserRequest);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var responseDto = Assert.IsType<RegisterPostResponseDTO>(badRequestResult.Value);
        Assert.True(responseDto.IsError);
        Assert.Equal("Invalid data.", responseDto.Errors);
    }

    //[Fact]
    //public async Task Register_ReturnsOkResult_When_Success()
    //{
    //    // Arrange
    //    var mediatorMock = new Mock<IMediator>();

    //    mediatorMock
    //        .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
    //        .ReturnsAsync(new AppUser());

    //    var controller = new RegistrationController(mediatorMock.Object);
    //    var validUserRequest = new UserRequestDTO
    //    {
    //        Email = "test@example.com",
    //        Password = "password",
    //        CountryId = 1,
    //        ProvinceId = 1
    //    };

    //    // Act
    //    var result = await controller.Register(validUserRequest);

    //    // Assert
    //    var okResult = Assert.IsType<OkObjectResult>(result);
    //    var responseDto = Assert.IsType<RegisterPostResponseDTO>(okResult.Value);
    //    Assert.True(responseDto.Success);
    //    Assert.Equal("User registered successfully", responseDto.Message);

    //    mediatorMock.Verify(m => m.Send(It.Is<CreateUserCommand>(cmd =>
    //        cmd.Email == validUserRequest.Email &&
    //        cmd.Password == validUserRequest.Password &&
    //        cmd.CountryId == validUserRequest.CountryId &&
    //        cmd.ProvinceId == validUserRequest.ProvinceId
    //    ), It.IsAny<CancellationToken>()), Times.Once);
    //}

    //[Fact]
    //public async Task Register_ReturnsInternalServerError_When_ExceptionThrown()
    //{
    //    // Arrange
    //    var mediatorMock = new Mock<IMediator>();

    //    mediatorMock
    //        .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
    //        .ThrowsAsync(new Exception("Test exception"));

    //    var controller = new RegistrationController(mediatorMock.Object);
    //    var validUserRequest = new UserRequestDTO
    //    {
    //        Email = "test@example.com",
    //        Password = "password",
    //        CountryId = 1,
    //        ProvinceId = 1
    //    };

    //    // Act
    //    var result = await controller.Register(validUserRequest);

    //    // Assert
    //    var objectResult = Assert.IsType<ObjectResult>(result);
    //    Assert.Equal(500, objectResult.StatusCode);
    //    var errorDto = Assert.IsType<ErrorDTO>(objectResult.Value);
    //    Assert.Contains("Test exception", errorDto.Error);
    //}
}
