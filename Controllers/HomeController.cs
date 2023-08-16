using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using l03_http.Models;

namespace l03_http.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

public IActionResult Index()
{
    // Get existing cookie from the Request, if it exists
    var requestCookie = Request.Cookies["myCookie"];
    var cookieString = "";

    // If myCookie does not exist (or has expired)
    if (string.IsNullOrWhiteSpace(requestCookie)) {
        // Set "no cookie" message on cookie string
        cookieString = "No cookies for you";

        // Set up new cookie value and expiration
        string newCookieVal = DateTime.Now.ToString("G");
        var cookieExpiration = DateTime.Now + TimeSpan.FromMinutes(1);

        // Send new cookie back to the browser on the Response
        Response.Cookies.Append("myCookie", newCookieVal, new CookieOptions {
            Expires = cookieExpiration
        });

    } else {
        // If cookie does exist, store it in cookieString
        cookieString = requestCookie;
    }
    // Pass cookie string to View via ViewBag
    ViewBag.CookieValue = cookieString;

    return View();
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
