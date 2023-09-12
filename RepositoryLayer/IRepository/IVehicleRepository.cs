using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IVehicleRepository <T> where T : Vehicle
    {
        IEnumerable<T> GetAll ();

        T Get(Guid Id);

        void Insert (T vehicle);

        void Update (T vehicle);

        void Delete (T vehicle);

        void Remove (T vehicle);

        void SaveChanges();

    }
}
