using DomainLayer.Models;
using ServiceLayer.ICustomServices;
using DomainLayer.Enums;
using DataLayer.Data;
using TransferLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ServiceLayer.CustomServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger<UserService> _logger;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();

                return _mapper.Map<List<UserDTO>>(users);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }

            return null;
        }

        public async Task<UserDTO> GetUserById(Guid Id)
        {
            try
            {
                var user = await _userRepository.GetUserById(Id);
                
                return _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }

            return null;
        }

        public async Task<bool> CreateUser(UserDTO userDTO) 
        {
            try
            {
                var id = new Guid();
                var user = _userRepository.MapUserDTO(userDTO);
                await _userRepository.Insert(user);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if(user == null)
                {
                    throw new ArgumentNullException("User");
                }
                await _userRepository.Update(userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if(user == null)
                {
                    throw new ArgumentNullException("user");
                }
                await _userRepository.Remove(userId); // Change here
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<List<UserDTO>> SearchUsers(string searchItem)
        {
            try
            {
                var results = await _userRepository.Search(searchItem);

                return _mapper.Map<List<UserDTO>>(results);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<bool> GetUserPayments(Guid userId)
        {
            try
            {
                await _userRepository.GetUserPayments(userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> GetUserRides(Guid userId)
        {
            try
            {
                await _userRepository.GetUserRides(userId);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> BookRide(Guid userId, Ride ride)
        {   
            try
            {
                await _userRepository.BookRide(userId, ride);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> CancelRide(Guid rideId)
        {
            try
            {
                await _userRepository.CancelRide(rideId);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }
    }
}
