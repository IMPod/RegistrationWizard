using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.BLL.Commands;
using RegistrationWizard.BLL.Queryes.Users;

namespace RegistrationWizard.Tests;

public class InMemoryContextFactory
{
    public static RegistrationContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<RegistrationContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new RegistrationContext(options);
        // Ensure database is created and seeded via OnModelCreating
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }
}

public class UserCommandsTests
{
    [Fact]
    public async Task CreateUserCommand_Should_Create_User()
    {
        // Arrange
        var dbName = Guid.NewGuid().ToString();
        using var context = InMemoryContextFactory.CreateContext(dbName);
        var handler = new CreateUserCommandHandler(context);
        var command = new CreateUserCommand
        {
            Email = "test@example.com",
            Password = "Password123",
            CountryId = 1,
            ProvinceId = 1
        };

        // Act
        var createdUser = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(createdUser);
        Assert.True(createdUser.Id > 0);
        Assert.Equal("test@example.com", createdUser.Email);

        var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Id == createdUser.Id);
        Assert.NotNull(userInDb);
    }

    [Fact]
    public async Task GetUserByIdQuery_Should_Return_User_When_Exists()
    {
        // Arrange
        var dbName = Guid.NewGuid().ToString();
        using var context = InMemoryContextFactory.CreateContext(dbName);

        var user = new User
        {
            Email = "existing@example.com",
            Password = "Secret",
            CountryId = 1,
            ProvinceId = 1
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new GetUserByIdQueryHandler(context);
        var query = new GetUserByIdQuery(user.Id);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Email, result.Email);
    }

    [Fact]
    public async Task GetUserByIdQuery_Should_Return_Null_When_NotFound()
    {
        // Arrange
        var dbName = Guid.NewGuid().ToString();
        using var context = InMemoryContextFactory.CreateContext(dbName);
        var handler = new GetUserByIdQueryHandler(context);
        var query = new GetUserByIdQuery(999); // Несуществующий Id

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}