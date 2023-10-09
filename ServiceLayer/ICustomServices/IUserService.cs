using DomainLayer.Models;
using System;
using TransferLayer.DTOS;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllUsers();

        Task<UserDTO> GetUserById(Guid Id);

        Task<bool> CreateUser (UserDTO userDTO);

        Task<bool> UpdateUser (Guid userId, UserDTO updatedUser);

        Task<bool> DeleteUser (Guid userId);

        Task<List<UserDTO>> SearchUsers (string searchItem);

        Task<bool> GetUserPayments(Guid userId);

        Task<bool> GetUserRides(Guid userId);

        //Task<bool> BookRide(Guid userId, Ride ride);

        Task<bool> CancelRide (Guid rideId);


    }
}
