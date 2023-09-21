using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IUserService
    {
        Task GetAllUsers();

        Task GetUserById(Guid Id);

        Task CreateUser (User user);

        Task UpdateUser (User user);

        Task DeleteUser (User user);

        Task SearchUser (string searchItem);

        Task GetUserPayments(Guid userId);

        Task GetUserRides(Guid userId);

        Task BookRide(Guid userId, Ride ride);

        Task CancelRide (Guid rideId);


    }
}
