using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entity;
using TestProject.Entity.Enum;
using TestProject.Service;
using TestProject.ViewModels;

namespace TestProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly AccountService _accountService;
        private readonly SignInManager<User> _signInManager;

        public AccountController(AccountService accountService, SignInManager<User> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            TempData.Clear();
            return View();
        }

        public IActionResult Register()
        {
            TempData.Clear();
            return View("Registration");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var validationError = _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                TempData.Keep("Error");
                return View();
            }

            var (isAuthenticated, user) = await _accountService.CheckUserAsync(loginViewModel.Email, loginViewModel.Password);

            if (!isAuthenticated)
            {
                TempData["Error"] = "Email or password is incorrect";
                return View();
            }

            await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
            return RedirectToAction("Index", "Task");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model, string role)
        {
            var validationError = _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
                if (validationError != "success")
                {
                    TempData["Error"] = validationError;
                    return View("Registration");
                }

                var isAuthenticated = await _accountService.CheckUser(model.Email);

                if (isAuthenticated)
                {
                    TempData["Error"] = "User with this email already exists";
                    return View("Registration");
                }

                await _accountService.RegisterUser(model);

                ViewBag.Success = "Registration successful! You will be redirected to the login page in 3 seconds.";

                return View("Registration");
        }

        [HttpGet]
        public async Task<RedirectToActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
