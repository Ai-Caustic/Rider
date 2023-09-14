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
    public class RideRepository : IRideRepository
    {
        private readonly ApplicationDbContext _context;

        public RideRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ride>> GetAllRides()
        {
            return await _context.Rides.AsNoTracking().ToListAsync();
        }

        public async Task<Ride> GetRideById (Guid Id)
        {
            return await _context.Rides.SingleOrDefaultAsync(r => r.Id == Id);
        }

        public async Task Insert(Ride ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }
            _context.Add(ride);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Ride ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }
            _context.Update(ride);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Ride ride)
        {
           if (ride == null)
           {
               throw new ArgumentNullException(nameof(ride));
           }
           _context.Remove(ride);
           await _context.SaveChangesAsync();
        }

        public void Remove(Ride ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }
            _context.Remove(ride);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
