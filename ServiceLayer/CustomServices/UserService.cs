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

                if (users == null)
                {
                    _logger.LogError("Could not get users");
                }
                else
                {
                    var result = _mapper.Map<List<UserDTO>>(users);
                    return result;
                }
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
                if (user == null)
                {
                    _logger.LogError("Could not find user");
                }
                else
                {
                    var result = _mapper.Map<UserDTO>(user);
                    return result;
                }
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
                if(userDTO == null)
                {
                    _logger.LogError("User cannot be null");
                    return false;
                }
                else
                {
                    var user = _userRepository.MapUserDTO(userDTO);
                    await _userRepository.Insert(user);
                    _logger.LogInformation("User created successfully");
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(Guid userId, UserDTO updatedUser)
        {
            try
            {
                var user = await _userRepository.GetUserById(userId);
                if (user == null)
                {
                    _logger.LogError("User not found");
                    return false;
                }
                else
                {
                    var mappedUser = _userRepository.MapUserDTO(updatedUser);
                    await _userRepository.Update(userId, mappedUser);
                    return true;
                }
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
                    _logger.LogError("User not found");
                    return false;
                }
                else
                {
                    await _userRepository.Remove(userId);
                    return true;
                }
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
                if(results == null)
                {
                    _logger.LogError($"Could not find users with {searchItem}");
                }
                else
                {
                    var result =  _mapper.Map<List<UserDTO>>(results);
                    return result;
                }
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

        //public async Task<bool> BookRide(Guid userId, Ride ride)
        //{   
        //    try
        //    {
        //        await _userRepository.BookRide(userId, ride);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
        //        return false;
        //    }
        //}

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
