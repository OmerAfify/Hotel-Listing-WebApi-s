using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Interfaces;
using HotelListing.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace HotelListing.Business_Services
{
    public class HotelServices : IHotelServices
    {
        private ApplicationDbContext _context { get; }

        public HotelServices(ApplicationDbContext context)
        {
            _context = context;

        }

        public IEnumerable<Hotel> GetAllHotels()
        {
            return _context.hotels.ToList();
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            return await _context.hotels.ToListAsync();
        }


        public async Task<IPagedList<Hotel>> GetAllHotelsAsync(RequestParams requestParams)
        {
            return await _context.hotels.ToPagedListAsync(requestParams.pageNumber, requestParams.pageSize);
        }

        public Hotel GetHotelById(int id)
        {
            return _context.hotels.Where(h => h.hotelId == id).FirstOrDefault();
        }

     
        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.hotels.Where(h=>h.hotelId==id).FirstOrDefaultAsync();
        }


        public List<Hotel> GetCountryHotelsByCountryName(string countryName)
        {

            var coutry = _context.countries.Where(c => c.countryName == countryName).FirstOrDefault();

            if (coutry == null)
            {
                return new List<Hotel>();
            }
            else
            {
                return _context.hotels.Where(c => c.countryId == coutry.countryId).ToList();
            }

        }

        public void AddHotel(Hotel hotel)
        {
            _context.hotels.AddAsync(hotel);
            _context.SaveChanges();
        }

        public void UpdateHotel(Hotel hotel)
        {
            _context.Attach(hotel);
            _context.Entry(hotel).State = EntityState.Modified;
            _context.SaveChanges();

        }
        
        public void DeleteHotel(int id)
        {
            var hotel = _context.hotels.Where(hotel => hotel.hotelId == id).FirstOrDefault();

            if (hotel == null)
                return;

            _context.Attach(hotel);
            _context.Remove(hotel);
            _context.SaveChanges();

        }
    }
}
