using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.IRepository
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPayments();

        Task<Payment> GetPaymentById(Guid id);

        Task Insert(Payment payment);

        Task Update(Payment payment);

        Task Delete(Payment payment);

        void Remove(Payment payment);

        void SaveChanges();

        Task SaveChangesAsync();
        
    }
}
