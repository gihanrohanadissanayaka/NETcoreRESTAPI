using Microsoft.EntityFrameworkCore;
using myRestApiApp.Data;
using myRestApiApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IModuleService, ModuleService>(); // Register ModuleService
builder.Services.AddScoped<IValidationService, ModuleValidationService>();
builder.Services.AddScoped<IStudySessionService, StudySessionService>(); // Register StudySessionService
builder.Services.AddScoped<IStudySessionValidationService, StudySessionValidationService>();
builder.Services.AddScoped<IFilterResultService, FilterResultService>();    // Register FilterResultService

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = string.Empty; // Sets Swagger UI at the app's root (base URL)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
