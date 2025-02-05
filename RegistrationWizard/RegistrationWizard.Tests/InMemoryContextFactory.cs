using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.BLL.Mapper;
using RegistrationWizard.DAL;

namespace RegistrationWizard.Tests;


public class InMemoryContextFactory
{
    public static RegistrationContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<RegistrationContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new RegistrationContext(options);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }
    public static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        return config.CreateMapper();
    }
}
