using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class PaymentRepository <T> : IPaymentRepository <T> where T : Payment 
    {
        #region property
        private readonly ApplicationDbContext _context;

        private DbSet<T> payments;
        #endregion

        #region constructor
        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
            payments = _context.Set<T>();
        }
        #endregion

        public IEnumerable<T> GetAll()
        {
            return payments.AsEnumerable();
        }

        public T Get(Guid Id)
        {
            return payments.SingleOrDefault(p => p.Id == Id);
        }

        public void Insert(T payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            payments.Add(payment);
            _context.SaveChanges();
        }

        public void Update(T payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            payments.Add(payment);
            _context.SaveChanges();
        }

        public void Delete(T payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            payments.Remove(payment);
            _context.SaveChanges();
        }

        public void Remove(T payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }
            payments.Remove(payment);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
