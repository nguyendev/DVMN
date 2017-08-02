using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVMN.Models.PuzzleViewModels;
using Microsoft.EntityFrameworkCore;
using DVMN.Services;
using DVMN.Models;

namespace DVMN.Data
{
    public class PuzzleRepository : IPuzzleRepository
    {
        private readonly ApplicationDbContext _context;
        public PuzzleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task IsWatchedSingleSinglePuzzle(string slug)
        {
            var item = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.Slug == slug);
            if (item != null)
            {
                item.Views++;
                _context.SinglePuzzle.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SingleSinglePuzzleViewModel> GetSingleSinglePuzzle(string slug)
        {
            var single = await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .SingleOrDefaultAsync(p => p.Slug == slug);
            var tags = await _context.SingPuzzleTag
               .Include(p => p.SinglePuzzle)
               .Include(p => p.Tag)
               .Where(p => p.SinglePuzzleID == single.ID)
               .ToListAsync();

           
            SingleSinglePuzzleViewModel model = new SingleSinglePuzzleViewModel
            {
                ID = single.ID,
                ImageID = single.ImageID,
                IsYesNo = single.IsYesNo,
                Level = single.Level,
                Like = single.Like,
                Reason = single.Reason,
                Slug = single.Slug,
                Views = single.Views,
                Title = single.Title,
                Tags = tags,
                AnswerA = single.AnswerA,
                AnswerB = single.AnswerB,
                AnswerC = single.AnswerC,
                AnswerD = single.AnswerD,
                Author = single.Author,
                Correct = single.Correct,
                Description = single.Description,
                Image = single.Image,
                DateTime = DateTimeExtension.CurrentDay(single.CreateDT.Value)
            };
            return model;
        }

        public async Task IsWatchedSingleMultiPuzzle(string slug)
        {
            var item = await _context.MultiPuzzle.SingleOrDefaultAsync(p => p.Slug == slug);
            if (item != null)
            {
                item.Views++;
                _context.MultiPuzzle.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsAnswerPuzzle(int PuzzleID, string UserID)
        {
            try
            {
                var result = await _context.HistoryAnswerPuzzle
                    .Include(p => p.Author)
                    .SingleAsync(p => p.PuzzleID == PuzzleID && p.Author.Id == UserID);

                return true;
            }
            catch { return false; }
        }

        public async Task IncreasePoint(string userID, int point)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.Id == userID);
            user.Score += point;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task SetIsAnsweredPuzzle(int PuzzleID, string AuthorID)
        {
            _context.HistoryAnswerPuzzle.Add(new Models.HistoryAnswerPuzzle
            {
                 Active = "A",
                 Approved = "A",
                 AuthorID = AuthorID,
                 CreateDT = DateTime.Now,
                 IsDeleted = false,
                 PuzzleID = PuzzleID,
                 UpdateDT = DateTime.Now,
            });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SingleSinglePuzzleViewModel>> GetSingleMultiPuzzle(string slug)
        {
            var multi = await _context.MultiPuzzle.SingleOrDefaultAsync(p => p.Slug == slug);
            //multi.IsAnswered = await _repository.IsAnswerPuzzle(multi.ID, true);
            var listSingle = await _context.SinglePuzzle.Where(p => p.MMultiPuzzleID == multi.ID).ToListAsync();

            List<SingleSinglePuzzleViewModel> model = new List<SingleSinglePuzzleViewModel>(listSingle.Capacity - 1);
            foreach (var item in listSingle)
            {
                List<SinglePuzzleTag> tags = null;
                try
                {
                    tags = await _context.SingPuzzleTag
                       .Include(p => p.SinglePuzzle)
                       .Include(p => p.Tag)
                       .Where(p => p.SinglePuzzleID == item.ID)
                       .ToListAsync();
                }
                catch { }

                SingleSinglePuzzleViewModel temp = new SingleSinglePuzzleViewModel
                {
                    ID = item.ID,
                    ImageID = item.ImageID,
                    IsYesNo = item.IsYesNo,
                    Level = item.Level,
                    Like = item.Like,
                    Reason = item.Reason,
                    Slug = item.Slug,
                    Views = item.Views,
                    Title = item.Title,
                    AnswerA = item.AnswerA,
                    AnswerB = item.AnswerB,
                    AnswerC = item.AnswerC,
                    AnswerD = item.AnswerD,
                    Author = item.Author,
                    Correct = item.Correct,
                    Description = item.Description,
                    Image = item.Image,
                    DateTime = DateTimeExtension.CurrentDay(item.CreateDT.Value)
                };
                if (tags != null)
                    temp.Tags = tags;
                model.Add(temp);
            }
            return model;
        }
    }
}
