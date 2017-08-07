using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Data;
using Microsoft.AspNetCore.Identity;
using DVMN.Models;
using DVMN.Models.MemberViewModels;

namespace DVMN.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _repository;
        private readonly UserManager<Member> _userManager;
        private readonly ISidebarRepository _sidebarRepository;

        public MemberController(IMemberRepository repository,
            ISidebarRepository sidebarRepository,
            UserManager<Member> userManager)
        {
            _repository = repository;
            _sidebarRepository = sidebarRepository;
            _userManager = userManager;
        }

        [Route("/thanh-vien/")]
        public async Task<IActionResult> Index(int? page)
        {
            var member = await _repository.GetAllMember(page, 10);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(member);
        }
        [Route("/thanh-vien/{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var profile = await _repository.GetProfile(slug);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(profile);
        }
        [Route("/thanh-vien/{slug}/lich-su-cau-do-moi-ngay")]
        public async Task<IActionResult> UserHistoryListSinglePuzzle(string slug, int? page)
        {
            var profile = await _repository.GetHistoryListSinglePuzzle(slug,page,10);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(profile);
        }

        [Route("/thanh-vien/{slug}/lich-su-cau-do-dac-biet")]
        public async Task<IActionResult> UserHistoryListMultiPuzzle(string slug, int? page)
        {
            var profile = await _repository.GetHistoryListMultiPuzzle(slug, page, 10);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(profile);
        }

        [Route("/thanh-vien/bang-xep-hang/")]
        public async Task<IActionResult> TopMember(int? page)
        {
            var topMember = await _repository.GetTopMember(page, 10);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(topMember);
        }

        [Route("/thanh-vien/{slug}/chinh-sua")]
        public async Task<IActionResult> EditProfile(string slug)
        {
            var currentUser = await GetCurrentUser();
            bool IsCurrentUser = await _repository.IsCurrentUser(currentUser.Id, slug);
            if (!IsCurrentUser)
                return NotFound();

            var editProfile = await _repository.GetEditProfile(currentUser.Id);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(editProfile);
        }

        [HttpPost]
        [Route("/thanh-vien/{slug}/chinh-sua")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserEditProfileViewModel model, string slug)
        {
            if(ModelState.IsValid)
            { 
                await _repository.SaveEditProfile(model);
            }
            return RedirectToAction("Index","Home");

        }


        private async Task<Member> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

    }
}