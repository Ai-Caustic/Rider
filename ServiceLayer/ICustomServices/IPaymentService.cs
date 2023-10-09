using System;
using DomainLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.DTOS;

namespace ServiceLayer.ICustomServices
{
    public interface IPaymentService
    {
        Task<List<PaymentDTO>> GetAllPayments();

        Task<PaymentDTO> GetPaymentById(Guid Id);

        Task<bool> CreatePayment(PaymentDTO paymentDTO);

        Task<bool> UpdatePayment(Guid paymentId, PaymentDTO updatedPayment);

        Task<bool> DeletePayment(Guid paymentId);

        Task<List<PaymentDTO>> GetUserPayments(Guid userId);

        Task<List<PaymentDTO>> SearchPaymentsByMethod(string method);

        Task<List<PaymentDTO>> SearchPaymentsByAmount(string amount);

        //Task ConfirmPayment(Guid paymentId);
    }
}
