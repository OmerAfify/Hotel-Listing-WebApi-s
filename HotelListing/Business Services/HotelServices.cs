using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Interfaces;
using HotelListing.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Business_Services
{
    public class HotelServices : IHotelServices
    {
        private ApplicationDbContext _context { get; }

        public HotelServices(ApplicationDbContext context)
        {
            _context = context;

        }

        public List<Hotel> GetAllHotels()
        {
            return _context.hotels.ToList();
        }

        public Hotel GetHotelById(int id)
        {
            return _context.hotels.Where(h => h.hotelId == id).FirstOrDefault();
        }

        public List<Hotel> GetCountryHotelsByCountryName(string countryName) {

            var coutry = _context.countries.Where(c => c.countryName == countryName).FirstOrDefault();

            if (coutry == null)
            {
                return new List<Hotel>();
            }
            else
            {
                 return _context.hotels.Where(c=>c.countryId == coutry.countryId).ToList();
            }

        }


        // Async Methods Section
        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _context.hotels.ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.hotels.Where(h=>h.hotelId==id).FirstOrDefaultAsync();
        }

      
    }
}
