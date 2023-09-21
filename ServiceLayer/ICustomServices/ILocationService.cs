using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface ILocationService
    {
        Task GetAllLocations();

        Task GetLocationById(Guid Id);

        Task CreateLocation(Location location);

        Task UpdateLocation(Location location);

        Task DeleteLocation(Location location);
    }
}


