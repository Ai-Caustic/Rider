using DataLayer.Data;
using DomainLayer.Enums;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.IRepository;
using Microsoft.Extensions.Logging;
using TransferLayer.DTOS;
using AutoMapper;

namespace RepositoryLayer.Repository
{
    public class DriverRepository : IDriverRepository
    {

        private readonly ApplicationDbContext _context;

        private readonly ILogger<DriverRepository> _logger;

        private readonly IMapper _mapper;

        public DriverRepository(ApplicationDbContext context, ILogger<DriverRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Driver>> GetAllDrivers()
        {
            return await _context.Drivers
                                 .Include(d => d.Vehicles)
                                 .Include(d => d.Rides)
                                 .AsNoTracking()
                                 .ToListAsync(); 
        }

        public async Task<Driver> GetDriverById(Guid Id)
        {
            return await _context.Drivers
                                 .Include(d => d.Vehicles)
                                 .Include(d => d.Rides)
                                 .SingleOrDefaultAsync(d => d.Id == Id);
        }

        public async Task<Vehicle> GetVehicleById(Guid Id)
        {
            return await _context.Vehicles
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync(v => v.Id == Id);
        }

        public async Task Insert(Driver driver)
        {
            try
            {
                if (driver != null)
                {
                    bool driverExists = await _context.Drivers.AnyAsync(d => d.Email == driver.Email);
                    if (driverExists)
                    {
                        _logger.LogError("Driver with the same email already exists");
                    }
                    else
                    {
                        await _context.AddAsync(driver); //TODO: Use create method instead
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    _logger.LogError("Driver cannot be null");
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Update(Guid driverId, Driver updatedDriver)
        {
            try
            {
                var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == driverId);
                if (driver != null)
                {
                    driver.Id = driver.Id;
                    driver.FirstName = updatedDriver.FirstName;
                    driver.LastName = updatedDriver.LastName;
                    driver.Email = updatedDriver.Email;
                    driver.Mobile = updatedDriver.Mobile;
                    driver.DriverPhotoUrl = updatedDriver.DriverPhotoUrl;
                    driver.LicensePhotoUrl = updatedDriver.LicensePhotoUrl;
                    driver.VerificationBadgeUrl = updatedDriver.VerificationBadgeUrl;
                    driver.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Driver not found");
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Remove(Guid driverId)
        {
            try
            {
                var driver = await _context.Drivers.SingleOrDefaultAsync(d => d.Id == driverId);
                if(driver != null)
                {
                    _context.Drivers.Remove(driver);
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

        public Driver MapDriverDTO(DriverDTO driverDTO)
        {
            return _mapper.Map<Driver>(driverDTO);
        }

        public async Task AssignVehicle(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
                var vehicle = await _context.Vehicles.FirstOrDefaultAsync(d => d.Id == vehicleId);

                if (driver != null && vehicle != null)
                {
                    if(vehicle.DriverId == null)
                    {
                        vehicle.DriverId = driverId;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Vehicle already has a driver");
                    }
                }
                else
                {
                    _logger.LogError("Vehicle or driver not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task UnassignVehicle(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver = await _context.Drivers.FirstOrDefaultAsync(d => d.Id == driverId);
                var vehicle = await _context.Vehicles.FirstOrDefaultAsync(d => d.Id == vehicleId);

                if(driver != null && vehicle != null)
                {
                    if (vehicle.DriverId == driverId)
                    {
                        _context.Entry(vehicle).Property("DriverId").CurrentValue = null;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new InvalidOperationException("The driver is not assigned to this vehicle");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task<List<Driver>> Search(string query)
        {
            return await _context.Drivers
                                       .Where(d => d.FirstName.Contains(query) ||
                                        d.LastName.Contains(query) ||
                                        d.Email.Contains(query) ||
                                        d.Equals(query))
                                        .ToListAsync();
        }

        public async Task<List<Vehicle>> GetDriverVehicles(Guid driverId)
        {
            return await _context.Vehicles
                                 .Where(v => v.DriverId == driverId)
                                 .ToListAsync();
        }

        public async Task<List<Ride>> GetDriverRides(Guid driverId)
        {
            return await _context.Rides
                                 .Where(r => r.DriverId == driverId)
                                 .OrderBy(r => r.CreatedAt)
                                 .ToListAsync();
        }

        public async Task StartRide(Guid driverId, Guid rideId, Guid vehicleId)
        {
            try
            {
                var driver = await _context.Drivers.AsNoTracking().FirstOrDefaultAsync(d => d.Id == driverId);
                var ride = await _context.Rides.AsNoTracking().FirstOrDefaultAsync(r => r.Id == rideId);
                var vehicle = await _context.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.Id == vehicleId);

                if (driver != null && ride != null && vehicle != null)
                {
                    if( ride.DriverId == null && ride.Status == RideStatus.Requested)
                    {
                        ride.DriverId = driverId;
                        ride.VehicleId = vehicleId;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new ArgumentException("Cannot find driver or ride");
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task EndRide(Guid rideId)
        {
            try
            {
            var ride = await _context.Rides.AsNoTracking().FirstOrDefaultAsync(r => r.Id == rideId);

            if (ride != null && ride.Status == RideStatus.InProgress)
            {
                ride.Status = RideStatus.Completed;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Cannot find ride or ride is not in progress");
            }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }
    }
}
