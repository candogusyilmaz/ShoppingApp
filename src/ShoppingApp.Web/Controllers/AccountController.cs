using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace ShoppingApp.Web.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [Route("/login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/login")]
    public async Task<IActionResult> Login(UserLoginDto user)
    {
        if (!ModelState.IsValid)
            RedirectToAction(nameof(Login));

        User result = await _userService.ValidateLogin(user.Email, user.Password);

        if (result is not null)
        {
            await HttpContext.SignOutAsync();

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, result.Email),
                new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                new Claim(ClaimTypes.GivenName, result.FirstName),
                new Claim(ClaimTypes.Surname, result.LastName),
                new Claim(ClaimTypes.Role, result.Role),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal, new AuthenticationProperties { IsPersistent = true });
        }
        else
        {
            ModelState.AddModelError("InvalidLogin", "There are no users with the specified email and password.");
            return View(user);
        }

        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    [Route("/register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/register")]
    public async Task<IActionResult> Register(UserRegisterDto user)
    {
        if (!ModelState.IsValid)
            RedirectToAction(nameof(Register));

        try
        {
            await _userService.Create(new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Role = UserRoles.Basic
            });

            return RedirectToAction(nameof(Login));
        }
        catch (ArgumentException ex)
        {
            ModelState.AddModelError("RegisterModelError", ex.Message);
            return View(user);
        }
    }

    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction(nameof(Login));
    }
}
