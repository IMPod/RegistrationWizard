using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RegistrationWizard.DAL;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.Tests;

public class CountriesControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly RegistrationContext _dbContext;

    public CountriesControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        var scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        var scope = scopeFactory.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<RegistrationContext>();
        _dbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task GetCountries_ReturnsSuccessAndCorrectData()
    {
        // Arrange: Insert test data
        var country = new Country { Name = "Testland" };
        _dbContext.Countries.RemoveRange(_dbContext.Countries.Where(c => c.Name == "Testland"));
        await _dbContext.SaveChangesAsync();

        _dbContext.Countries.Add(country);
        await _dbContext.SaveChangesAsync();

        // Act
        var response = await _client.GetAsync("/api/Countries");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<BaseResponseDto<CountryResponseDTO>>();
        result.Should().NotBeNull();
        result.Data.Should().ContainSingle(c => c.Name == "Testland");
    }

    [Fact]
    public async Task GetProvincesByCountry_ReturnsSuccess()
    {
        // Arrange: Insert test data
        var country = new Country { Name = "Testland" };
        var province = new Province { Name = "TestProvince", Country = country };

        _dbContext.Countries.RemoveRange(_dbContext.Countries.Where(c => c.Name == "Testland"));
        _dbContext.Provinces.RemoveRange(_dbContext.Provinces.Where(c => c.Name == "TestProvince"));

        await _dbContext.SaveChangesAsync();
        _dbContext.Countries.Add(country);
        _dbContext.Provinces.Add(province);
        await _dbContext.SaveChangesAsync();

        // Act
        var response = await _client.GetAsync($"/api/Countries/{country.Id}/provinces");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<BaseResponseDto<ProvinceResponceDTO>>();
        result.Should().NotBeNull();
        result.Data.Should().ContainSingle(p => p.Name == "TestProvince");
    }
}