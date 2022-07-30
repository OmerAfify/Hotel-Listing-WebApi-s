using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{

    public class CreateCountryDTO
    {
        public string countryName { get; set; }
        public string countryShortName { get; set; }

    }
    
    public class CountryDTO : CreateCountryDTO
    {
        public int countryId { get; set; }

    }
    public class UpdateCountryDTO : CreateCountryDTO
    {
     

    }

}
