using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOs;
using HotelListing.Models;

namespace HotelListing.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
          
            //country Mapping
            CreateMap<Country, CountryDTO>().ReverseMap();

            CreateMap<Country, CreateCountryDTO>().ReverseMap();

            CreateMap<Country, UpdateCountryDTO>().ReverseMap();


            //hotel Mapping
            CreateMap<Hotel, HotelDTO>().ReverseMap();

            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();

            CreateMap<Hotel, UpdateHotelDTO>().ReverseMap();


            //user Mapping

            CreateMap<MyIdentityUser, UserDTO>().ReverseMap();

        }

    }
}
