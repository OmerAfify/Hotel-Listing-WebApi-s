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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelListing.APIs
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountryApiController : ControllerBase
    {
        private ICountryServices _countryServices { get; }
        private IMapper _mapper { get; }
        private ILogger<CountryApiController> _logger { get; }

        public CountryApiController(ICountryServices countryServices, IMapper mapper, ILogger<CountryApiController> logger)
        {
            _countryServices = countryServices;
            _mapper = mapper;
            _logger = logger;
        }


        // GET: api/<CountryApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllCountries([FromQuery] RequestParams requestParams)
        {
            try {
                var countries = await _countryServices.GetAllCountriesAsync(requestParams);
                return Ok(_mapper.Map<List<CountryDTO>>(countries));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(GetAllCountries)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }


        // GET: api/<CountryApiController>/id

        [HttpGet("{id:int}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountryById(int id)
        {
            try
            {
                var country = await _countryServices.GetCountryByIdAsync(id);
                return Ok(_mapper.Map<CountryDTO>(country));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(GetCountryById)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }

        // POST : api/<CountryApiController>

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public IActionResult AddCountry([FromBody] CreateCountryDTO countryDTO)
        {

        
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(AddCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var NewCountry = _mapper.Map<Country>(countryDTO);

                _countryServices.AddCountry(NewCountry);

                return CreatedAtRoute("GetCountry", new { id = NewCountry.countryId }, NewCountry);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(GetAllCountries)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }



        // PUT : api/<CountryApiController>/id
        [Authorize(Roles ="ADMIN")]
        [HttpPut("{id:int}")]
        public IActionResult UpdateCountry([FromBody] UpdateCountryDTO countryDTO, int id)
        {

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid PUT attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country =  _countryServices.GetCountryById(id);

                if (country == null)
                {
                    return BadRequest();
                }

                 _mapper.Map(countryDTO, country);
                 _countryServices.UpdateCountry(country);


                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(UpdateCountry)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }


        // Delete : api/<CountryApiController>/id
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteCountry(int id)
        {

            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(UpdateCountry)}");
                return BadRequest(ModelState);
            }

            try
            {
                var country =  _countryServices.GetCountryById(id);

                if (country == null)
                {
                    return BadRequest("Country Not Found, Please enter a valid Id");
                }

                 _countryServices.DeleteCountry(id);


                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"an error occured while accessing {nameof(DeleteCountry)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }

    }
}
