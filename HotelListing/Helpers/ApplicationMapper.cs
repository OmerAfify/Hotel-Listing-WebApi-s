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
            CreateMap<Country, CountryDTO>().ReverseMap();

            CreateMap<Country, CreateCountryDTO>().ReverseMap();

            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();

            CreateMap<MyIdentityUser, UserDTO>().ReverseMap();

        }

    }
}
