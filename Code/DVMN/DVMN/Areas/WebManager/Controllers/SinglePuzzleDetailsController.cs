using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Services;
using DVMN.Models;

namespace DVMN.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    public class SinglePuzzleDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var multiPuzzle =  HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");
            ViewData["NumberQuestion"] = multiPuzzle.NumberQuestion;
            return View();
        }
        [HttpPost]

        public string Create(Temp temp)
        {
            
                //HttpContext.Session.SetObjectAsJson("cau" + id, model);
            return "1";
            
            //var MultiPuzzle= HttpContext.Session.GetObjectFromJson<MultiPuzzle>("MultiPuzzle");
            //MultiPuzzle.NumberQuestion;
            //HttpContext.Session.SetObjectAsJson("")

            return "0";
        }

        //public IActionResult SavePuzzleToSession([Bind("Title", "Description", "Image", "IsYesNo", "AnswerA", "AnswerB", "AnswerC", "AnswerD", "Correct")] SinglePuzzleDetails model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        HttpContext.Session.SetObjectAsJson("")
        //    }
        //}
    }
}