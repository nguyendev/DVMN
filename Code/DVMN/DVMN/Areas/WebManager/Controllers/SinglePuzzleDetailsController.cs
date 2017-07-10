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
        [HttpPost]
        public string Create(SinglePuzzleDetails model, string id)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetObjectAsJson("cau" + id, model);
                return "1";
            }
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