using DomainLayer.Models;
using ServiceLayer.ICustomServices;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Repository;
using Microsoft.EntityFrameworkCore;
using DomainLayer.IRepository;

namespace ServiceLayer.CustomServices
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;


        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;

        }

        public async Task<List<Driver>> GetAllDrivers()
        {
            try
            {
                var obj =  await _driverRepository.GetAllDrivers();
                if (obj != null)
                {
                    return obj;
                }    
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Driver> GetDriverByID(Guid Id)
        {
            try
            {
                var obj = await _driverRepository.GetDriverById(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task CreateDriver(Driver driver)
        {
            try
            {
                if (driver != null)
                {
                    await _driverRepository.Insert(driver);
                    await _driverRepository.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(driver));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateDriver(Driver driver)
        {
            try
            {
                if (driver != null)
                {
                    await _driverRepository.Update(driver);
                    await _driverRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteDriver(Driver driver)
        {
            try
            {
                if (driver != null)
                {
                    await _driverRepository.Delete(driver);
                    await _driverRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //start here
        public async Task AssignVehicleToDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver =  await _driverRepository.Drivers.FindAsync(driverId);
                var vehicle = await _driverRepository.Vehicles.FindAsync(vehicleId);
                if (driver != null && vehicle != null)
                {
                    await _driverRepository.Vehicles.AddAsync(vehicle); //TODO
                    await _driverRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RemoveVehicleFromDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver = await _driverRepository.Drivers.FindAsync(driverId);
                var vehicle = await _driverRepository.Vehicles.FindAsync(vehicleId);

                if (driver != null && vehicle != null)
                {
                    //driver.Vehicles.Remove(vehicle);
                    _driverRepository.Vehicles.Remove(vehicle);
                    await _driverRepository.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Driver>> SearchDriver(string searchItem)
        {
            try
            {
                if (searchItem != null)
                {
                    return await _driverRepository.Drivers
                                            .Where(d => d.FirstName.Contains(searchItem) || d.LastName.Contains(searchItem) || d.Email.Contains(searchItem))
                                            .AsNoTracking()
                                            .ToListAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(searchItem));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Vehicle>> GetDriverVehicles(Guid driverId)
        {
            try
            {
                var driver =  await _driverRepository.Drivers
                                     .Include(d => d.Vehicles)
                                     .FirstOrDefaultAsync(d => d.Id == driverId);

                return driver.Vehicles.ToList() ?? new List<Vehicle>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Ride>> GetDriverRideHistory(Guid driverId)
        {
            try
            {
                var driverRides = await _driverRepository.Rides
                                          .Where(r => r.DriverId == driverId)
                                          .ToListAsync();

                return driverRides;
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}