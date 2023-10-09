using TransferLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface ILocationService
    {
        Task<List<LocationDTO>> GetAllLocations();

        Task<LocationDTO> GetLocationById(Guid Id);

        Task<bool> CreateLocation(LocationDTO locationDTO);

        Task<bool> UpdateLocation(Guid locationId, LocationDTO updatedLocation);

        Task<bool> DeleteLocation(Guid locationId);

        //TODO: Implement search method by name
    }
}


