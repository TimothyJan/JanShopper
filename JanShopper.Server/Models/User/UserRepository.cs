using Microsoft.EntityFrameworkCore;
using JanShopper.Server.Models;
using BCrypt.Net; // For password hashing

namespace JanShopper.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JanShopperDbContext _context;

        public UserRepository(JanShopperDbContext context)
        {
            _context = context;
        }

        // Get all users
        public async Task<IEnumerable<UserProfileDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserProfileDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
        }

        // Get a user by Id
        public async Task<UserProfileDTO?> GetUserByIdAsync(int Id)
        {
            return await _context.Users
                .Where(u => u.Id == Id)
                .Select(u => new UserProfileDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();
        }

        // Get a user by email
        public async Task<UserProfileDTO?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .Select(u => new UserProfileDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();
        }

        // Add a new user
        public async Task<UserProfileDTO> CreateUserAsync(UserRegistrationDTO userRegistrationDTO)
        {
            // Check if the user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == userRegistrationDTO.Email || u.UserName == userRegistrationDTO.UserName);

            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with the same email or username already exists.");
            }

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegistrationDTO.Password);

            // Create the new user
            var user = new User
            {
                UserName = userRegistrationDTO.UserName,
                Email = userRegistrationDTO.Email,
                Password = hashedPassword // Store the hashed password
            };

            // Add the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return the created user as a UserProfileDTO
            return new UserProfileDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }

        // Update a user
        public async Task<bool> UpdateUserAsync(UserProfileDTO userProfileDTO)
        {
            var user = await _context.Users.FindAsync(userProfileDTO.Id);
            if (user == null)
            {
                return false; // User not found
            }

            user.UserName = userProfileDTO.UserName;
            user.Email = userProfileDTO.Email;
            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a user
        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false; // User not found
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}