
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TestAPI.DBFiles.Entities;
using TestAPI.DTOS.User;
using Microsoft.EntityFrameworkCore;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
               
                return Ok("User registered successfully.");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserExistenceCheckDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                // Fetch the user from the database
                var user = await _userManager.Users
                    .Where(u => u.UserName == model.UserName)
                    .Select(u => new SendBackUserDataDTO
                    {
                        UserName = u.UserName,
                       Email = u.Email
                    })
                    .FirstOrDefaultAsync();

                // Check if the user exists (should be true since login succeeded)
                if (user != null)
                {
                    
                    return Ok(user);
                }
            }

            return Unauthorized("Invalid login attempt.");

        }
        
    }
}
