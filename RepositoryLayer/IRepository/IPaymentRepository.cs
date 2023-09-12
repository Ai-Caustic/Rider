using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IPaymentRepository <T> where T : Payment
    {
        IEnumerable<T> GetAll();

        T Get(Guid Id);

        void Insert(T payment);

        void Update(T payment);

        void Delete(T payment);

        void Remove(T payment);

        void SaveChanges();
    }
}
