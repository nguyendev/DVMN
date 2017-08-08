using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoVuiHaiNao.Services;
using DoVuiHaiNao.Models;
using Microsoft.AspNetCore.Authorization;
using DoVuiHaiNao.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoVuiHaiNao.Areas.WebManager.Controllers
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
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "Name");
            return View();
        }
        [Route("/quan-ly-web/nhieu-cau-hoi/tao")]
        [HttpPost]
        public IActionResult Create(MultiPuzzle model)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetObjectAsJson("MultiPuzzle", model);
                
                return RedirectToAction("CreateMulti");
            }
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "Name",model.ImageID);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAll()
        {
            var sMultiPuzzle = HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");
            var sListSinglePuzzleDetails = HttpContext.Session.GetObjectFromJson<List<Temp>>("listSinglePuzzleDetails");
            var user = await GetCurrentUserAsync();
            MultiPuzzle multipuzzle = new MultiPuzzle
            {
                Active = "1",
                Approved = "1",
                ImageID = sMultiPuzzle.ImageID,
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
                Note = sMultiPuzzle.Note,
                Level = 1
            };
            List<SinglePuzzle> listSinglePuzzleDetails = new List<SinglePuzzle>(multipuzzle.NumberQuestion - 1);
            await _context.MultiPuzzle.AddAsync(multipuzzle);
            await _context.SaveChangesAsync();
            foreach (var item in sListSinglePuzzleDetails)
            {
                SinglePuzzle singlePuzzleDetails = new SinglePuzzle
                {
                    ImageID = item.Image,
                    IsMMultiPuzzle = true,
                    Reason = item.Reason,
                    AuthorID = (await GetCurrentUserAsync()).Id,
                    Active = multipuzzle.Active,
                    Note = multipuzzle.Note,
                    Title = item.Title,
                    Description = item.Description,
                    IsYesNo = item.IsYesNo,
                    MMultiPuzzleID = multipuzzle.ID,
                    AnswerA = item.AnswerA,
                    AnswerB = item.AnswerB,
                    AnswerC = item.AnswerC,
                    AnswerD = item.AnswerD,
                    Correct = item.Correct,
                    IsDeleted = false,
                    CreateDT = DateTime.Now,
                    UpdateDT = DateTime.Now,
                    Approved = "A",
                };
                if (singlePuzzleDetails.ImageID == 0)
                    singlePuzzleDetails.ImageID = null;
                _context.SinglePuzzle.Add(singlePuzzleDetails);
                List<string> listString = StringExtensions.ConvertStringToListString(item.TempTag);
                List<Tag> listTag = new List<Tag>(listString.Capacity - 1);

                // Save all tag
                foreach (var itemTag in listString)
                {
                    bool IsExitsTag = await _context.Tag.AnyAsync(p => p.Slug == StringExtensions.ConvertToUnSign3(itemTag));
                    Tag tag;
                    if (IsExitsTag)
                    {
                        tag = await _context.Tag.SingleOrDefaultAsync(p => p.Slug == StringExtensions.ConvertToUnSign3(itemTag));
                    }
                    else
                    {
                        tag = new Tag
                        {
                            Title = itemTag,
                            Slug = StringExtensions.ConvertToUnSign3(itemTag)
                        };
                        _context.Tag.Add(tag);
                    }
                     _context.SingPuzzleTag.Add(new SinglePuzzleTag { TagID = tag.ID, SinglePuzzleID = singlePuzzleDetails.ID });
                     await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "MultiPuzzle");
        }

        public IActionResult CreateMulti()
        {
            var multiPuzzle = HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");
            var listSinglePuzzleDetails = HttpContext.Session.GetObjectFromJson<List<Temp>>("listSinglePuzzleDetails");
            if (listSinglePuzzleDetails == null)
            {

                List<Temp> list = new List<Temp>(multiPuzzle.NumberQuestion - 1);
                for (int i = 0; i <= multiPuzzle.NumberQuestion - 1; i++)
                {
                    list.Add(new Temp { ID = i });
                }
                ViewData["listSinglePuzzleDetails"] = list.ToList();
                HttpContext.Session.SetObjectAsJson("listSinglePuzzleDetails", list);
            }
            else
            {
                ViewData["listSinglePuzzleDetails"] = listSinglePuzzleDetails.ToList();
            }
            ViewData["NumberQuestion"] = multiPuzzle.NumberQuestion;
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "Name");
            return View();
        }
        [HttpPost]
        [Route("sendData")]
        public IActionResult SaveTemp(Temp temp, string id)
        {
            var oldListSinglePuzzleDetails = HttpContext.Session.GetObjectFromJson<List<Temp>>("listSinglePuzzleDetails");
            var multiPuzzle = HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");

            //luu gia tri moi vao session
            List<Temp> newListSinglePuzzleDetails = new List<Temp>(multiPuzzle.NumberQuestion - 1);
            foreach (var item in oldListSinglePuzzleDetails)
            {
                if (item.ID == Int32.Parse(id))
                {
                    item.Title = temp.Title;
                    item.Description = temp.Description;
                    item.Image = temp.Image;
                    item.AnswerA = temp.AnswerA;
                    item.AnswerB = temp.AnswerB;
                    item.AnswerC = temp.AnswerC;
                    item.AnswerD = temp.AnswerD;
                    item.Correct = temp.Correct;
                    item.IsYesNo = temp.IsYesNo;
                    item.TempTag = temp.TempTag;
                }
                newListSinglePuzzleDetails.Add(item);
            }
            HttpContext.Session.SetObjectAsJson("listSinglePuzzleDetails", newListSinglePuzzleDetails);
            ViewData["listSinglePuzzleDetails"] = newListSinglePuzzleDetails.ToList();

            var serializedJsonModel = JsonConvert.SerializeObject(newListSinglePuzzleDetails.ToList());
            return Json(serializedJsonModel);
        }


        private async Task<Member> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
    
}