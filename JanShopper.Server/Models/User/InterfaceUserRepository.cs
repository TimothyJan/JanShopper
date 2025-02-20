using Microsoft.EntityFrameworkCore;
using JanShopper.Server.Models;

namespace JanShopper.Server.Repositories
{
    public interface InterfaceUserRepository
    {
        Task<IEnumerable<UserProfileDTO>> GetAllUsersAsync();
        Task<UserProfileDTO?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task AddUserAsync(UserRegistrationDTO userRegistrationDTO);
        Task UpdateUserAsync(UserProfileDTO userProfileDTO);
        Task DeleteUserAsync(int userId);
    }
}
