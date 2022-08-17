using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Categoris.Models;

namespace Categoris.Controllers;

public class HomeController : Controller
{
    public IActionResult Privacy()
    {
        return View();
    }

}
