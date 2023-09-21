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

        Task Remove(Payment payment);

        Task<List<Payment>> GetUserPayments(Guid userId);

        Task<List<Payment>> QueryPaymentByAmount(string query);

        Task<List<Payment>> QueryPaymentByMethod(string query);
        
    }
}
