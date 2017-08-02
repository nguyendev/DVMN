using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Data;
using Microsoft.EntityFrameworkCore;

namespace DVMN.Controllers
{
    [ResponseCache(Duration = 30)]
    public class HomeController : Controller
    {
        private readonly IHomeRepository _repository;
        private readonly ISidebarRepository _sidebarRepository;

        public HomeController(IHomeRepository repository,
            ISidebarRepository sidebarRepository)
        {
            _repository = repository;
            _sidebarRepository = sidebarRepository;
        }

        [ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> Index()
        {
            //ViewData["listPuzzleNormal"] = await _context.MultiPuzzle.ToListAsync();
            ViewData["listSinglePuzzle"] = await _repository.GetIndex();
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View();
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
