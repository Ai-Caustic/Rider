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

        public async Task Delete(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Remove(vehicle);
            await _context.SaveChangesAsync();
        }

        public void Remove(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            _context.Remove(vehicle);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public DbSet<Driver> Drivers
        {
            get
            {
                return _context.Drivers;
            }
            set
            {
                _context.Drivers = value;
            }
        }

        public DbSet<Vehicle> Vehicles
        {
            get
            {
                return _context.Vehicles;
            }
            set
            {
                _context.Vehicles = value;
            }
        }

        public DbSet<Ride> Rides
        {
            get
            {
                return _context.Rides;
            }
            set
            {
                _context.Rides = value;
            }
        }
    }
}
