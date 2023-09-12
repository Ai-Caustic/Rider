using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using RepositoryLayer.IRepository;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Repository
{
    public class UserRepository <T> : IUserRepository<T> where T : User
    {
        #region property
        private readonly ApplicationDbContext _context;
        private DbSet<T> users;
        #endregion

        #region constructor
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
            users = _context.Set<T>();
        }
        #endregion

        public IEnumerable<T> GetAll()
        {
            return users.AsEnumerable();
        }

        public T Get (Guid Id)
        {
            return users.SingleOrDefault(u => u.Id == Id);
        }

        public void Insert (T user)
        {
            if (user == null)
            {
                throw new ArgumentException("user");
            }
            users.Add(user);
            _context.SaveChanges();
        }

        public void Update(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            users.Update(user);
            _context.SaveChanges();
        }

        public void Delete (T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            users.Remove(user);
            _context.SaveChanges();
        }

        public void Remove(T user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            users.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
