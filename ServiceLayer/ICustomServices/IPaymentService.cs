using System;
using DomainLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IPaymentService
    {
        Task GetAllPayments();

        Task GetPaymentById(Guid Id);

        Task CreatePayment(Payment payment);

        Task UpdatePayment(Payment payment);

        Task SearchPaymentsByMethod(string method);

        Task SearchPaymentsByAmount(string amount);

        //Task ConfirmPayment(Guid paymentId);
    }
}
