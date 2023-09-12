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
    public class RideRepository <T> : IRideRepository <T> where T : Ride
    {
        #region property
        private readonly ApplicationDbContext _context;

        private DbSet<T> rides;
        #endregion


        #region constructor
        public RideRepository (ApplicationDbContext context)
        {
            _context = context;
            rides = _context.Set<T>();
        }
        #endregion

        public IEnumerable<T> GetAll()
        {
            return rides.AsEnumerable();
        }

        public T Get (Guid Id)
        {
            return rides.SingleOrDefault(r => r.Id == Id);
        }

        public void Insert(T ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }
            rides.Add(ride);
            _context.SaveChanges();
        }

        public void Update(T ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }
            rides.Update(ride);
            _context.SaveChanges();
        }

        public void Delete(T ride)
        {
           if (ride == null)
           {
               throw new ArgumentNullException(nameof(ride));
           }
           rides.Remove(ride);
            _context.SaveChanges();
        }

        public void Remove(T ride)
        {
            if (ride == null)
            {
                throw new ArgumentNullException(nameof(ride));
            }
            rides.Remove(ride);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
