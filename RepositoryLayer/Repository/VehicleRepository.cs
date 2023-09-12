using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class VehicleRepository <T> : IVehicleRepository <T> where T : Vehicle
    {
        #region property
        private readonly ApplicationDbContext _context;

        private DbSet<T> vehicles;
        #endregion

        #region Constructor
        public VehicleRepository (ApplicationDbContext context)
        {
            _context = context;
            vehicles = _context.Set<T>();
        }
        #endregion

        public IEnumerable<T> GetAll()
        {
            return vehicles.AsEnumerable();
        }

        public T Get(Guid Id)
        {
            return vehicles.SingleOrDefault(v => v.Id == Id);
        }

        public void Insert(T vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public void Update(T vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            vehicles.Update(vehicle);
            _context.SaveChanges();
        }

        public void Delete(T vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            vehicles.Remove(vehicle);
            _context.SaveChanges();
        }

        public void Remove(T vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }
            vehicles.Remove(vehicle);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
