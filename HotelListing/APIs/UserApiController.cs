using System;
using System.Threading.Tasks;
using AutoMapper;
using HotelListing.Data;
using HotelListing.DTOs;
using HotelListing.Interfaces.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
        private ILogger<UserApiController> _logger { get; }


        public UserApiController( UserManager<MyIdentityUser> userManager,
            SignInManager<MyIdentityUser> signInManager ,
            IUserAuthenticationManager userAuthenticationManager,
            IMapper mapper,
            IConfiguration configuration,
            ILogger<UserApiController> logger
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userAuthenticationManager = userAuthenticationManager;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }



        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation("Registeration attempt for " + userDTO.Email);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(Register)}");
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

                    _logger.LogError($"Invalid POST attempt in {nameof(Register)}");
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, userDTO.roles);

                return Accepted();

             }catch(Exception ex)
                {
                _logger.LogError(ex, $"an error occured while accessing {nameof(Register)}");

                return StatusCode(500, ex.Message + "Internal server error");
                }

        }

  


        [HttpPost]
        [Route("loginWithJWT")]
        public async Task<IActionResult> LoginWithJWT([FromBody] LoginUserDto userDTO)
        {

            _logger.LogInformation("Login attempt for " + userDTO.Email);

            if (!ModelState.IsValid)
            {

                _logger.LogError($"Invalid POST attempt in {nameof(LoginWithJWT)}");
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
                _logger.LogError(ex, $"an error occured while accessing {nameof(LoginWithJWT)}");
                return Problem($"Something went wrong in {nameof(LoginWithJWT)} error is {ex.Message}", statusCode: 500);
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto userDTO)
        {

            _logger.LogInformation("Login attempt for " + userDTO.Email);

            if (!ModelState.IsValid)
            {

                _logger.LogError($"Invalid POST attempt in {nameof(Login)}");
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
                _logger.LogError(ex, $"an error occured while accessing {nameof(Login)}");
                return StatusCode(500, ex.Message + "Internal server error");
            }

        }






    }
}
