using DomainLayer.Models;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CustomServices
{
    public class LocationService : ICustomService<Location>
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public void Delete(Location entity) {
            try {
                if (entity != null) {
                    _locationRepository.Delete(entity);
                    _locationRepository.SaveChanges();
                }
            } catch (Exception) {
                throw;
            }
        }
        public Location Get(Guid Id) {
            try {
                var obj = _locationRepository.Get(Id);
                if (obj != null) {
                    return obj;
                } else {
                    return null;
                }
            } catch (Exception) {
                throw;
            }
        }
        public IEnumerable < Location > GetAll() {
            try {
                var obj = _locationRepository.GetAll();
                if (obj != null) {
                    return obj;
                } else {
                    return null;
                }
            } catch (Exception) {
                throw;
            }
        }
        public void Insert(Location entity) {
            try {
                if (entity != null) {
                    _locationRepository.Insert(entity);
                    _locationRepository.SaveChanges();
                }
            } catch (Exception) {
                throw;
            }
        }
        public void Remove(Location entity) {
            try {
                if (entity != null) {
                    _locationRepository.Remove(entity);
                    _locationRepository.SaveChanges();
                }
            } catch (Exception) {
                throw;
            }
        }
        public void Update(Location entity) {
            try {
                if (entity != null) {
                    _locationRepository.Update(entity);
                    _locationRepository.SaveChanges();
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}