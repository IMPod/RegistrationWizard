using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RegistrationWizard.BLL.DTOs;
using RegistrationWizard.BLL.Commands;
using RegistrationWizard.DAL;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.BLL.Mapper;
using RegistrationWizard.Server.Controllers;

namespace RegistrationWizard.FunctionalTests
{
    public class RegistrationControllerFunctionalTests
    {
        [Fact]
        public async Task Register_UserSavedToDb_When_ValidData()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RegistrationContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
            })
            .AddEntityFrameworkStores<RegistrationContext>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(CreateUserCommand).Assembly
                );
            });

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
            var controller = new RegistrationController(mediator);

            var validUserRequest = new UserRequestDto
            {
                Email = "test@example.com",
                Password = "ZXasqw12!@",
                CountryId = 1,
                ProvinceId = 2
            };
            var cancellationToken = new CancellationToken();

            // Act
            var result = await controller.Register(validUserRequest, cancellationToken);

            // Assert 
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDto = Assert.IsType<RegisterPostResponseDTO>(okResult.Value);
            Assert.True(responseDto.Success);
            Assert.Equal("User registered successfully", responseDto.Message);

            var db = provider.GetRequiredService<RegistrationContext>();
            var userInDb = await db.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
            Assert.NotNull(userInDb);
            Assert.Equal("test@example.com", userInDb.Email);
            Assert.Equal(1, userInDb.CountryId);
            Assert.Equal(2, userInDb.ProvinceId);
        }
    }
}
