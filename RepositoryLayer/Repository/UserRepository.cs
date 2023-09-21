using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using DomainLayer.IRepository;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using DomainLayer.Enums;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                                 .Include(u => u.Rides)
                                 .Include(u => u.Payments)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task<User> GetUserById (Guid Id)
        {
            return await _context.Users
                                 .Include(u => u.Rides)
                                 .Include(u => u.Payments)
                                 .SingleOrDefaultAsync(u => u.Id == Id);
        }

        public async Task Insert (User user)
        {
            if (user == null)
            {
                throw new ArgumentException("user");
            }
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> Search(string query)
        {
            return await _context.Users
                                     .Where(u => u.Email.Contains(query) ||
                                     u.IdNumber.Equals(query) ||
                                     u.PhoneNumber.Equals(query))
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

        public async Task<List<Payment>> GetUserPayments(Guid userId)
        {
            return await _context.Payments
                                 .Where(p => p.UserId == userId)
                                 .AsNoTracking()
                                 .OrderBy(p => p.CreatedAt)
                                 .ToListAsync();
        }

        public async Task BookRide(Guid userId, Ride ride) //TODO: Work on a better system for Booking rides
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
            if(user != null)
            {
                Ride newRide = new Ride
                {
                    //TODO: Add other ride attributes
                    Status = RideStatus.Requested
                };
                user.Rides.Add(newRide);
                await _context.SaveChangesAsync(); 
            }
        }

        public async Task CancelRide(Guid rideId)
        {
            var ride = await _context.Rides.AsNoTracking().FirstOrDefaultAsync(r => r.Id == rideId);

            if (ride != null && ride.Status == RideStatus.Requested)
            {
                ride.Status = RideStatus.Completed;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Cannot find ride or ride has not been booked");
            }
        }
    }
}
