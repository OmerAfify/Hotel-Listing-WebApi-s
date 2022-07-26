using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Models
{
    public class Country
    {
        public int countryId { get; set; }
        public string countryName { get; set; }
        public string countryShortName { get; set; }
        public virtual List<Hotel> hotels { get; set; }


    }
}
