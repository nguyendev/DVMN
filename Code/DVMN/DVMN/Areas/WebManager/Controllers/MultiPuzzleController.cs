using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Services;
using DVMN.Models;
using Microsoft.AspNetCore.Authorization;
using DVMN.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace DVMN.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    [Authorize]
    public class MultiPuzzleController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IMultiPuzzle _multiPuzzle;
        private readonly UserManager<Member> _userManager;
        public MultiPuzzleController(
            ApplicationDbContext context,
            UserManager<Member> userManager,
            IMultiPuzzle multiPuzzle)
        {
            _context = context;
            _userManager = userManager;
            _multiPuzzle = multiPuzzle;
        }
        [Route("/quan-ly-web/nhieu-cau-hoi")]
        public async Task<IActionResult> Index()
        {
            return View(await _multiPuzzle.GetAll());
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
                
                return RedirectToAction("Create", "SinglePuzzleDetails");
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAll()
        {
            try
            {
                var sMultiPuzzle = HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");
                var sListSinglePuzzleDetails = HttpContext.Session.GetObjectFromJson<List<Temp>>("listSinglePuzzleDetails");
                var user = await GetCurrentUserAsync();
                MultiPuzzle multipuzzle = new MultiPuzzle
                {
                    Active = "1",
                    Approved = "1",
                    AuthorID = user.Id,
                    CreateDT = DateTime.Now,
                    Description = sMultiPuzzle.Description,
                    Image = sMultiPuzzle.Image,
                    IsDeleted = false,
                    Like = 0,
                    UpdateDT = DateTime.Now,
                    Title = sMultiPuzzle.Title,
                    NumberQuestion = sMultiPuzzle.NumberQuestion,
                    Slug = sMultiPuzzle.Slug,
                    Note = "",
                    Level = 1
                };
                List<SinglePuzzleDetails> listSinglePuzzleDetails = new List<SinglePuzzleDetails>(multipuzzle.NumberQuestion - 1);
                _context.Add(multipuzzle);
                foreach (var item in sListSinglePuzzleDetails)
                {
                    SinglePuzzleDetails singlePuzzleDetails = new SinglePuzzleDetails
                    {
                        Active = multipuzzle.Active,
                        Note = multipuzzle.Note,
                        Title = item.Title,
                        Description = item.Description,
                        IsYesNo = item.IsYesNo,
                        MultiPuzzleID = multipuzzle.ID,
                        AnswerA = item.AnswerA,
                        AnswerB = item.AnswerB,
                        AnswerC = item.AnswerC,
                        AnswerD = item.AnswerD,
                        Correct = item.Correct,
                        Image = item.Image,
                        IsDeleted = false,
                        CreateDT = DateTime.Now,
                        Approved = "A",
                    };
                    _context.Add(singlePuzzleDetails);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                string error = e.StackTrace;
            }
            return RedirectToAction("Index", "MultiPuzzle");
        }
        private Task<Member> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
    
}