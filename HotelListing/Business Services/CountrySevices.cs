﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Interfaces;
using HotelListing.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Business_Services
{
    public class CountrySevices : ICountryServices
    {

        private ApplicationDbContext _context { get;}

        public CountrySevices(ApplicationDbContext context)
        {
            _context = context;

        }

        public IEnumerable<Country> GetAllCountries()
        {
            return  _context.countries;
        }

        public Country GetCountryById(int id)
        {
            return _context.countries.Where(c=>c.countryId==id).FirstOrDefault();
        }



        // Async Methods Section
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return  await _context.countries.ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.countries.Where(c => c.countryId == id).FirstOrDefaultAsync();
        }



      






    }
}
