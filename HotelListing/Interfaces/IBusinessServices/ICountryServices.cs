using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Models;
using X.PagedList;

namespace HotelListing.Interfaces
{
    public interface ICountryServices
    {
        public IEnumerable<Country> GetAllCountries();
        public Task<IEnumerable<Country>> GetAllCountriesAsync();
        public Task<IPagedList<Country>> GetAllCountriesAsync(RequestParams requestParams);

        public Country GetCountryById(int id);
        public Task<Country> GetCountryByIdAsync(int id);

        public void AddCountry(Country country);
        public void UpdateCountry(Country country);
        public void DeleteCountry(int id);

    }

}
