using DataLayer.Data;
using DomainLayer.IRepository;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAllLocations()
        {
            return await _context.Locations.AsNoTracking().ToListAsync();
        }

        public async Task<Location> GetLocationById(Guid Id)
        {
            return await _context.Locations.SingleOrDefaultAsync(l => l.Id == Id);
        }

        public async Task Insert(Location location)
        {
            if(location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            await _context.AddAsync(location);
            await _context.SaveChangesAsync();  
        }

        public async Task Update(Location location)
        {
            if(location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            _context.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(Location location)
        {
            if(location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }
            _context.Remove(location);
            await _context.SaveChangesAsync();
        }
    }
}
