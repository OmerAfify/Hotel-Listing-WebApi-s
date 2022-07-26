using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.DTOs;
using HotelListing.Models;

namespace HotelListing.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();

        }

    }
}
