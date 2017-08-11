using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoVuiHaiNao.Data;

namespace DoVuiHaiNao.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _repository;
        private readonly ISidebarRepository _sidebarRepository;

        public TagController(ITagRepository repository,
            ISidebarRepository sidebarRepository)
        {
            _repository = repository;
            _sidebarRepository = sidebarRepository;
        }
        [Route("/the/{slug}")]
        public async Task<IActionResult> Single(string slug, int? page)
        {
            var applicationDbContext = await _repository.GetSingle(slug, page, 10);
            if (applicationDbContext == null)
                return NotFound();
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(applicationDbContext);
        }
    }
}