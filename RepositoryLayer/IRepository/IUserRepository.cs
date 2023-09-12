using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace RepositoryLayer.IRepository
{
    public interface IUserRepository <T> where T : User
    {
        IEnumerable<T> GetAll ();

        T Get(Guid Id);

        void Insert(T user);

        void Update(T user);

        void Delete(T user);

        void Remove (T user);

        void SaveChanges();
    }
}
