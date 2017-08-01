using DVMN.Areas.WebManager.ViewModels.SinglePuzzleViewModels;
using DVMN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public class SinglePuzzleRepository : ISinglePuzzle
    {
        protected readonly ApplicationDbContext _context;

        public SinglePuzzleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(CreateSinglePuzzleViewModel model)
        {
            _context.Add(new SinglePuzzle
            {
                CreateDT = DateTime.Now,
                Approved = "A",
                IsDeleted = false,
                Active = "A",
                AnswerA = model.AnswerA,
                AnswerB = model.AnswerB,
                AnswerC = model.AnswerC,
                AnswerD = model.AnswerD,
                Correct = model.Correct,
                Title = model.Title,
                Slug = model.Slug,
                IsYesNo = model.IsYesNo,
                Like = 0,
                Reason = model.Reason,
                Note = model.Note,
                Level = 0,
                Description = model.Description,
                ImageID = model.ImageID,
                AuthorID = model.AuthorID,
                
            });
            await Save();
        }

        public async Task<SinglePuzzle> Get(int? id)
        {
            return await _context.SinglePuzzle.SingleOrDefaultAsync(c => c.ID == id);
        }
        public bool Exists(int id)
        {
            return _context.SinglePuzzle.Any(c => c.ID == id);
        }

        public async Task<List<SinglePuzzle>> GetAll()
        {
            return await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author).ToListAsync();
        }

        public async Task Update(SinglePuzzle Entity)
        {
            _context.SinglePuzzle.Update(Entity);
            await Save();
        }

        public async Task Delete(int id)
        {
            var temp = await _context.SinglePuzzle.SingleOrDefaultAsync(m => m.ID == id);
            _context.SinglePuzzle.Remove(temp);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
