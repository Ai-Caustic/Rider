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
    public class Repository <T> : IRepository <T> where T : BaseEntity
    {
        #region property
        private readonly ApplicationDbContext _context;

        private DbSet<T> entities;

        #endregion

        #region Constructor
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        #endregion

        public void Delete(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            _context.SaveChanges();
        }
        public T Get(Guid Id) {
            return entities.SingleOrDefault(c => c.Id == Id);
        }
        public IEnumerable < T > GetAll() {
            return entities.AsEnumerable();
        }
        public void Insert(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            _context.SaveChanges();
        }
        public void Remove(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }
        public void SaveChanges() {
            _context.SaveChanges();
        }
        public void Update(T entity) {
            if (entity == null) {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            _context.SaveChanges();
        }
    }
}