using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using DomainLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
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
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Remove(vehicle);
            await _context.SaveChangesAsync();
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
