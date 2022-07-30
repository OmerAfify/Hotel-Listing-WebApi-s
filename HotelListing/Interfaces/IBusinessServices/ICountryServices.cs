using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Models;

namespace HotelListing.Interfaces
{
    public interface ICountryServices
    {
        public IEnumerable<Country> GetAllCountries();
        public Country GetCountryById(int id);



        // Async Methods Section
        public Task<Country> GetCountryByIdAsync(int id);
        public Task<IEnumerable<Country>> GetAllCountriesAsync();
        public void AddCountryAsync(Country country);
        public void UpdateCountry(Country country);
        public void DeleteCountry(int id);

    }

}
