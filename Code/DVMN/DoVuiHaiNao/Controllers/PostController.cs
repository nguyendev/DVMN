using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Areas.WebManager.Data;
using DoVuiHaiNao.Data;

namespace DoVuiHaiNao.Controllers
{
    public class PostController : Controller
    {
        private readonly SignInManager<Member> _signInManager;
        private readonly UserManager<Member> _userManager;
        private readonly IPostRepository _repository;
        private readonly ISidebarRepository _sidebarRepository;
        private const int POINT = 10;

        public PostController(ApplicationDbContext _context,
            SignInManager<Member> _signInManager,
            UserManager<Member> _userManager,
            IPostRepository _repository,
            ISidebarRepository _sidebarRepository)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._repository = _repository;
            this._sidebarRepository = _sidebarRepository;
        }
        [Route("/blogs/")]
        public async Task<IActionResult> Index(int? page)
        {
            var single = await _repository.GetListPost(page, 10);
            if (single == null)
                return NotFound();
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(single);
        }


        [Route("/blogs/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            var single = await _repository.GetSinglePost(slug);
            if (single == null)
                return NotFound();
            // increase View
            await _repository.IncreaseView(slug);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(single);
        }
    }
}