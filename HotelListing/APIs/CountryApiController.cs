using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.DTOs;
using HotelListing.Interfaces;
using HotelListing.Models;
using Microsoft.AspNetCore.Authorization;
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
            try{
                var countries = await _countryServices.GetAllCountriesAsync();
                return Ok(_mapper.Map<List<CountryDTO>>(countries));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message + "Internal server error");
            }
        }


        // GET: api/<CountryApiController>/id

       // [Authorize]
        [HttpGet("{id:int}")]

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


    }
}
