using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {

        private UserManager<MyIdentityUser> _userManager { get; }
        private SignInManager<MyIdentityUser> _signInManager { get; }
        private IMapper _mapper { get; }

        public UserApiController(   UserManager<MyIdentityUser> userManager,SignInManager<MyIdentityUser> signInManager ,IMapper mapper )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }



        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<MyIdentityUser>(userDTO);
                user.UserName = userDTO.Email;

  
                var result = await _userManager.CreateAsync(user, userDTO.Password);


                if (!result.Succeeded)
                {

                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDTO.roles);

                return Accepted();

             }catch(Exception ex)
                {
                    return StatusCode(500, ex.Message + "Internal server error");
                }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _signInManager.
                    PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);

                if (!result.Succeeded)
                {
                    return Unauthorized(userDTO);
                }

                return Accepted();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + "Internal server error");
            }

        }

    }
}
