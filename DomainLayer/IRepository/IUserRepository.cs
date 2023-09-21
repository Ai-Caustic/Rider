using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(Guid Id);

        Task Insert(User user);

        Task Update(User user);

        Task Remove(User user);

        Task<List<User>> Search(string query);

        Task<List<Ride>> GetUserRides(Guid userId);

        Task<List<Payment>> GetUserPayments(Guid userId);

        Task BookRide(Guid userId, Ride ride);

        Task CancelRide(Guid rideId);
    }
}
