using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.BLL.Commands;
using RegistrationWizard.BLL.Queries.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace RegistrationWizard.Tests;


public class UserCommandsTests
{
    [Fact]
    public async Task CreateUserCommand_Should_Create_User()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddDbContext<RegistrationContext>(options =>
            options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}"));

        services.AddIdentityCore<AppUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
        })
        .AddEntityFrameworkStores<RegistrationContext>();

        var provider = services.BuildServiceProvider();
        var userManager = provider.GetRequiredService<UserManager<AppUser>>();
        var mapper = InMemoryContextFactory.CreateMapper();
        var handler = new CreateUserCommandHandler(userManager, mapper);
        var command = new CreateUserCommand
        {
            Email = "test@example.com",
            Password = "Password123",
            CountryId = 1,
            ProvinceId = 1
        };

        // Act
        var resultDto = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(resultDto);
        Assert.Equal("test@example.com", resultDto.Email);

        var userInDb = await userManager.FindByEmailAsync("test@example.com");
        Assert.NotNull(userInDb);
        Assert.Equal("test@example.com", userInDb.Email);
        Assert.Equal(1, userInDb.CountryId);
        Assert.Equal(1, userInDb.ProvinceId);
    }

    [Fact]
    public async Task GetUserByIdQuery_Should_Return_User_When_Exists()
    {
        // Arrange
        var dbName = Guid.NewGuid().ToString();
        using var context = InMemoryContextFactory.CreateContext(dbName);

        var user = new AppUser
        {
            Email = "existing@example.com",
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
        var query = new GetUserByIdQuery(999); 

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}