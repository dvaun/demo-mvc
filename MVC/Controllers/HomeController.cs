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
        // var members = new List<Member> {
        //     new Member
        //     {
        //         FirstName = "Willie",
        //         LastName = "Nelson",
        //         Address = null,
        //         PhoneNumber = "555-9012",
        //         DOB = new DateTime(1933, 4, 29),
        //         MembershipID = "FL1234"
        //     },
        //     new Member
        //     {
        //         FirstName = "Jayden",
        //         LastName = "Smith",
        //         Address = "ABC Hollywood Ln",
        //         PhoneNumber = "279-1234",
        //         DOB = new DateTime(1999, 7, 8),
        //         MembershipID = "XYZ2345"
        //     },
        //     new Member
        //     {
        //         FirstName = "Bill",
        //         LastName = "Gates",
        //         Address = "Some Rich Road",
        //         PhoneNumber = "123-4567",
        //         DOB = new DateTime(1956, 10, 28),
        //         MembershipID = "RICH1234"
        //     },
        //     new Member
        //     {
        //         FirstName = "David",
        //         LastName = "Lynch",
        //         Address = "High Heaven Blvd",
        //         PhoneNumber = "987-6543",
        //         DOB = new DateTime(1946, 1, 20),
        //         MembershipID = "HEAV1234"
        //     }
        // };
        var memberViewModel = new MemberListViewModel {
            Members = _memberService.GetMembers().ToList()
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
