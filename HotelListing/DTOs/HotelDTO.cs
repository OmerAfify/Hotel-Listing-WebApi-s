using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{
    public class HotelDTO
    {
        public int hotelId { get; set; }
        public string hotelName { get; set; }
        public string hotelAddress { get; set; }
        public double rating { get; set; }
        public int countryId { get; set; }
    }
}
