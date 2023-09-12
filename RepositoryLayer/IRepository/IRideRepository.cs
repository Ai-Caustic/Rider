using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IRideRepository <T> where T : Ride
    {
        IEnumerable<T> GetAll();

        T Get(Guid Id);

        void Insert(T ride);

        void Update(T ride);

        void Delete(T ride);

        void Remove(T ride);

        void SaveChanges();

    }
}
