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
    public class DriverRepository<T> : IDriverRepository<T> where T : Driver
    {
        #region property
        private readonly ApplicationDbContext _context;
        private DbSet<T> drivers;
        #endregion

        #region constructor
        public DriverRepository(ApplicationDbContext context)
        {
            _context = context;
            drivers = _context.Set<T>();
        }
        #endregion

        public IEnumerable<T> GetAll()
        {
            return drivers.AsEnumerable();
        }

        public T Get(Guid Id)
        {
            return drivers.SingleOrDefault(d => d.Id == Id);
        }

        public void Insert(T driver)
        {
            if (driver == null)
            {
               throw new ArgumentNullException("driver");
            }
            drivers.Add(driver);
            _context.SaveChanges();
        }

        public void Update(T driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }
            drivers.Update(driver);
            _context.SaveChanges();
        }

        public void Delete(T driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }
            drivers.Remove(driver);
            _context.SaveChanges();
        }

        public void Remove(T driver)
        {
            if (driver == null)
            {
                throw new ArgumentNullException("driver");
            }
            drivers.Remove(driver);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
