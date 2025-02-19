using Microsoft.EntityFrameworkCore;

namespace JanShopper.Server.Models
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
