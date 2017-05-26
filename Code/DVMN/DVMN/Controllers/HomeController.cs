using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DVMN.Controllers
{
    [ResponseCache(Duration = 30)]
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            return View("Index1");
        }
        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        [ResponseCache(Duration = 60)]

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
