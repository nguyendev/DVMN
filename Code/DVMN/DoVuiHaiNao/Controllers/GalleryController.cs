using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoVuiHaiNao.Data;

namespace DoVuiHaiNao.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryRepository _repository;
        private readonly ISidebarRepository _sidebarRepository;
        public GalleryController(IGalleryRepository repository,
            ISidebarRepository sidebarRepository)
        {
            _repository = repository;
            _sidebarRepository = sidebarRepository;
        }
        [Route("hinh-anh")]
        public async Task<IActionResult> Index(int? page)
        {
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            var gallery = await _repository.GetAll(page,50);
            return View(gallery);
        }
    }
}