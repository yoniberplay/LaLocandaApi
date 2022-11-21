using LaLocanda.Core.Application;
using LaLocanda.Infrastructure.Identity;
using LaLocanda.Infrastructure.Identity.Entities;
using LaLocanda.Infrastructure.Identity.Seeds;
using LaLocanda.Infrastructure.Persistence;
using LaLocanda.Infrastucture.Identity.Seeds;
using LaLocanda.Presentation.WebApi.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddHealthChecks();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var s = scope.ServiceProvider;

    try
    {
        var userM = s.GetRequiredService<UserManager<ApplicationUser>>();
        var roleM = s.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userM, roleM);
        await DefaultSuperAdminUser.SeedAsync(userM, roleM);
        await DefaultAdminUser.SeedAsync(userM, roleM);
        await DefaultBasicUser.SeedAsync(userM, roleM);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error executing seeds: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks("/health");
app.UseSwaggerExtension();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.Run();
