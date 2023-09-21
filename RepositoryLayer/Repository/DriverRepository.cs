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

namespace RepositoryLayer.Repository
{
    public class DriverRepository : IDriverRepository
    {

        private readonly ApplicationDbContext _context;

        public DriverRepository(ApplicationDbContext context)
        {
            _context = context;
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
            if (driver == null)
            {
               throw new ArgumentNullException("driver");
            }
            await _context.AddAsync(driver);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }
            _context.Update(driver);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }
            _context.Remove(driver);
            await _context.SaveChangesAsync();
        }

        public async Task AssignVehicle(Guid driverId, Guid vehicleId)
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
        }

        public async Task UnassignVehicle(Guid driverId, Guid vehicleId)
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
            var driver = await _context.Drivers.AsNoTracking().FirstOrDefaultAsync(d => d.Id == driverId);
            var ride = await _context.Rides.AsNoTracking().FirstOrDefaultAsync(r => r.Id == rideId);
            var vehicle = await _context.Vehicles.AsNoTracking().FirstOrDefaultAsync(v => v.Id == vehicleId);

            if (driver != null && ride != null && vehicle != null)
            {
                if( ride.DriverId == null && ride.Status == RideStatus.Requested)
                {
                    ride.DriverId = driverId;
                    ride.VehicleId = vehicleId;
                    //await _context.Rides.Update(ride); TODO: Confirm if I should update context
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Cannot find driver or ride");
                }
            }
        }

        public async Task EndRide(Guid rideId)
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

        //TODO: Implement method GetRides and GetVehicles eg _context.Drivers.Include(x => x.Rides) 
    }
}
