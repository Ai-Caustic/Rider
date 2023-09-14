using DomainLayer.Models;
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

        Task Insert(Driver driver);

        Task Update(Driver driver);

        Task Delete(Driver driver);

        Task Remove(Driver driver);

        void SaveChanges();

        Task SaveChangesAsync();

        DbSet<Driver> Drivers { get; set; }

        DbSet<Vehicle> Vehicles { get; set; }

        DbSet<Ride> Rides { get; set; }
    }
}
