using Microsoft.EntityFrameworkCore;
using TrainComponentManagement.Api.Middleware;
using TrainComponentManagement.Application.Interfaces;
using TrainComponentManagement.Application.Services;
using TrainComponentManagement.Domain.Interfaces;
using TrainComponentManagement.Infrastructure.Data;
using TrainComponentManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IComponentRepository, ComponentRepository>();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // for dev
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
