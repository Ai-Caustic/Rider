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
using AutoMapper;
using Microsoft.Extensions.Logging;
using TransferLayer.DTOS;

namespace RepositoryLayer.Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly ILogger<RideRepository> _logger;

        public RideRepository (ApplicationDbContext context, IMapper mapper, ILogger<RideRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
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
            try
            {
                if(ride != null)
                {
                    bool rideExists = await _context.Rides.AnyAsync(r => r.Id == ride.Id);
                    if (rideExists)
                    {
                        _logger.LogError("Ride already exists");
                    }
                    await _context.Rides.AddAsync(ride);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Problem with inserting ride");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Update(Guid rideId, Ride updatedRide)
        {
            try
            {
                var ride = await _context.Rides.SingleOrDefaultAsync(r => r.Id == rideId);
                if(ride != null)
                {
                    ride.Id = ride.Id;
                    ride.PickupLocation = updatedRide.PickupLocation;
                    ride.Destination = updatedRide.Destination;
                    ride.StartTime = updatedRide.StartTime;
                    ride.EndTime = updatedRide.EndTime;
                    ride.RideFare = updatedRide.RideFare;
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

        public async Task Remove(Guid rideId)
        {
            try
            {
                var ride = await _context.Rides.SingleOrDefaultAsync(r => r.Id == rideId);
                if (ride != null)
                {
                    _context.Rides.Remove(ride);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Ride not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task CancelRide(Guid rideId)
        {
            try
            {
                var ride = await _context.Rides.SingleOrDefaultAsync(r => r.Id == rideId);
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
                        _logger.LogError("Problem with cancelling ride");
                    }
                }
                else
                {
                    _logger.LogError("Ride not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public Ride MapRideDTO(RideDTO rideDTO)
        {
            return _mapper.Map<Ride>(rideDTO);
        }

        public async Task<List<Ride>> GetDriverRides(Guid driverId)
        {
            try
            {
                bool driverExists = await _context.Drivers.AnyAsync(d => d.Id == driverId);
                if (driverExists)
                {
                    return await _context.Rides
                          .Where(r => r.DriverId == driverId)
                          .AsNoTracking()
                          .OrderBy(r => r.CreatedAt)
                          .ToListAsync();
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
            return null;
        }

        public async Task<List<Ride>> GetUserRides(Guid userId)
        {
            try
            {
                bool userExists = await _context.Users.AnyAsync(u => u.Id == userId);
                if(userExists)
                { 
                    return await _context.Rides
                                    .Where(r => r.UserId == userId)
                                    .AsNoTracking()
                                    .OrderBy(r => r.CreatedAt)
                                    .ToListAsync();
                }
                else
                {
                    _logger.LogError("User not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return null;
        }

    }
}
