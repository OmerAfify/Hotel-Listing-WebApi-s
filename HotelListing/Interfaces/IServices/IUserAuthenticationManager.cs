using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.DTOs;

namespace HotelListing.Interfaces.IServices
{
    public interface IUserAuthenticationManager
    {
        public Task<bool> ValidateUser(LoginUserDto userDto);
        public Task<string> CreateToken();

    }
}
