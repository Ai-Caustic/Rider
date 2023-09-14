using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.IRepository
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllVehicles();

        Task<Vehicle> GetVehicleById(Guid Id);

        Task Insert(Vehicle vehicle);

        Task Update(Vehicle vehicle);

        Task Delete(Vehicle vehicle);

        void Remove(Vehicle vehicle);

        void SaveChanges();

        Task SaveChangesAsync();

        DbSet<Vehicle> Vehicles { get; set; }

        DbSet<Driver> Drivers { get; set; }

        DbSet<Ride> Rides { get; set; }

    }
}
