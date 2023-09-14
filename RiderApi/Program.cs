using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repository;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;
using ServiceLayer.CustomServices;
//using ServiceLayer.IDriverService;
//using ServiceLayer.DriverService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
    b => b.MigrationsAssembly("DataLayer")
    ));
#region Service Injected
//builder.Services.AddScoped(typeof(IDriverService), typeof(Repository<>));
builder.Services.AddScoped<IDriverService, DriverService>(); //This is the way to inject services
builder.Services.AddScoped< ICustomService <Location>, LocationService >();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
