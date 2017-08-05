using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Data;

namespace DVMN.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _repository;
        private readonly ISidebarRepository _sidebarRepository;

        public MemberController(IMemberRepository repository,
            ISidebarRepository sidebarRepository)
        {
            _repository = repository;
            _sidebarRepository = sidebarRepository;
        }

        [Route("/thanh-vien/")]
        public async Task<IActionResult> Index(int? page)
        {
            var member = await _repository.GetAllMember(page, 10);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(member);
        }
        [Route("/thanh-vien/{slug}")]
        public IActionResult Details(string slug)
        {
            return View();
        }
        [Route("/thanh-vien/bang-xep-hang/")]
        public async Task<IActionResult> TopMember(int? page)
        {
            var topMember = await _repository.GetTopMember(page, 10);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(topMember);
        }
    }
}