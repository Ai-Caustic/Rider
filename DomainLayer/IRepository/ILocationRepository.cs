using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;


namespace DomainLayer.IRepository
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllLocations();

        Task<Location> GetLocationById(Guid Id);

        Task Insert(Location location);

        Task Update(Location location);

        Task Delete(Location location);

        void Remove(Location location);

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
