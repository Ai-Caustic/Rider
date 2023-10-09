using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using DomainLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TransferLayer.DTOS;

namespace RepositoryLayer.Repository
{
    public class PaymentRepository : IPaymentRepository 
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<PaymentRepository> _logger;

        private readonly IMapper _mapper;

        public PaymentRepository(ApplicationDbContext context, ILogger<PaymentRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Payment>> GetAllPayments()
        {
            return await _context.Payments.AsNoTracking().ToListAsync();
        }

        public async Task<Payment> GetPaymentById(Guid Id)
        {
            return await _context.Payments.SingleOrDefaultAsync(p => p.Id == Id);
        }

        public async Task Insert(Payment payment)
        {
           try
           {
                if(payment != null)
                {
                    bool paymentExists = await _context.Payments.AnyAsync(p => p.Id == payment.Id);
                    if(paymentExists)
                    {
                        _logger.LogError("Payment with similar Id exists");
                    }
                    else
                    {
                        await _context.Payments.AddAsync(payment);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    _logger.LogError("Payment cannot be null");
                }
           }
           catch (Exception ex)
           {
                _logger.LogError($"Error {ex.Message} Exception: {ex.InnerException}");
           }
        }

        public async Task Update(Guid paymentId,Payment updatedPayment)
        {
            try
            {
                var payment = await _context.Payments.SingleOrDefaultAsync(p => p.Id == paymentId);
                if(payment != null)
                {
                    payment.Id = payment.Id;
                    payment.PaymentAmount = updatedPayment.PaymentAmount;
                    payment.PaymentMethod = updatedPayment.PaymentMethod;
                    payment.TimeStamp = updatedPayment.TimeStamp;
                    payment.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Payment not found");
                }
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError($"Error {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Remove(Guid paymentId)
        {
            try
            {
                var payment = await _context.Payments.SingleOrDefaultAsync(p => p.Id == paymentId);
                if(payment != null)
                {
                    _context.Payments.Remove(payment);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Payment not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public Payment MapPaymentDTO(PaymentDTO paymentDTO)
        {
            return _mapper.Map<Payment>(paymentDTO);
        }

        public async Task<List<Payment>> GetUserPayments(Guid userId)
        {
            return await _context.Payments
                          .Where(u => u.UserId == userId)
                          .AsNoTracking()
                          .ToListAsync();  
        }
        
        public async Task<List<Payment>> QueryPaymentByAmount(string query)
        {
            //We assume the query is an amount
            var amount = float.Parse(query);

            return await _context.Payments
                                        .Where(p => p.PaymentAmount.Equals(amount))
                                        .AsNoTracking()
                                        .ToListAsync();
        }

        public async Task<List<Payment>> QueryPaymentByMethod(string query)
        {
            return await _context.Payments
                                        .Where(p => p.PaymentMethod.Contains(query))
                                        .AsNoTracking()
                                        .ToListAsync();
        }
    }
}
