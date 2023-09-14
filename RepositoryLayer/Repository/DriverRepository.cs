using DataLayer.Data;
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
            //return await drivers.AsNoTracking().ToListAsync();
            return await _context.Drivers.AsNoTracking().ToListAsync();
        }

        public async Task<Driver> GetDriverById(Guid Id)
        {
            return await _context.Drivers.SingleOrDefaultAsync(d => d.Id == Id);
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

        public async Task Delete(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Driver driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }
            _context.Remove(driver);
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
