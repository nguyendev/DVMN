using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoVuiHaiNao.Models.HomeViewModels;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Services;

namespace DoVuiHaiNao.Data
{
    public class HomeRepository: IHomeRepository
    {
        private readonly ApplicationDbContext _context;
        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<HomeViewModel> GetIndex()
        {
            HomeViewModel model = new HomeViewModel
            {
                MostFavorite = await GetMostFavoritePuzzle(),
                Mostpopular = await GetMostPopularPuzzle(),
                RecentPuzzle = await GetRecentPuzzle()
            };
            return model;
        }

        private async Task<IEnumerable<SingleViewModel>> GetRecentPuzzle()
        {
            var SinglePuzzleDbContext = await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where (p => !p.IsMMultiPuzzle)
                .Where(p => p.Approved == Global.APPROVED)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => !p.IsDeleted)
                .Take(5)
                .OrderByDescending(p => p.CreateDT)
                .ToListAsync();
            var MultiPuzzleDbContext = await _context.MultiPuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => p.Approved == Global.APPROVED)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => !p.IsDeleted)
                .Take(5)
                .OrderByDescending(p => p.CreateDT)
                .ToListAsync();
            try
            {
                List<SingleViewModel> model = new List<SingleViewModel>(SinglePuzzleDbContext.Capacity - 1);
                foreach (var item in SinglePuzzleDbContext)
                {
                    model.Add(new SingleViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                        IsMultiPuzzle = false,
                        DateTime = item.CreateDT,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        Like = item.Like,
                        Title = item.Title
                    });
                }
                foreach (var item in MultiPuzzleDbContext)
                {
                    model.Add(new SingleViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                        Title = item.Title,
                        DateTime = item.CreateDT,
                        Like = item.Like,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        IsMultiPuzzle = true,
                    });
                }
                return model.OrderByDescending(p => p.DateTime);
                
            }
            catch
            {
                return new List<SingleViewModel>();
            }
        }

        private async Task<IEnumerable<SingleViewModel>> GetMostFavoritePuzzle()
        {

            var SinglePuzzleDbContext = await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => !p.IsMMultiPuzzle)
                .Where(p => p.Approved == Global.APPROVED)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => !p.IsDeleted)
                .Take(5)
                .OrderByDescending(p => p.Like)
                .ToListAsync();
            var MultiPuzzleDbContext = await _context.MultiPuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => p.Approved == Global.APPROVED)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => !p.IsDeleted)
                .Take(5)
                .OrderByDescending(p => p.Like)
                .ToListAsync();
            try
            {
                List<SingleViewModel> model = new List<SingleViewModel>(SinglePuzzleDbContext.Capacity - 1);
                foreach (var item in SinglePuzzleDbContext)
                {
                    model.Add(new SingleViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                        IsMultiPuzzle = false,
                        DateTime = item.CreateDT,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        Like = item.Like,
                        Title = item.Title
                    });
                }
                foreach (var item in MultiPuzzleDbContext)
                {
                    model.Add(new SingleViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                        Title = item.Title,
                        DateTime = item.CreateDT,
                        Like = item.Like,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        IsMultiPuzzle = true,
                    });
                }
                return model.OrderByDescending(p => p.Like);

            }
            catch
            {
                return new List<SingleViewModel>();
            }
        }



        private async Task<IEnumerable<SingleViewModel>> GetMostPopularPuzzle()
        {

            var SinglePuzzleDbContext = await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => !p.IsMMultiPuzzle)
                .Where(p => p.Approved == Global.APPROVED)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => !p.IsDeleted)
                .OrderByDescending(p => p.Views)
                .Take(5)
                .ToListAsync();
            var MultiPuzzleDbContext = await _context.MultiPuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => p.Approved == Global.APPROVED)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => !p.IsDeleted)
                .Take(5)
                .OrderByDescending(p => p.Views)
                .ToListAsync();
            try
            {
                List<SingleViewModel> model = new List<SingleViewModel>(SinglePuzzleDbContext.Capacity - 1);
                foreach (var item in SinglePuzzleDbContext)
                {
                    model.Add(new SingleViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                        IsMultiPuzzle = false,
                        DateTime = item.CreateDT,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        Like = item.Like,
                        Title = item.Title
                    });
                }
                foreach (var item in MultiPuzzleDbContext)
                {
                    model.Add(new SingleViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                        Title = item.Title,
                        DateTime = item.CreateDT,
                        Like = item.Like,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        IsMultiPuzzle = true,
                    });
                }
                return model.OrderByDescending(p => p.Views);

            }
            catch
            {
                return new List<SingleViewModel>();
            }
        }


    }
}
