using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DVMN.Data;
using DVMN.Models;

namespace DVMN.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    public class SinglePuzzlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SinglePuzzlesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: WebManager/SinglePuzzles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SinglePuzzle.Include(s => s.Image).Include(s => s.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WebManager/SinglePuzzles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singlePuzzle = await _context.SinglePuzzle
                .Include(s => s.Image)
                .Include(s => s.Member)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (singlePuzzle == null)
            {
                return NotFound();
            }

            return View(singlePuzzle);
        }

        // GET: WebManager/SinglePuzzles/Create
        public IActionResult Create()
        {
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID");
            ViewData["MemberID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: WebManager/SinglePuzzles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Slug,ImageID,IsYesNo,AnswerA,AnswerB,AnswerC,AnswerD,Correct,Reason,Like,Level,IsMMultiPuzzle,MMultiPuzzleID,CreateDT,UpdateDT,MemberID,Approved,Active,IsDeleted,Note")] SinglePuzzle singlePuzzle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(singlePuzzle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID", singlePuzzle.ImageID);
            ViewData["MemberID"] = new SelectList(_context.Users, "Id", "Id", singlePuzzle.MemberID);
            return View(singlePuzzle);
        }

        // GET: WebManager/SinglePuzzles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singlePuzzle = await _context.SinglePuzzle.SingleOrDefaultAsync(m => m.ID == id);
            if (singlePuzzle == null)
            {
                return NotFound();
            }
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID", singlePuzzle.ImageID);
            ViewData["MemberID"] = new SelectList(_context.Users, "Id", "Id", singlePuzzle.MemberID);
            return View(singlePuzzle);
        }

        // POST: WebManager/SinglePuzzles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Slug,ImageID,IsYesNo,AnswerA,AnswerB,AnswerC,AnswerD,Correct,Reason,Like,Level,IsMMultiPuzzle,MMultiPuzzleID,CreateDT,UpdateDT,MemberID,Approved,Active,IsDeleted,Note")] SinglePuzzle singlePuzzle)
        {
            if (id != singlePuzzle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(singlePuzzle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinglePuzzleExists(singlePuzzle.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ImageID"] = new SelectList(_context.Images, "ID", "ID", singlePuzzle.ImageID);
            ViewData["MemberID"] = new SelectList(_context.Users, "Id", "Id", singlePuzzle.MemberID);
            return View(singlePuzzle);
        }

        // GET: WebManager/SinglePuzzles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singlePuzzle = await _context.SinglePuzzle
                .Include(s => s.Image)
                .Include(s => s.Member)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (singlePuzzle == null)
            {
                return NotFound();
            }

            return View(singlePuzzle);
        }

        // POST: WebManager/SinglePuzzles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var singlePuzzle = await _context.SinglePuzzle.SingleOrDefaultAsync(m => m.ID == id);
            _context.SinglePuzzle.Remove(singlePuzzle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SinglePuzzleExists(int id)
        {
            return _context.SinglePuzzle.Any(e => e.ID == id);
        }
    }
}
