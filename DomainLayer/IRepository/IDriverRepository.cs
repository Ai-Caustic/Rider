﻿using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.IRepository
{
    public interface IDriverRepository
    {

        Task<List<Driver>> GetAllDrivers();

        Task<Driver> GetDriverById(Guid id);

        Task<Vehicle> GetVehicleById(Guid id);

        Task Insert(Driver driver);

        Task Update(Driver driver);

        Task Remove(Driver driver);

        Task AssignVehicle(Guid driverId, Guid vehicleId);

        Task UnassignVehicle(Guid driverId, Guid vehicleId);

        Task<List<Driver>> Search(string query);

        Task<List<Vehicle>> GetDriverVehicles(Guid driverId);

        Task<List<Ride>> GetDriverRides(Guid driverId);

        Task StartRide(Guid driverId, Guid rideId, Guid vehicleId);

        Task EndRide(Guid rideId);
    }
}
