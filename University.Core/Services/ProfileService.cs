using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Data.Entities.Identitiy;

namespace University.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;

        public ProfileService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDTO> GetUserProfile(ClaimsPrincipal claimsUser)
        {
            // Check if the user is authenticated
            if (claimsUser?.Identity?.IsAuthenticated != true)
                throw new UnauthorizedAccessException("User is not authenticated");

            // Get the user id from claims
            var userIdStr = claimsUser.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr))
                throw new UnauthorizedAccessException("User identifier not found in claims");

            if (!int.TryParse(userIdStr, out var userId))
                throw new UnauthorizedAccessException("User identifier is invalid");

            var role = claimsUser.FindFirst(ClaimTypes.Role)?.Value;

            var user = await _userManager.FindByIdAsync(userIdStr);

            if (user == null)
                throw new NotFoundException("User not found");

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                Phone = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed
            };
        }
    }

    public interface IProfileService
    {
        Task<UserDTO> GetUserProfile(ClaimsPrincipal claimsUser);
    }
}
