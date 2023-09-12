using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepository
{
    public interface IDriverRepository <T> where T : Driver
    {
        IEnumerable<T> GetAll();

        T Get(Guid Id);

        void Insert(T driver);

        void Update(T driver);

        void Delete(T driver);

        void Remove(T driver);

        void SaveChanges();
    }
}
