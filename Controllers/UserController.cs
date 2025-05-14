using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Data.DTOs;
using PizzaAPI.Data.Entities;
using PizzaAPI.Data.Interfaces;
using PizzaAPI.JwtToken;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _tokenGenerator;

        // Inject the user repository and the JWT token generator
        public UserController(IUserRepository userRepository, JwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        // POST: api/user/register
        // Endpoint to register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            // Check if username is already taken
            var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }

            // Create a new user entity
            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password, // In production, hash this!
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            await _userRepository.AddUserAsync(user);
            var result = await _userRepository.SaveChangesAsync();

            if (!result)
            {
                return StatusCode(500, "Failed to register user.");
            }

            return Ok("User registered successfully.");
        }

        // POST: api/user/login
        // Endpoint to log in and receive a JWT token
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            // Get user by username
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user == null || user.Password != dto.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Generate a JWT token for this user
            var token = _tokenGenerator.GenerateToken(user.Id, user.Username);

            // Return the token in the response
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("Read_My_Details")]
        public async Task<IActionResult> GetMyDetails()
        {
            // Extract user ID from the JWT token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized("Invalid user ID in token.");

            // Get the user from the database
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            // Map to read-only DTO
            var result = new UserReadDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password

            };

            return Ok(result);
        }

        [Authorize]
        [HttpPut("Update_My_Data")]
        public async Task<IActionResult> UpdateMyDetails([FromBody] UserUpdateDto dto)
        {
            // Get user ID from token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return Unauthorized("Invalid user ID in token.");

            // Load user from DB
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            // Update only allowed fields
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Password = dto.Password;
            user.Username = dto.Username;

            var success = await _userRepository.SaveChangesAsync();
            if (!success)
                return StatusCode(500, "Failed to update user.");

            return Ok("Profile updated successfully.");
        }

        




    }
}
