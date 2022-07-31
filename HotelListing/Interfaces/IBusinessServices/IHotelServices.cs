using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Models;
using X.PagedList;

namespace HotelListing.Interfaces
{
    public interface IHotelServices
    {
        public IEnumerable<Hotel> GetAllHotels();     
        public Task<IEnumerable<Hotel>> GetAllHotelsAsync();
        public Task<IPagedList<Hotel>> GetAllHotelsAsync(RequestParams requestParams);


        public Hotel GetHotelById(int id);
        public Task<Hotel> GetHotelByIdAsync(int id);

        public List<Hotel> GetCountryHotelsByCountryName(string countryName);


        public void AddHotel(Hotel hotel);
        public void UpdateHotel(Hotel hotel);
        public void DeleteHotel(int id);



    }
}
