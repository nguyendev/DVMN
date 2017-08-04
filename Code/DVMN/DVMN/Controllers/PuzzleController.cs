using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DVMN.Data;
using Microsoft.EntityFrameworkCore;
using DVMN.Models;
using Microsoft.AspNetCore.Identity;
using DVMN.Models.PuzzleViewModels;

namespace DVMN.Controllers
{
    public class PuzzleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<Member> _signInManager;
        private readonly UserManager<Member> _userManager;
        private readonly IPuzzleRepository _repository;
        private readonly ISidebarRepository _sidebarRepository;
        private const int POINT = 10;

        public PuzzleController(ApplicationDbContext _context,
            SignInManager<Member> _signInManager,
            UserManager<Member> _userManager,
            IPuzzleRepository _repository,
            ISidebarRepository _sidebarRepository)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._context = _context;
            this._repository = _repository;
            this._sidebarRepository = _sidebarRepository;
        }

        [Route("/cau-hoi-don")]
        public async Task<IActionResult> ListSinglePuzzle()
        {
            var single = await _context.SinglePuzzle.Include(p => p.Image)
                .Where(p => !p.IsMMultiPuzzle) .ToListAsync();
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(single);
        }

        [Route("/cau-hoi-da")]
        public async Task<IActionResult> ListMultiPuzzle()
        {
            var single = await _context.MultiPuzzle.Include(p => p.Image)
                .ToListAsync();
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(single);
        }


        [Route("/cau-hoi-don/{slug}")]
        public async Task<IActionResult> SingleSinglePuzzle(string slug)
        {
            SingleSinglePuzzleViewModel single = await _repository.GetSingleSinglePuzzle(slug);
            var user = await GetCurrentUser();

            
            HttpContext.Session.SetInt32("Id", single.ID);

            // save currentURL to return after login
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                string path = HttpContext.Request.Path.ToString();
                HttpContext.Session.SetString("currentUrl", path);
            }
            else
            {
                // Kiem tra thu da tra loi cau hoi nay chua
                single.IsAnswered = await _repository.IsAnswerPuzzle(single.ID, user.Id);
            }

            // increase View
            await _repository.IsWatchedSingleSinglePuzzle(slug);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(single);
        }
        [Route("/cau-hoi-da/{slug}")]
        public async Task<IActionResult> SingleMultiPuzzle(string slug)
        {
            MultiPuzzleViewModel listSingle = await _repository.GetSingleMultiPuzzle(slug);
            var user = await GetCurrentUser();

            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                string path = HttpContext.Request.Path.ToString();
                HttpContext.Session.SetString("currentUrl", path);
            }
            else
            {
                // Kiem tra thu da tra loi cau hoi nay chua
                foreach (var item in listSingle.listSinglePuzzle)
                {
                    item.IsAnswered = await _repository.IsAnswerPuzzle(item.ID, user.Id);
                }
            }

            await _repository.IsWatchedSingleMultiPuzzle(slug);
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View(listSingle);
        }


        [HttpPost]
        public async Task<bool> checkAnswer(int select)
        {
            int Id = HttpContext.Session.GetInt32("Id").Value;
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == Id);
            var user = await GetCurrentUser();

            await _repository.SetIsAnsweredPuzzle(Id, user.Id);
            if (single.Correct == select)
            {
                await _repository.IncreasePoint(user.Id, POINT);
                return true;
            }
            return false;
        }

        public async Task<Member> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
        [HttpPost]
        public async Task<bool> checkAnswerMulti(int select, int id)
        {
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == id);
            var user = await GetCurrentUser();

            await _repository.SetIsAnsweredPuzzle(id, user.Id);
            if (single.Correct == select)
            {
                await _repository.IncreasePoint(user.Id, POINT);
                return true;
            }
            return false;
        }

        public async Task<bool> VoteDown()
        {
            int Id = HttpContext.Session.GetInt32("Id").Value;
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == Id);
            var user = await GetCurrentUser();

            return await _repository.VoteDownPuzzle(Id, user.Id);
        }

        public async Task<bool> VoteUp()
        {
            int Id = HttpContext.Session.GetInt32("Id").Value;
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == Id);
            var user = await GetCurrentUser();

            return await _repository.VoteUpPuzzle(Id, user.Id);
        }

        public async Task<bool> VoteDownMulti(int id)
        {
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == id);
            var user = await GetCurrentUser();
            return await _repository.VoteDownPuzzle(id, user.Id);
        }

        public async Task<bool> VoteUpMulti(int id)
        {
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == id);
            var user = await GetCurrentUser();
            return await _repository.VoteUpPuzzle(id, user.Id);
        }
    }
        
}