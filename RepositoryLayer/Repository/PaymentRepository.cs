using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using DomainLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class PaymentRepository : IPaymentRepository 
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
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
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            _context.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            _context.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            _context.Remove(payment);
            await _context.SaveChangesAsync();
        }

        public void Remove(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            _context.Remove(payment);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
