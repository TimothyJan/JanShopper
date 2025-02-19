using JanShopper.Server.Models;
using JanShopper.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JanShopper.Server.Controllers
{
    [Route("api/[controller]")] // Base route: /api/user
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfileDTO>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<UserProfileDTO>> CreateUser([FromBody] UserRegistrationDTO userRegistrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request with validation errors
            }

            // Check if the email or username already exists
            var existingUserByEmail = await _userRepository.GetUserByEmailAsync(userRegistrationDTO.Email);
            if (existingUserByEmail != null)
            {
                return Conflict("Email already exists."); // 409 Conflict
            }

            // Add the new user
            await _userRepository.AddUserAsync(userRegistrationDTO);

            // Return the created user (without password)
            var createdUser = await _userRepository.GetUserByEmailAsync(userRegistrationDTO.Email);
            if (createdUser == null)
            {
                return StatusCode(500, "An error occurred while retrieving the created user."); // 500 Internal Server Error
            }

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserID }, createdUser); // 201 Created
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserProfileDTO userProfileDTO)
        {
            if (id != userProfileDTO.UserID)
            {
                return BadRequest("ID mismatch."); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request with validation errors
            }

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(); // 404 Not Found
            }

            await _userRepository.UpdateUserAsync(userProfileDTO);
            return NoContent(); // 204 No Content
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); // 404 Not Found
            }

            await _userRepository.DeleteUserAsync(id);
            return NoContent(); // 204 No Content
        }
    }
}
