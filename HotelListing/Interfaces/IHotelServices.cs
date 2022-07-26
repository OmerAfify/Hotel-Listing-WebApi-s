using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Models;

namespace HotelListing.Interfaces
{
    public interface IHotelServices
    {
        public List<Hotel> GetAllHotels();
        public Hotel GetHotelById(int id);
        public List<Hotel> GetCountryHotelsByCountryName(string countryName);

        //Async Methods Section
        public Task<List<Hotel>> GetAllHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);



    }
}
