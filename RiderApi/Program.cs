using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using DomainLayer.IRepository;
using RepositoryLayer.Repository;
using ServiceLayer.ICustomServices;
using ServiceLayer.CustomServices;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TransferLayer.DTOS;
using static DomainLayer.Models.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Inject services
#region Service Injected
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IRideRepository, RideRepository>();
builder.Services.AddScoped<IRideService, RideService>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
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

#region MappingProfile
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>()
            .ForMember(dto => dto.RideIds, opt => opt.MapFrom(src => src.Rides.Select(r => r.Id).ToList()))
            .ForMember(dto => dto.PaymentIds, opt => opt.MapFrom(src => src.Payments.Select(p => p.Id).ToList()))
            .ReverseMap();
        CreateMap<Driver, DriverDTO>()
            .ForMember(dto => dto.VehicleIds, opt => opt.MapFrom(src => src.Vehicles.Select(v => v.Id).ToList()))
            .ForMember(dto => dto.RideIds, opt => opt.MapFrom(src => src.Rides.Select(r => r.Id).ToList()))
            .ReverseMap();
        CreateMap<VehicleDTO, Vehicle>();
        CreateMap<Vehicle, VehicleDTO>();
    }
}
#endregion
