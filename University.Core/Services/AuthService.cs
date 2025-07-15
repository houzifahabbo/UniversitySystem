using Microsoft.AspNetCore.Identity;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities.Identitiy;

namespace University.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManger;

        public AuthService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManger = roleManager;
        }

        public async Task<UserDTO> Login(LoginForm form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var result = await _signInManager.PasswordSignInAsync(form.Email, form.Password, true, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(form.Email);
                if (user == null)
                    throw new NotFoundException($"User not found with email {form.Email}");

                var roles = await _userManager.GetRolesAsync(user);

                return new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = roles.Count > 0 ? Enum.Parse<UserRole>(roles[0]) : UserRole.Student // Default to Student if no roles
                };
            }
            else if (result.IsLockedOut)
                throw new BusinessException("Account is locked out.");
            else if (result.IsNotAllowed)
                throw new BusinessException("Account is not allowed to login.");
            throw new BusinessException("Invalid login attempt.");
        }

        public async Task<UserDTO> Register(RegisterForm form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var userExists = await _userManager.FindByEmailAsync(form.Email);
            if (userExists != null)
                throw new BusinessException("User already exists");

            var user = new User
            {
                Email = form.Email,
                FirstName = form.FirstNmae,
                LastName = form.LastName,
                UserName = form.Email
            };

            var result = await _userManager.CreateAsync(user, form.Password);
            if (!result.Succeeded)
            {
                throw new BusinessException(
                    result.Errors
                        .GroupBy(e => e.Code)
                        .ToDictionary(f => f.Key, m => m.Select(d => d.Description).ToList())
                );
            }

            var role = form.Role.ToString();
            if (!await _roleManger.RoleExistsAsync(role))
                await _roleManger.CreateAsync(new IdentityRole<int>(role));

            await _userManager.AddToRoleAsync(user, role);

            return new UserDTO()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = form.Role
            };
        }
    }

    public interface IAuthService
    {
        Task<UserDTO> Login(LoginForm form);
        Task<UserDTO> Register(RegisterForm form);

    }
}
