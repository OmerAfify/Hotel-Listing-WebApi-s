using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOs;
using HotelListing.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace HotelListing.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {

        private UserManager<MyIdentityUser> _userManager { get; }
        private SignInManager<MyIdentityUser> _signInManager { get; }
        private IUserAuthenticationManager _userAuthenticationManager { get; }
        private IMapper _mapper { get; }

        private IConfiguration _configuration;
       
        public UserApiController(UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager ,
            IUserAuthenticationManager userAuthenticationManager,
            IMapper mapper,
            IConfiguration configuration
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userAuthenticationManager = userAuthenticationManager;
            _mapper = mapper;
            _configuration = configuration;
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



        [HttpPost]
        [Route("loginWithJWT")]
        public async Task<IActionResult> LoginWithJWT([FromBody] LoginUserDto userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                if (await _userAuthenticationManager.ValidateUser(userDTO) == true)
                {
                    return Accepted(new { Token = await _userAuthenticationManager.CreateToken() });
                }
                else
                {
                    return Unauthorized(userDTO);
                }

            }
            catch (Exception ex)
            {
                return Problem($"Something went wrong in {nameof(LoginWithJWT)} error is {ex.Message}", statusCode: 500);
            }

        }


      


    }
}
