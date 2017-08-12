using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoVuiHaiNao.Models.PuzzleViewModels;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Services;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Extension;

namespace DoVuiHaiNao.Data
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
                .Where(p => !p.IsDeleted)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => p.Approved == Global.APPROVED)
                .SingleOrDefaultAsync(p => p.Slug == slug);
            var bestSingle = await _context.SinglePuzzle
                .Take(4)
                .Where(p => !p.IsMMultiPuzzle)
                .OrderByDescending(p => p.Like)
                .ToListAsync();
            List<SimplePostPuzzle> listbestPuzzle = new List<SimplePostPuzzle>(3);
            foreach(var item in bestSingle)
            {
                listbestPuzzle.Add(new SimplePostPuzzle { Slug = item.Slug, Title = item.Title });
            }
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
                Correct = ShowCorrectAnswer(single.Correct),
                Description = single.Description,
                Image = single.Image,
                DateTime = DateTimeExtension.CurrentDay(single.CreateDT.Value),
                RelatedPuzzle = listbestPuzzle
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
           return await _context.HistoryAnswerPuzzle.AnyAsync(p => p.PuzzleID == PuzzleID && p.AuthorID == UserID);
        }

        public async Task IncreasePoint(string userID, int point)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.Id == userID);
            user.Score += point;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task SetIsAnsweredPuzzle(int PuzzleID, string AuthorID, bool IsMulti)
        {
            bool IsExits = _context.HistoryAnswerPuzzle.Any(p => p.PuzzleID == PuzzleID && p.AuthorID == AuthorID);
            if (!IsExits)
            {
                _context.HistoryAnswerPuzzle.Add(new Models.HistoryAnswerPuzzle
                {
                    Active = "A",
                    Approved = "A",
                    AuthorID = AuthorID,
                    CreateDT = DateTime.Now,
                    IsDeleted = false,
                    PuzzleID = PuzzleID,
                    IsMultiPuzzle = IsMulti,
                    UpdateDT = DateTime.Now,
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SingleMultiPuzzleViewModel> GetSingleMultiPuzzle(string slug)
        {
            try
            {
                var multi = await _context.MultiPuzzle
                    .Where(p =>p.CreateDT <= DateTime.Now)
                    .Where(p => !p.IsDeleted)
                    .Where(p => p.Approved == Global.APPROVED)
                    .SingleOrDefaultAsync(p => p.Slug == slug);
                //multi.IsAnswered = await _repository.IsAnswerPuzzle(multi.ID, true);
                var bestSingle = await _context.MultiPuzzle
                    .Take(4)
                    .ToListAsync();
                List<SimplePostPuzzle> listbestPuzzle = new List<SimplePostPuzzle>(3);
                foreach (var item in bestSingle)
                {
                    listbestPuzzle.Add(new SimplePostPuzzle { Slug = item.Slug, Title = item.Title });
                }

                var listSingle = await _context.SinglePuzzle
                    .Include(p => p.Image)
                    .Include(p => p.Author)
                    .Where(p => p.MultiPuzzleID == multi.ID).ToListAsync();

                List<SingleSinglePuzzleViewModel> listSingleViewModel = new List<SingleSinglePuzzleViewModel>();
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
                        Correct = ShowCorrectAnswer(item.Correct),
                        Description = item.Description,
                        Image = item.Image,
                        DateTime = DateTimeExtension.CurrentDay(item.CreateDT.Value)
                    };
                    if (tags != null)
                        temp.Tags = tags;
                    listSingleViewModel.Add(temp);
                }
                SingleMultiPuzzleViewModel model = new SingleMultiPuzzleViewModel { listSinglePuzzle = listSingleViewModel, Title = multi.Title, ListbestPuzzle = listbestPuzzle, Description = SEOExtension.GetStringToLength(multi.Description, SEOExtension.MaxDescriptionSEO) };
                return model;
            }
            catch { }
            return null;
        }


        private int ShowCorrectAnswer(int number)
        {
            return number++;
        }

        public async Task<bool> VoteDownPuzzle(int puzzle, string UserId)
        {
            bool IsExists = await _context.HistoryLikePuzzle.AnyAsync(p => p.PuzzleID == puzzle && p.AuthorID == UserId);

            // neu chua vote lan nao thi tien hanh vote
            if(!IsExists)
            {
                HistoryLikePuzzle model = new HistoryLikePuzzle
                {
                    Active = "A",
                    Approved = "A",
                    AuthorID = UserId,
                    CreateDT = DateTime.Now,
                    IsDeleted = false,
                    IsMultiPuzzle = false,
                    PuzzleID = puzzle,
                    UpdateDT = DateTime.Now,
                };
                await _context.HistoryLikePuzzle.AddAsync(model);

                var singlePuzzle = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == puzzle);
                singlePuzzle.Like--;
                _context.SinglePuzzle.Update(singlePuzzle);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> VoteUpPuzzle(int puzzle, string UserId)
        {
            bool IsExists = await _context.HistoryLikePuzzle.AnyAsync(p => p.PuzzleID == puzzle && p.AuthorID == UserId);

            // neu chua vote lan nao thi tien hanh vote
            if (!IsExists)
            {
                HistoryLikePuzzle model = new HistoryLikePuzzle
                {
                    Active = "A",
                    Approved = "A",
                    AuthorID = UserId,
                    CreateDT = DateTime.Now,
                    IsDeleted = false,
                    IsMultiPuzzle = false,
                    PuzzleID = puzzle,
                    UpdateDT = DateTime.Now,
                };
                await _context.HistoryLikePuzzle.AddAsync(model);

                var singlePuzzle = await _context.SinglePuzzle.SingleOrDefaultAsync(p => p.ID == puzzle);
                singlePuzzle.Like++;
                _context.SinglePuzzle.Update(singlePuzzle);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ListSinglePuzzleViewModel> ListSinglePuzzle(int? page, int? pageSize)
        {
            var listSingle = _context.SinglePuzzle.Include(p =>p.Author)
                                                  .Include(p =>p.Image)
                                                  .Where(p => !p.IsMMultiPuzzle)
                                                  .Where(p => !p.IsDeleted)
                                                  .Where(p => p.CreateDT <= DateTime.Now)
                                                  .Where(p => p.Approved == Global.APPROVED);
            
           
            var pagelist = await PaginatedList<SinglePuzzle>.CreateAsync(listSingle.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10);

            
            List<SinglePuzzleViewModel> list = new List<SinglePuzzleViewModel>();
            foreach (var item in pagelist)
            {
                SinglePuzzleViewModel temp = new SinglePuzzleViewModel
                {
                    Image = item.Image,
                    Author = item.Author,
                    Slug = item.Slug,
                    Views = item.Views,
                    ImageID = item.ImageID,
                    Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                    DateTime = item.CreateDT,
                    ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                    Like = item.Like,
                    Title = item.Title
                };
                list.Add(temp);
            }
            ListSinglePuzzleViewModel model = new ListSinglePuzzleViewModel
            {
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                List = list,
                TotalPages = pagelist.TotalPages
            };
            return model;
        }

        public async Task<ListMultiPuzzleViewModel> ListMultiPuzzle(int? page, int? pageSize)
        {
            var listMulti = _context.MultiPuzzle.Include(p => p.Author)
                                                  .Include(p => p.Image)
                                                  .Where(p => !p.IsDeleted)
                                                  .Where(p => p.CreateDT <= DateTime.Now)
                                                  .Where(p => p.Approved == Global.APPROVED);


            var pagelist = await PaginatedList<MultiPuzzle>.CreateAsync(listMulti.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10);


            List<MultiPuzzleViewModel> list = new List<MultiPuzzleViewModel>();
            foreach (var item in pagelist)
            {
                MultiPuzzleViewModel temp = new MultiPuzzleViewModel
                {
                    Image = item.Image,
                    Author = item.Author,
                    Slug = item.Slug,
                    Views = item.Views,
                    ImageID = item.ImageID,
                    Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                    DateTime = item.CreateDT,
                    ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                    Like = item.Like,
                    Title = item.Title
                };
                list.Add(temp);
            }
            ListMultiPuzzleViewModel model = new ListMultiPuzzleViewModel
            {
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                List = list,
                TotalPages = pagelist.TotalPages
            };
            return model;
        }
    }
}
