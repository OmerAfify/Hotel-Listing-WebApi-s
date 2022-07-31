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
        public Task<IEnumerable<Country>> GetAllCountriesAsync();

        public Country GetCountryById(int id);
        public Task<Country> GetCountryByIdAsync(int id);

        public void AddCountry(Country country);
        public void UpdateCountry(Country country);
        public void DeleteCountry(int id);

    }

}
