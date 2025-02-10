using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace RegistrationWizard.Tests;

public class RegistrationControllerIntegrationTests(WebApplicationFactory<Program> factory)
        : IClassFixture<WebApplicationFactory<Program>> 
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenModelIsInvalid()
    {
        // Arrange
        var invalidUserRequest = new
        {
            Email = "",
            Password = "",
            CountryId = 0,
            ProvinceId = 0
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Registration", invalidUserRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("Email is required", responseContent);
    }
}