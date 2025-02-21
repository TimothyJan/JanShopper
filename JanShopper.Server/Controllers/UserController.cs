using JanShopper.Server.Models;
using JanShopper.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JanShopper.Server.Controllers
{
    [Route("api/[controller]")] // Base route: /api/user
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
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

            try
            {
                var createdUser = await _userRepository.CreateUserAsync(userRegistrationDTO);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser); // 201 Created
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409 Conflict if user already exists
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the user."); // 500 Internal Server Error
            }
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserProfileDTO userProfileDTO)
        {
            if (id != userProfileDTO.Id)
            {
                return BadRequest("ID mismatch."); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request with validation errors
            }

            var result = await _userRepository.UpdateUserAsync(userProfileDTO);
            if (!result)
            {
                return NotFound(); // 404 Not Found if user does not exist
            }

            return NoContent(); // 204 No Content
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound(); // 404 Not Found if user does not exist
            }

            return NoContent(); // 204 No Content
        }
    }
}