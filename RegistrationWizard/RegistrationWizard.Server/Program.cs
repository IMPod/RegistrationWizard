using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.BLL.Mapper;
using RegistrationWizard.DAL;
using RegistrationWizard.DAL.Models;
using RegistrationWizard.BLL;


var builder = WebApplication.CreateBuilder(args);

//if set only Assembly.GetExecutingAssembly() =>RegistrationWizard.BLL not loaded and MediatoR not work normal
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(MediatRCommandAssemblyMarker).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<RegistrationContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<RegistrationContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RegistrationContext>();
    db.Database.Migrate();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RegistrationWizard API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowClient");
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
