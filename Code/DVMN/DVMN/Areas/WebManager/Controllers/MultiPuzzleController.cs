using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Services;
using DVMN.Models;

namespace DVMN.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    public class MultiPuzzleController : Controller
    {
        private readonly IMultiPuzzle _context;
        public MultiPuzzleController(IMultiPuzzle context)
        {
            _context = context;
        }
        [Route("/quan-ly-web/nhieu-cau-hoi")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAll());
        }
        [Route("/quan-ly-web/nhieu-cau-hoi/tao")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("/quan-ly-web/nhieu-cau-hoi/tao")]
        [HttpPost]
        public IActionResult Create([Bind("Title,Description,NumberQuestion,Image,Slug")] MultiPuzzle model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetObjectAsJson("MultiPuzzle", model);
                ViewData["NumberQuestion"] = model.NumberQuestion;
                return View("~/Areas/WebManager/Views/SinglePuzzleDetails/Create.cshtml");
            }
            return View(model);
        }
    }
}