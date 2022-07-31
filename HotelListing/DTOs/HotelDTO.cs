using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{
    public class CreateHotelDTO {


        [Required]
        [MinLength(5, ErrorMessage = "Hotels's name should be atleast 5 characters")]
        [MaxLength(30, ErrorMessage = "Hotels's name should be atmost 30 characters")]
        public string hotelName { get; set; }

        [Required]
        public string hotelAddress { get; set; }
        public double rating { get; set; }
        public int countryId { get; set; }

    }


     public class HotelDTO : CreateHotelDTO {

        [Required]
        public int hotelId { get; set; }

    } 
    
    public class UpdateHotelDTO : CreateHotelDTO {


    }

}
