using DomainLayer.Models;
using ServiceLayer.ICustomServices;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.IRepository;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TransferLayer.DTOS;
using Microsoft.Identity.Client;

namespace ServiceLayer.CustomServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        private readonly ILogger<PaymentService> _logger;

        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, ILogger<PaymentService> logger, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<PaymentDTO>> GetAllPayments()
        {
            try
            {
                var payments = await _paymentRepository.GetAllPayments();
                if(payments != null)
                {
                    var result = _mapper.Map<List<PaymentDTO>>(payments);

                    return result;
                }
                else
                {
                    _logger.LogError("Payments not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<PaymentDTO> GetPaymentById(Guid Id)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentById(Id);
                if (payment == null)
                {
                    _logger.LogError("Could not find payment");
                }
                else
                {
                    var result = _mapper.Map<PaymentDTO>(payment);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<bool> CreatePayment(PaymentDTO paymentDTO)
        {
            try
            {
                if(paymentDTO == null)
                {
                    _logger.LogError("Payment cannot be null");
                    return false;
                }
                else
                {
                    var payment = _paymentRepository.MapPaymentDTO(paymentDTO);
                    await _paymentRepository.Insert(payment);
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UpdatePayment(Guid paymentId, PaymentDTO updatedPayment)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentById(paymentId);
                if(payment == null)
                {
                    _logger.LogError("Payment not found");
                    return false;
                }
                else
                {
                    var mappedPayment = _paymentRepository.MapPaymentDTO(updatedPayment);
                    await _paymentRepository.Update(paymentId,mappedPayment);
                    _logger.LogInformation("Updated payment");
                    return true;
                }

            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> DeletePayment(Guid paymentId)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentById(paymentId);
                if(payment == null)
                {
                    _logger.LogError("Payment not found");
                    return false;
                }
                else
                {
                    await _paymentRepository.Remove(paymentId);
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<List<PaymentDTO>> GetUserPayments(Guid userId)
        {
            try
            {
                var results = await _paymentRepository.GetUserPayments(userId);
                if(results == null)
                {
                    _logger.LogError($"No payments found for user {userId}");
                }
                else
                {
                    var payments = _mapper.Map<List<PaymentDTO>>(results);
                    return payments;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}]");
            }
            return null;
        }

        public async Task<List<PaymentDTO>> SearchPaymentsByMethod(string method)
        {
            try
            {
                var results = await _paymentRepository.QueryPaymentByMethod(method);
                if(results == null)
                {
                    _logger.LogError($"Could not get payments by method {method}");
                }
                else
                {
                    var payments = _mapper.Map<List<PaymentDTO>>(results);

                    return payments;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<List<PaymentDTO>> SearchPaymentsByAmount(string amount)
        {
            try
            {
                var results = await _paymentRepository.QueryPaymentByAmount(amount);
                if(results == null)
                {
                    _logger.LogError($"Could not get payments with amount {amount}");
                }
                else
                {
                    var payments = _mapper.Map<List<PaymentDTO>>(results);
                    return payments;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return null;
        }
    }
}
