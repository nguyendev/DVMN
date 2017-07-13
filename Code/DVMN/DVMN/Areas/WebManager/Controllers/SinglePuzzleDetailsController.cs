using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Services;
using DVMN.Models;
using Newtonsoft.Json;
using DVMN.Data;
using Microsoft.AspNetCore.Identity;

namespace DVMN.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    public class SinglePuzzleDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Member> _userManager;
        public SinglePuzzleDetailsController(
            ApplicationDbContext context,
            UserManager<Member> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var multiPuzzle =  HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");
            var listSinglePuzzleDetails = HttpContext.Session.GetObjectFromJson <List<Temp>>("listSinglePuzzleDetails");
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
            return View();
        }
        [HttpPost]
        [Route("sendData")]
        public IActionResult SaveTemp(Temp temp,string id)
        {
            var oldListSinglePuzzleDetails = HttpContext.Session.GetObjectFromJson<List<Temp>>("listSinglePuzzleDetails");
            var multiPuzzle = HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");

            //luu gia tri moi vao session
            List<Temp> newListSinglePuzzleDetails = new List<Temp>(multiPuzzle.NumberQuestion-1);
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
                }
                newListSinglePuzzleDetails.Add(item);
            }
            HttpContext.Session.SetObjectAsJson("listSinglePuzzleDetails", newListSinglePuzzleDetails);
            ViewData["listSinglePuzzleDetails"] = newListSinglePuzzleDetails.ToList();

            var serializedJsonModel = JsonConvert.SerializeObject(newListSinglePuzzleDetails.ToList());
            return Json(serializedJsonModel);
        }

   
       
    }
}