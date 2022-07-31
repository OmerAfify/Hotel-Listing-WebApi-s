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

namespace HotelListing.APIs
{
    [Route("api/[controller]")]
    [ApiController]

    public class CountryApiController : ControllerBase
    {
        private ICountryServices _countryServices { get; }
        private IMapper _mapper { get; }

        public CountryApiController(ICountryServices countryServices, IMapper mapper)
        {
            _countryServices = countryServices;
            _mapper = mapper;
        }


        // GET: api/<CountryApiController>
        [HttpGet]
        public async Task<IActionResult> GetAllCountries()
        {
            try {
                var countries = await _countryServices.GetAllCountriesAsync();
                return Ok(_mapper.Map<List<CountryDTO>>(countries));
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }

    }
}
