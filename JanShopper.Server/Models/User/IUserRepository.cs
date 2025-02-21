using Microsoft.EntityFrameworkCore;
using JanShopper.Server.Models;

namespace JanShopper.Server.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserProfileDTO>> GetAllUsersAsync();
        Task<UserProfileDTO?> GetUserByIdAsync(int Id);
        Task<UserProfileDTO?> GetUserByEmailAsync(string email);
        Task<UserProfileDTO> CreateUserAsync(UserRegistrationDTO userRegistrationDTO);
        Task<bool> UpdateUserAsync(UserProfileDTO userProfileDTO);
        Task<bool> DeleteUserAsync(int Id);
    }
}
