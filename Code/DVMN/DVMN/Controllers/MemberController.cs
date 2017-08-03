using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DVMN.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/thanh-vien/{slug}")]
        public IActionResult Member(string slug)
        {
            return View();
        }
    }
}