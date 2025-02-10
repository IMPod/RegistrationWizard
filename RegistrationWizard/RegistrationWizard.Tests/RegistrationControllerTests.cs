using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationWizard.BLL.Commands;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.Server.Controllers;

namespace RegistrationWizard.Tests;

public class RegistrationControllerTests
{
    [Fact]
    public async Task Register_ReturnsBadRequest_When_InvalidData()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var controller = new RegistrationController(mediatorMock.Object);

        var invalidUserRequest = new UserRequestDto
        {
            Email = "",
            Password = "",
            CountryId = 0,
            ProvinceId = 0
        };
        var cancellationToken = new CancellationToken();
        // Act
        var result = await controller.Register(invalidUserRequest, cancellationToken);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var responseDto = Assert.IsType<RegisterPostResponseDTO>(badRequestResult.Value);

        Assert.True(responseDto.IsError);
        Assert.Equal("Invalid data.", responseDto.Errors);
    }

    [Fact]
    public async Task Register_ReturnsOkResult_When_ValidData()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserRequestDto
            {
                Email = "test@example.com",
                Password = "password",
                CountryId = 1,
                ProvinceId = 1
            });

        var controller = new RegistrationController(mediatorMock.Object);

        var validUserRequest = new UserRequestDto
        {
            Email = "test@example.com",
            Password = "password",
            CountryId = 1,
            ProvinceId = 1
        };
        var cancellationToken = new CancellationToken();

        // Act
        var result = await controller.Register(validUserRequest, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseDto = Assert.IsType<RegisterPostResponseDTO>(okResult.Value);

        Assert.True(responseDto.Success);
        Assert.Equal("User registered successfully", responseDto.Message);

        mediatorMock.Verify(m => m.Send(
            It.Is<CreateUserCommand>(cmd =>
                cmd.Email == validUserRequest.Email &&
                cmd.Password == validUserRequest.Password &&
                cmd.CountryId == validUserRequest.CountryId &&
                cmd.ProvinceId == validUserRequest.ProvinceId
            ),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

}
