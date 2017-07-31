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

namespace DVMN.Controllers
{
    public class PuzzleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<Member> _signInManager;
        private readonly UserManager<Member> _userManager;

        public PuzzleController(ApplicationDbContext _context,
            SignInManager<Member> _signInManager,
            UserManager<Member> _userManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            this._context = _context;
        }
        [Route("/cau-hoi-don/{slug}")]
        public async Task<IActionResult> SingleSinglePuzzle(string slug, string returnUrl = null)
        {
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.Slug == slug);
            HttpContext.Session.SetInt32("Id", single.ID);
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                string path = HttpContext.Request.Path.ToString();
                HttpContext.Session.SetString("currentUrl", path);
            }
            return View(single);
        }
        [Route("/cau-hoi-don/{slug}")]
        public async Task<IActionResult> SingleMultiPuzzle(string slug, string returnUrl = null)
        {
            var multi = await _context.MultiPuzzle.SingleOrDefaultAsync(p => p.Slug == slug);
            HttpContext.Session.SetInt32("Id", multi.ID);
            if (!_signInManager.IsSignedIn(HttpContext.User))
            {
                string path = HttpContext.Request.Path.ToString();
                HttpContext.Session.SetString("currentUrl", path);
            }
            return View(multi);
        }


        [HttpPost]
        public async Task<bool> checkAnswer(int select)
        {
            int Id = HttpContext.Session.GetInt32("Id").Value;
            var single = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == Id);
            if (single.Correct == select)
            {
                return true;
            }
            return false;
        }
    }
}