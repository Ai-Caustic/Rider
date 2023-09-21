using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using DomainLayer.IRepository;
using DomainLayer.Enums;
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

        public async Task Remove(Ride ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }
            _context.Remove(ride);
            await _context.SaveChangesAsync();
        }

        public async Task CancelRide(Guid rideId)
        {
                var ride = await _context.Rides.FirstOrDefaultAsync(r => r.Id == rideId);
                var rideStatus = ride.Status;

                if(ride != null)
                {
                    if(rideStatus == RideStatus.InProgress)
                    {
                        rideStatus = RideStatus.Canceled;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Problem with completing ride");
                    }
                }
        }

        public async Task<List<Ride>> GetDriverRides(Guid driverId)
        {
            return await _context.Rides
                          .Where(r => r.DriverId == driverId)
                          .AsNoTracking()
                          .OrderBy(r => r.CreatedAt)
                          .ToListAsync();
        }

        public async Task<List<Ride>> GetUserRides(Guid userId)
        {
            return await _context.Rides
                                 .Where(r => r.UserId == userId)
                                 .AsNoTracking()
                                 .OrderBy(r => r.CreatedAt)
                                 .ToListAsync();
        }

    }
}
