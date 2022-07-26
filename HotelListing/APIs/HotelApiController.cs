using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.DTOs;
using HotelListing.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.APIs
{

    [Route("api/[controller]")]
    [ApiController]
    public class HotelApiController : ControllerBase
    {
        private IHotelServices _hotelServices { get; }
        private IMapper _mapper { get; }

        public HotelApiController(IHotelServices hotelServices, IMapper mapper)
        {
            _hotelServices = hotelServices;
            _mapper = mapper;
        }


        // GET: api/<HotelApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                var hotels = await _hotelServices.GetAllHotelsAsync();
                return Ok(_mapper.Map<List<HotelDTO>>(hotels));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }


        // GET: api/<HotelApiController>/id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            try
            {
                var hotel = await _hotelServices.GetHotelByIdAsync(id);
                return Ok(_mapper.Map<HotelDTO>(hotel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }


        // GET: api/<HotelApiController>/countryName
        [HttpGet("{countryName}")]
        public  IActionResult GetAllHotelsInCountry(string countryName)
        {
            try
            {
                var hotels =  _hotelServices.GetCountryHotelsByCountryName(countryName);
                return Ok(_mapper.Map<List<HotelDTO>>(hotels));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }







    }
}