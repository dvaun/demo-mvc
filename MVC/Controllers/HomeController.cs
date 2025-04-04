using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using Data.Models;

namespace MVC.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IMemberService _memberService;

    public HomeController(IMemberService memberService, ILogger<HomeController> logger)
    {
        _memberService = memberService;
        _logger = logger;
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
