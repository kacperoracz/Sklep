using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklep.ViewModels;

namespace Sklep.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
			_roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if(!ModelState.IsValid)
				return View(loginVM);

			var user = await _userManager.FindByNameAsync(loginVM.Login);

			if(user != null)
			{
				var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
				if(result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
			}
            else
            {
				//kod dodany na potrzeby założenia konta administratora i ról przy pierwszym odpaleniu aplikacji
				if(loginVM.Login == "admin" && loginVM.Password == "123!@#qweQWE")
                {
					IdentityRole adminRole = new IdentityRole();
					adminRole.Name = "admin";
					await _roleManager.CreateAsync(adminRole);
					IdentityRole userRole = new IdentityRole();
					userRole.Name = "user";
					await _roleManager.CreateAsync(userRole);
					user = new IdentityUser() { UserName = "admin" };
					await _userManager.CreateAsync(user, "123!@#qweQWE");
					await _userManager.AddToRoleAsync(user, "admin");
					await _signInManager.PasswordSignInAsync(user, "123!@#qweQWE", false, false);
					return RedirectToAction("Index", "Home");
				}
            }

			ModelState.AddModelError("", "Nazwa użytkownika lub hasło nieprawidłowe");

			return View(loginVM);
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(LoginVM loginVM)
		{
			if(ModelState.IsValid)
			{
				var user = new IdentityUser() { UserName = loginVM.Login };
				var result = await _userManager.CreateAsync(user, loginVM.Password);

				if(result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "user");
					return RedirectToAction("Index", "Home");
				}
				var tmpUser = await _userManager.FindByNameAsync(user.UserName);
				if (tmpUser != null)
					if (tmpUser.UserName == user.UserName)
					{
					ModelState.AddModelError("", "Taki użytkownik już istnieje");
					}
			}
			return View(loginVM);
		}

		[HttpPost]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

	}
}
