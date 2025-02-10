using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.BLL.Queries.Countries;
using RegistrationWizard.BLL.Queries.Provinces;
using RegistrationWizard.Server.Controllers;

namespace RegistrationWizard.Tests;

public class CountriesControllerTests
{
    [Fact]
    public async Task GetCountries_ReturnsOkResult_WithData()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        var sampleCountries = new List<CountryResponseDTO>
            {
                new()
                {
                    Id = 1,
                    Name = "Country A",
                    Provinces = new List<ProvinceResponceDTO>
                    {
                        new() { Id = 1, Name = "Province A1", CountryId = 1 },
                        new() { Id = 2, Name = "Province A2", CountryId = 1 }
                    }
                },
                new()
                {
                    Id = 2,
                    Name = "Country B",
                    Provinces = new List<ProvinceResponceDTO>
                    {
                        new() { Id = 3, Name = "Province B1", CountryId = 2 },
                        new() { Id = 4, Name = "Province B2", CountryId = 2 }
                    }
                }
            };
        var request = new BaseResponseDto<CountryResponseDTO>()
        {
            Data = sampleCountries.ToList()
        };
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllCountriesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(request);

        var controller = new CountriesController(mediatorMock.Object);
        var cancellationToken = new CancellationToken();

        // Act
        var actionResult = await controller.GetCountries(cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);

        var baseResponse = Assert.IsType<BaseResponseDto<CountryResponseDTO>>(okResult.Value);
        Assert.NotNull(baseResponse.Data);
        Assert.Equal(2, baseResponse.Data.Count);

        var countryA = baseResponse.Data.FirstOrDefault(c => c.Id == 1);
        Assert.NotNull(countryA);
        Assert.Equal("Country A", countryA.Name);
        Assert.Equal(2, countryA.Provinces.Count);
    }

    [Fact]
    public async Task GetProvincesByCountry_ReturnsOkResult_WithData()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        var sampleProvinces = new List<ProvinceResponceDTO>
            {
                new() { Id = 1, Name = "Province A1", CountryId = 1 },
                new() { Id = 2, Name = "Province A2", CountryId = 1 }
            };

        var request = new BaseResponseDto<ProvinceResponceDTO>()
        {
            Data = sampleProvinces.ToList()
        };

        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetProvincesByCountryIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(request);

        var controller = new CountriesController(mediatorMock.Object);
        int testCountryId = 1;
        var cancellationToken = new CancellationToken();

        // Act
        var actionResult = await controller.GetProvincesByCountry(testCountryId, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);

        var baseResponse = Assert.IsType<BaseResponseDto<ProvinceResponceDTO>>(okResult.Value);
        Assert.NotNull(baseResponse.Data);
        Assert.Equal(2, baseResponse.Data.Count);
        Assert.All(baseResponse.Data, p => Assert.Equal(testCountryId, p.CountryId));
    }

    [Fact]
    public async Task GetCountries_WhenMediatorThrowsException_ThrowsException()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();

        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllCountriesQuery>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Test exception"));

        var controller = new CountriesController(mediatorMock.Object);
        var cancellationToken = new CancellationToken();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => controller.GetCountries(cancellationToken));
    }

}
