using JanShopper.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace JanShopper.Server.Repositories
{
    public class UserRepository : InterfaceUserRepository
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
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
        }

        // Get a user by ID
        public async Task<UserProfileDTO?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.UserID == userId)
                .Select(u => new UserProfileDTO
                {
                    UserID = u.UserID,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();
        }

        // Get a user by email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        // Add a new user
        public async Task AddUserAsync(UserRegistrationDTO userRegistrationDTO)
        {
            var user = new User
            {
                UserName = userRegistrationDTO.UserName,
                Email = userRegistrationDTO.Email,
                Password = userRegistrationDTO.Password // Note: Hash the password before saving
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        // Update a user
        public async Task UpdateUserAsync(UserProfileDTO userProfileDTO)
        {
            var user = await _context.Users.FindAsync(userProfileDTO.UserID);
            if (user != null)
            {
                user.UserName = userProfileDTO.UserName;
                user.Email = userProfileDTO.Email;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        // Delete a user
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}