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
using TransferLayer.DTOS;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly ILogger<UserRepository> _logger;

        public UserRepository(ApplicationDbContext context, IMapper mapper, ILogger<UserRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
            try
            {
                if (user != null)
                {
                    bool userExists = await _context.Users.AnyAsync(u => u.Email == user.Email);
                    if (userExists)
                    {
                        _logger.LogError("User already exists");
                        return;
                    }
                    else
                    {
                        //create the User using the User class's Create method
                        User newUser = User.Create(user.Email, user.UserName, user.Mobile, user.IdNumber, user.IdPhotoUrl, user.ProfilePhotoUrl, user.BirthDate, user.Role, user.Gender, user.IsActive);
                        await _context.Users.AddAsync(newUser);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    _logger.LogError("User is null");
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            
        }

        public async Task Update(Guid userId, User updatedUser) //TODO: Work on this update method until its sharp
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {

                    // Update all properties from updatedUser
                    user.Id = user.Id;
                    user.Email = updatedUser.Email;
                    user.UserName = updatedUser.UserName;
                    user.Mobile = updatedUser.Mobile;
                    user.IdNumber = updatedUser.IdNumber;
                    user.IdPhotoUrl = updatedUser.IdPhotoUrl;
                    user.ProfilePhotoUrl = updatedUser.ProfilePhotoUrl;
                    user.Gender = updatedUser.Gender;
                    user.BirthDate = updatedUser.BirthDate;
                    user.Role = updatedUser.Role;
                    user.IsActive = updatedUser.IsActive;
                    user.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("User not found");
                }
            }
            catch (DbUpdateException ex)
            {
               _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Remove(Guid userId)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("User not found");
                }
            }
            catch (Exception ex)
            {
               _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public User MapUserDTO(UserDTO userDTO)
        {
            return _mapper.Map<User>(userDTO);
        }


        public async Task<List<User>> Search(string query)
        {
            return await _context.Users
                                     .Where(u => u.Email.Contains(query) ||
                                     u.IdNumber.Equals(query) ||
                                     u.Mobile.Equals(query))
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

        //public async Task BookRide(Guid userId, Ride ride) //TODO: Work on a better system for Booking rides
        //{
        //    var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        //    if(user != null)
        //    {
        //        Ride newRide = new Ride
        //        {
        //            //TODO: Add other ride attributes
        //            Status = RideStatus.Requested
        //        };
        //        user.Rides.Add(newRide);
        //        await _context.SaveChangesAsync(); 
        //    }
        //}

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
