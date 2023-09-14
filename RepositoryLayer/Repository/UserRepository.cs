using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using DomainLayer.IRepository;
using DataLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetUserById (Guid Id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == Id);
        }

        public async Task Insert (User user)
        {
            if (user == null)
            {
                throw new ArgumentException("user");
            }
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete (User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public void Remove(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            _context.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
