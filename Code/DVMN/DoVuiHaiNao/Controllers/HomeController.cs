using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoVuiHaiNao.Data;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DoVuiHaiNao.Controllers
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
        [Route("")]

        //[ResponseCache(CacheProfileName = "Default")]
        public async Task<IActionResult> Index()
        {
            ViewData["listSinglePuzzle"] = await _repository.GetIndex();
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View();
        }
        
        //[Route("/ve-chung-toi")]
        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}
        [ResponseCache(Duration = 120)]
        [Route("/lien-he")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


        [Route("robots.txt", Name = "GetRobotsText")]
        public ContentResult RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("User-agent: *");
            stringBuilder.AppendLine("Disallow: /quan-ly-web/");
            stringBuilder.AppendLine("Disallow: /WebManager/");
            stringBuilder.AppendLine("Disallow: /wwwroot");
            stringBuilder.AppendLine("User-agent: Googlebot-Image");
            stringBuilder.AppendLine("Allow: /wwwroot/images");
            return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }
    }
}
