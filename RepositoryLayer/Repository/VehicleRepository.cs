using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using DomainLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.DTOS;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly ILogger<VehicleRepository> _logger;

        public VehicleRepository(ApplicationDbContext context, IMapper mapper, ILogger<VehicleRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _context.Vehicles.AsNoTracking().ToListAsync();
        }

        public async Task<Vehicle> GetVehicleById(Guid Id)
        {
            return await _context.Vehicles.SingleOrDefaultAsync(v => v.Id == Id);
        }

        public async Task Insert(Vehicle vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    bool vehicleExists = await _context.Vehicles.AnyAsync(v => v.Id == vehicle.Id);
                    if (vehicleExists)
                    {
                        _logger.LogError("Vehicle already exists");
                    }
                    else
                    {
                       Vehicle newVehicle = Vehicle.Create(vehicle.DriverId, vehicle.Model, vehicle.Color, vehicle.NumberOfSeats, vehicle.LicensePlate, vehicle.VehiclePhotoUrl, vehicle.InsurancePhotoUrl);
                       await _context.Vehicles.AddAsync(newVehicle);
                       await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    _logger.LogError("Driver is null");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exeption: {ex.InnerException}");
            }
        }

        public async Task Update(Guid vehicleId, Vehicle updatedVehicle)
        {
            try
            {
                var vehicle = await _context.Vehicles.SingleOrDefaultAsync(d => d.Id == vehicleId);
                if (vehicle != null)
                {
                    vehicle.Id = vehicle.Id;
                    vehicle.DriverId = updatedVehicle.DriverId;
                    vehicle.Model = updatedVehicle.Model;
                    vehicle.Color = updatedVehicle.Color;
                    vehicle.NumberOfSeats = updatedVehicle.NumberOfSeats;
                    vehicle.VehiclePhotoUrl = updatedVehicle.VehiclePhotoUrl;
                    vehicle.InsurancePhotoUrl = updatedVehicle.InsurancePhotoUrl;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Vehicle not found");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Remove(Guid vehicleId)
        {
            try
            {
                var vehicle = await _context.Vehicles.SingleOrDefaultAsync(d => d.Id == vehicleId);
                if(vehicle != null)
                {
                    _context.Vehicles.Remove(vehicle);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Driver not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public Vehicle MapVehicleDTO(VehicleDTO vehicleDTO)
        {
            return _mapper.Map<Vehicle>(vehicleDTO); 
        }

        public async Task<Vehicle> SearchPlate(string plateNo)
        {
            return await _context.Vehicles
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(v => v.LicensePlate == plateNo);
        }

        public async Task<List<Vehicle>> QueryVehicles(string query)
        {
            return await _context.Vehicles
                                   .AsNoTracking()
                                   .Where(v => v.Model.Contains(query) || v.LicensePlate.Contains(query))
                                   .ToListAsync(); 
        }

        public async Task<List<Vehicle>> SearchVehicleBySeats(int seatsNo)
        {
            return await _context.Vehicles
                                    .AsNoTracking()
                                    .Where(v => v.NumberOfSeats.Equals(seatsNo))
                                    .ToListAsync();
        }


        public async Task<Driver> GetVehicleDriver(Guid driverId)
        {
            return await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
        }

        public async Task<List<Ride>> GetVehicleRides(Guid vehicleId)
        {
            return await _context.Rides
                                 .Where(r => r.VehicleId == vehicleId)
                                 .OrderBy(r => r.CreatedAt)
                                 .ToListAsync();
        }
    }
}
