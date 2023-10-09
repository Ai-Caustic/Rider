using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using TransferLayer.DTOS;


namespace DomainLayer.IRepository
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllLocations();

        Task<Location> GetLocationById(Guid Id);

        Task Insert(Location location);

        Task Update(Guid locationId, Location updatedLocation);

        Task Remove(Guid locationId);

        Location MapLocationDTO(LocationDTO locationDTO);

        //TODO: Ad search method by name

    }
}
