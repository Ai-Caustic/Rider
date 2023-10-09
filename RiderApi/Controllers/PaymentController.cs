using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ICustomServices;
using TransferLayer.DTOS;

namespace RiderApi.Controllers
{
    [Route("rideshare/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet(nameof(GetAllPayments))]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var payments = await _paymentService.GetAllPayments();
                if(payments != null)
                {
                    _logger.LogInformation("Returned all payments");
                    return Ok(payments);
                }
                else
                {
                    return NotFound("Payments not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetPaymentById))]
        public async Task<IActionResult> GetPaymentById(Guid Id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentById(Id);
                if(payment != null)
                {
                    _logger.LogInformation("Returned payment");
                    return Ok(payment);
                }
                else
                {
                    return NotFound("Payment");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CreatePayment))]
        public async Task<IActionResult> CreatePayment(PaymentDTO payment)
        {
            try
            {
                if(payment == null)
                {
                    _logger.LogError("Invalid payment input");
                    return BadRequest();
                }
                else
                {
                    await _paymentService.CreatePayment(payment);
                    _logger.LogInformation("Created payment");
                    return Ok("Created payment");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdatePayment))]
        public async Task<IActionResult> UpdatePayment(Guid Id, PaymentDTO updatedPayment)
        {
            try
            {
                var payment = await _paymentService.GetPaymentById(Id);
                if(payment == null)
                {
                    _logger.LogError($"Could not find payment {Id}");
                    return NotFound("Payment not found");
                }
                else
                {
                    await _paymentService.UpdatePayment(Id, updatedPayment);
                    _logger.LogInformation("Updated payment");
                    return Ok("updated payment");
                }
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeletePayment))]
        public async Task<IActionResult> DeletePayment(Guid Id)
        {
            try
            {
                var payment = await _paymentService.GetPaymentById(Id);
                if(payment == null)
                {
                    _logger.LogError("Payment not found");
                    return NotFound("Payment not found");
                }
                else
                {
                    await _paymentService.DeletePayment(Id);
                    _logger.LogInformation("Deleted payment");
                    return Ok("Deleted payment");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetUserPayments))]
        public async Task<IActionResult> GetUserPayments(Guid userId)
        {
            try
            {
                var payments = await _paymentService.GetUserPayments(userId);
                if(payments == null)
                {
                    _logger.LogError($"Payments for user {userId} not found");
                    return NotFound("User payments");
                }
                else
                {
                    _logger.LogInformation("Returned user payments");
                    return Ok(payments);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetPaymentsByMethod))]
        public async Task<IActionResult> GetPaymentsByMethod(string method)
        {
            try
            {
                var payments = await _paymentService.SearchPaymentsByMethod(method);
                if(payments == null)
                {
                    _logger.LogError($"Payments by {method} not found");
                    return NotFound("Payments by method");
                }
                else
                {
                    _logger.LogInformation($"Returned payments by method: {method}");
                    return Ok(payments);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetPaymentsByAmount))]
        public async Task<IActionResult> GetPaymentsByAmount(string amount)
        {
            try
            {
                var payments = await _paymentService.SearchPaymentsByAmount(amount);
                if(payments == null)
                {
                    _logger.LogError($"Payments of amount {amount} not found");
                    return NotFound("Payments by amount");
                }
                else
                {
                    _logger.LogInformation($"Returned payments of amount: {amount}");
                    return Ok(payments);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }
    }
}