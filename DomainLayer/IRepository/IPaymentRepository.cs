using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using TransferLayer.DTOS;

namespace DomainLayer.IRepository
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllPayments();

        Task<Payment> GetPaymentById(Guid id);

        Task Insert(Payment payment);
        
        Task Update(Guid paymentId, Payment updatedPayment);

        Task Remove(Guid paymentId);

        Payment MapPaymentDTO(PaymentDTO paymentDTO);

        Task<List<Payment>> GetUserPayments(Guid userId);

        Task<List<Payment>> QueryPaymentByAmount(string query);

        Task<List<Payment>> QueryPaymentByMethod(string query);
        
    }
}
