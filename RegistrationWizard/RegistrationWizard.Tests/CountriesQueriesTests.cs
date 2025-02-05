using RegistrationWizard.BLL.Queryes.Countries;

namespace RegistrationWizard.Tests
{
    public class CountriesQueriesTests
    {
        [Fact]
        public async Task GetAllCountriesQuery_Should_Return_All_Countries_With_Provinces()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = InMemoryContextFactory.CreateContext(dbName);
            var mapper = InMemoryContextFactory.CreateMapper();

            var handler = new GetAllCountriesQueryHandler(context, mapper);
            var query = new GetAllCountriesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);              
            Assert.NotEmpty(result);            

            var countryA = result.FirstOrDefault(c => c.Id == 1);
            Assert.NotNull(countryA);
            Assert.False(string.IsNullOrEmpty(countryA.Name));

            Assert.NotEmpty(countryA.Provinces); 
        }

        [Fact]
        public async Task GetCountryByIdQuery_Should_Return_Correct_Country()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = InMemoryContextFactory.CreateContext(dbName);
            var mapper = InMemoryContextFactory.CreateMapper();

            var handler = new GetCountryByIdQueryHandler(context, mapper);
            var query = new GetCountryByIdQuery(1);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);

            var countryDto = result;
            Assert.Equal("Country A", countryDto.Name);
            Assert.NotNull(countryDto.Provinces);
            Assert.NotEmpty(countryDto.Provinces);
        }

        [Fact]
        public async Task GetCountryByIdQuery_Should_Return_Null_When_NotFound()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            using var context = InMemoryContextFactory.CreateContext(dbName);
            var mapper = InMemoryContextFactory.CreateMapper();

            var handler = new GetCountryByIdQueryHandler(context, mapper);
            var query = new GetCountryByIdQuery(999); 

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
