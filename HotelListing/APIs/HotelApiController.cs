using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.DTOs;
using HotelListing.Interfaces;
using HotelListing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelListing.APIs
{

    [Route("api/[controller]")]
    [ApiController]
    public class HotelApiController : ControllerBase
    {
        private IHotelServices _hotelServices { get; }
        private IMapper _mapper { get; }

        private ILogger<HotelApiController> _logger { get; }

        public HotelApiController(IHotelServices hotelServices, IMapper mapper, ILogger<HotelApiController> logger)
        {
            _hotelServices = hotelServices;
            _mapper = mapper;
            _logger = logger;
        }


        // GET: api/<HotelApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllHotels([FromQuery] RequestParams requestParams)
        {
            try
            {
                var hotels = await _hotelServices.GetAllHotelsAsync(requestParams);
                return Ok(_mapper.Map<List<HotelDTO>>(hotels));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(GetAllHotels)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }


        // GET: api/<HotelApiController>/id
        [HttpGet("{id:int}", Name = "GetHotel")]

        public async Task<IActionResult> GetHotelById(int id)
        {
            try
            {
                var hotel = await _hotelServices.GetHotelByIdAsync(id);
                return Ok(_mapper.Map<HotelDTO>(hotel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(GetHotelById)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }


        // GET: api/<HotelApiController>/countryName
        [HttpGet("{countryName}")]
        public IActionResult GetAllHotelsInCountry(string countryName)
        {
            try
            {
                var hotels = _hotelServices.GetCountryHotelsByCountryName(countryName);
                return Ok(_mapper.Map<List<HotelDTO>>(hotels));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }




        // POST: api/<HotelApiController>
        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public IActionResult AddHotel([FromBody] CreateHotelDTO hotelDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(AddHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _mapper.Map<Hotel>(hotelDTO);

                _hotelServices.AddHotel(hotel);

                return CreatedAtRoute("GetHotel", new { id = hotel.hotelId }, hotel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(AddHotel)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }




        // PUT: api/<HotelApiController>

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id:int}")]
        public IActionResult UpdateHotel([FromBody] UpdateHotelDTO hotelDTO, int id)
        {

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid PUT attempt in {nameof(UpdateHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _hotelServices.GetHotelById(id);

                if (hotel == null)
                    return BadRequest("Please enter a valid hotel ID");


                _mapper.Map(hotelDTO, hotel);

                _hotelServices.UpdateHotel(hotel);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(UpdateHotel)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }

        // Delete: api/<HotelApiController>/id

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteHotel(int id)
        {

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(DeleteHotel)}");
                return BadRequest(ModelState);
            }

            try
            {
                var hotel = _hotelServices.GetHotelById(id);

                if (hotel == null)
                    return BadRequest("Please enter a valid hotel ID");

                _hotelServices.DeleteHotel(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(DeleteHotel)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }





    }
}