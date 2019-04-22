using Microsoft.AspNetCore.Mvc;

namespace Literature.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "A description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "A contact page.";

      return View();
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}