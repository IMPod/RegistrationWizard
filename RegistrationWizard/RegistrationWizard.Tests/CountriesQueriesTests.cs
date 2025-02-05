using RegistrationWizard.BLL.Queryes.Countries;

namespace RegistrationWizard.Tests;

public class CountriesQueriesTests
{
    //[Fact]
    //public async Task GetAllCountriesQuery_Should_Return_All_Countries_With_Provinces()
    //{
    //    // Arrange
    //    var dbName = Guid.NewGuid().ToString();
    //    using var context = InMemoryContextFactory.CreateContext(dbName);
    //    var handler = new GetAllCountriesQueryHandler(context);
    //    var query = new GetAllCountriesQuery();

    //    // Act
    //    var countries = await handler.Handle(query, CancellationToken.None);

    //    // Assert
    //    Assert.NotNull(countries);
    //    Assert.True(countries.Count() >= 2);

    //    var countryA = countries.FirstOrDefault(c => c.Id == 1);
    //    Assert.NotNull(countryA);
    //    Assert.True(countryA.Provinces.Any());
    //}

    //[Fact]
    //public async Task GetCountryByIdQuery_Should_Return_Correct_Country()
    //{
    //    // Arrange
    //    var dbName = Guid.NewGuid().ToString();
    //    using var context = InMemoryContextFactory.CreateContext(dbName);
    //    var handler = new GetCountryByIdQueryHandler(context);
    //    var query = new GetCountryByIdQuery(1);

    //    // Act
    //    var country = await handler.Handle(query, CancellationToken.None);

    //    // Assert
    //    Assert.NotNull(country);
    //    Assert.Equal("Country A", country.Name);
    //    Assert.NotNull(country.Provinces);
    //    Assert.True(country.Provinces.Any());
    //}

    //[Fact]
    //public async Task GetCountryByIdQuery_Should_Return_Null_When_NotFound()
    //{
    //    // Arrange
    //    var dbName = Guid.NewGuid().ToString();
    //    using var context = InMemoryContextFactory.CreateContext(dbName);
    //    var handler = new GetCountryByIdQueryHandler(context);
    //    var query = new GetCountryByIdQuery(999);

    //    // Act
    //    var country = await handler.Handle(query, CancellationToken.None);

    //    // Assert
    //    Assert.Null(country);
    //}
}
