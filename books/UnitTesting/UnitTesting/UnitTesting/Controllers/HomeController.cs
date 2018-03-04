using Microsoft.AspNetCore.Mvc;
using UnitTesting.Models;
using System.Linq;

namespace UnitTesting.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
             => View(SimpleRepository.SharedRepository.Products
                 .Where(p => p.Price < 50));
    }
}
