using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.BLL.Queryes.Countries;
using RegistrationWizard.BLL.Queryes.Provinces;
using RegistrationWizard.Controllers;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.Tests
{
    public class CountriesControllerTests
    {
        [Fact]
        public async Task GetCountries_ReturnsOkResult_WithData()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var sampleCountries = new List<Country>
            {
                new Country {
                    Id = 1,
                    Name = "Country A",
                    Provinces = new List<Province>
                    {
                        new Province { Id = 1, Name = "Province A1", CountryId = 1 },
                        new Province { Id = 2, Name = "Province A2", CountryId = 1 }
                    }
                },
                new Country {
                    Id = 2,
                    Name = "Country B",
                    Provinces = new List<Province>
                    {
                        new Province { Id = 3, Name = "Province B1", CountryId = 2 },
                        new Province { Id = 4, Name = "Province B2", CountryId = 2 }
                    }
                }
            };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sampleCountries);

            var controller = new CountriesController(mediatorMock.Object);

            // Act
            var actionResult = await controller.GetCountries();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var baseResponse = Assert.IsType<BaseResponseDTO<CountryResponseDTO>>(okResult.Value);
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

            var sampleProvinces = new List<Province>
            {
                new Province { Id = 1, Name = "Province A1", CountryId = 1 },
                new Province { Id = 2, Name = "Province A2", CountryId = 1 }
            };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetProvincesByCountryIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(sampleProvinces);

            var controller = new CountriesController(mediatorMock.Object);
            int testCountryId = 1;

            // Act
            var actionResult = await controller.GetProvincesByCountry(testCountryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult);
            var baseResponse = Assert.IsType<BaseResponseDTO<ProvinceResponceDTO>>(okResult.Value);
            Assert.NotNull(baseResponse.Data);
            Assert.Equal(2, baseResponse.Data.Count);

            Assert.All(baseResponse.Data, p => Assert.Equal(testCountryId, p.CountryId));
        }

        [Fact]
        public async Task GetCountries_WhenMediatorThrowsException_Returns500()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllCountriesQuery>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Test exception"));

            var controller = new CountriesController(mediatorMock.Object);

            // Act
            var actionResult = await controller.GetCountries();

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(actionResult);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
