using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MVC.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMemberService _memberService;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HomeController(
        IMemberService memberService,
        ILogger<HomeController> logger,
        IUserService userService,
        IHttpContextAccessor contextAccessor)
    {
        _memberService = memberService;
        _logger = logger;
        _userService = userService;
        _httpContextAccessor = contextAccessor;
    }

    public IActionResult Index()
    {
        var memberViewModel = new MemberListViewModel {
            Members = _memberService.GetMembers()
                                    .OrderBy(member => member.DOB)
                                    .ToList()
        };
        return View("Index", memberViewModel);
    }

    [HttpGet]
    public IActionResult AddNewMember()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddNewMember(Member member)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError(string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)));
            return View(member);
        }

        _memberService.AddMember(member);

        return Index();
    }

    public IActionResult GetMember(string memberID)
    {
        var member = _memberService.GetMemberByID(memberID);

        return View(member);
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel { returnUrl = returnUrl });
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel loginDetails)
    {
        var isValidUser = _userService.IsUserValid(loginDetails.username, loginDetails.password);

        if (isValidUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginDetails.username),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var identity = new ClaimsIdentity(
                claims,
                "default",
                ClaimTypes.Name,
                ClaimTypes.Role
            );

            var principal = new ClaimsPrincipal(identity);
            var authProps = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddHours(1),
                IsPersistent = true,
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authProps
            );

            if (String.IsNullOrEmpty(loginDetails.returnUrl))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(loginDetails.returnUrl);
        }

        return View(new LoginViewModel() { returnUrl = loginDetails.returnUrl, username = loginDetails.username });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
