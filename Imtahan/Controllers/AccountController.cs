using Imtahan.DTOs.UserDto;
using Imtahan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Imtahan.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userRegisterDto);
            }
            User user = new User()
            {
                Fullname = userRegisterDto.Fullname,
                UserName=userRegisterDto.Username,
                Email = userRegisterDto.Email
            };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(userRegisterDto);
            }
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "admin"
            });
            await _userManager.AddToRoleAsync(user, "admin");

            return Redirect(nameof(Login));

        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(userLoginDto);
            }
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);
            if (!await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                ModelState.AddModelError("", "username or Password is wrong");
                return View(userLoginDto);
            }
            await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, false, false);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "Home");
        }
    }
}
