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
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            try
            {
                var obj = await _vehicleRepository.GetAllVehicles();
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

        public async Task<Vehicle> GetVehicleById(Guid Id)
        {
            try
            {
                var obj = await _vehicleRepository.GetVehicleById(Id);
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

        public async Task<Vehicle> GetVehicleByPlate(string licensePlate)
        {
            try
            {
                var vehicle = await _vehicleRepository.Vehicles
                                      .FirstOrDefaultAsync(v => v.LicensePlate == licensePlate);

                return vehicle;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {
            try
            {
                if(vehicle != null)
                {
                    await _vehicleRepository.Insert(vehicle);
                    await _vehicleRepository.SaveChangesAsync();   
                }
                else
                {
                    throw new ArgumentNullException(nameof(vehicle));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    await _vehicleRepository.Update(vehicle);
                    await _vehicleRepository.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(vehicle));
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle != null)
                {
                    await _vehicleRepository.Delete(vehicle);
                    await _vehicleRepository.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentNullException(nameof(vehicle));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Vehicle>> SearchVehicles(string searchItem)
        {
            try
            {
                if (searchItem == null)
                {
                    return await _vehicleRepository.Vehicles
                                   .Where(v => v.Model.Contains(searchItem) || v.LicensePlate.Contains(searchItem))
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

        public async Task<List<Driver>> GetVehicleDriver(Guid vehicleId)
        {
            try
            {
                var vehicle = await _vehicleRepository.Vehicles.Include(v => v.Driver).FirstOrDefaultAsync(v => v.Id == vehicleId);

                return vehicle?.Driver != null ? new List<Driver> { vehicle.Driver } : new List<Driver>();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<Ride>> GetVehicleRideHistory(Guid vehicleId)
        {
            try
            {
                var rides = await _vehicleRepository.Rides
                                    .Where(r => r.VehicleId == vehicleId)
                                    .AsNoTracking()
                                    .ToListAsync();

                return rides;
            }
            catch(Exception)
            {
                throw;
            }
        }


    }
}