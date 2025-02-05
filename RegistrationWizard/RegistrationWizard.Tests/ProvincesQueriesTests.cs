﻿using RegistrationWizard.BLL.Queryes.Provinces;

namespace RegistrationWizard.Tests;

public class ProvincesQueriesTests
{
    //[Fact]
    //public async Task GetAllProvincesQuery_Should_Return_All_Provinces()
    //{
    //    // Arrange
    //    var dbName = Guid.NewGuid().ToString();
    //    using var context = InMemoryContextFactory.CreateContext(dbName);
    //    var handler = new GetAllProvincesQueryHandler(context);
    //    var query = new GetAllProvincesQuery();

    //    // Act
    //    var provinces = await handler.Handle(query, CancellationToken.None);

    //    // Assert
    //    Assert.NotNull(provinces);
    //    Assert.True(provinces.Count() >= 4);
    //}

    //[Fact]
    //public async Task GetProvinceByIdQuery_Should_Return_Correct_Province()
    //{
    //    // Arrange
    //    var dbName = Guid.NewGuid().ToString();
    //    using var context = InMemoryContextFactory.CreateContext(dbName);
    //    var handler = new GetProvinceByIdQueryHandler(context);
    //    var query = new GetProvinceByIdQuery(1); 

    //    // Act
    //    var province = await handler.Handle(query, CancellationToken.None);

    //    // Assert
    //    Assert.NotNull(province);
    //    Assert.Equal("Province A1", province.Name);
    //}

    //[Fact]
    //public async Task GetProvinceByIdQuery_Should_Return_Null_When_NotFound()
    //{
    //    // Arrange
    //    var dbName = Guid.NewGuid().ToString();
    //    using var context = InMemoryContextFactory.CreateContext(dbName);
    //    var handler = new GetProvinceByIdQueryHandler(context);
    //    var query = new GetProvinceByIdQuery(999);

    //    // Act
    //    var province = await handler.Handle(query, CancellationToken.None);

    //    // Assert
    //    Assert.Null(province);
    //}

    //[Fact]
    //public async Task GetProvincesByCountryIdQuery_Should_Return_Provinces_For_Given_Country()
    //{
    //    // Arrange
    //    var dbName = Guid.NewGuid().ToString();
    //    using var context = InMemoryContextFactory.CreateContext(dbName);
    //    var handler = new GetProvincesByCountryIdQueryHandler(context);
    //    var query = new GetProvincesByCountryIdQuery(1);

    //    // Act
    //    var provinces = await handler.Handle(query, CancellationToken.None);

    //    // Assert
    //    Assert.NotNull(provinces);
    //    Assert.Equal(2, provinces.Count());
    //}
}
