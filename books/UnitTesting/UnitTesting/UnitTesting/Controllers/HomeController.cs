using Microsoft.AspNetCore.Mvc;
using UnitTesting.Models;

namespace UnitTesting.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
             => View(SimpleRepository.SharedRepository.Products);
    }
}
