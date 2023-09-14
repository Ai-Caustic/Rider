using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(Guid Id);

        Task Insert(User user);

        Task Update(User user);

        Task Delete(User user);

        void Remove(User user);

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
