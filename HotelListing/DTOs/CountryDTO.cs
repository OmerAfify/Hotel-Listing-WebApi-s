using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.DTOs
{

    public class CreateCountryDTO
    {

        [Required]
        [MinLength(3, ErrorMessage ="Country's name should be atleast 3 characters") ]
        [MaxLength(35, ErrorMessage ="Country's name should be atmost 35 characters") ]
        public string countryName { get; set; }


        [MinLength(1, ErrorMessage = "Country's short name should be atleast 1 characters")]
        [MaxLength(4, ErrorMessage = "Country's short name should be atmost 4 characters")]
        public string countryShortName { get; set; }


        public List<CreateHotelDTO> hotels { get; set; }

    }
    

    public class CountryDTO : CreateCountryDTO
    {

        [Required]
        public int countryId { get; set; }

    }
    public class UpdateCountryDTO : CreateCountryDTO
    {
     

    }

}
