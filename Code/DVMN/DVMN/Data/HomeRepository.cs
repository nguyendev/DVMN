using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVMN.Models.HomeViewModels;
using Microsoft.EntityFrameworkCore;
using DVMN.Services;

namespace DVMN.Data
{
    public class HomeRepository: IHomeRepository
    {
        private readonly ApplicationDbContext _context;
        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MainHomeViewModel>> GetIndex()
        {

            var SinglePuzzleDbContext = await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where (p => !p.IsMMultiPuzzle)
                .OrderByDescending(p => p.CreateDT)
                .ToListAsync();
            var MultiPuzzleDbContext = await _context.MultiPuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreateDT)
                .ToListAsync();
            try
            {
                List<MainHomeViewModel> model = new List<MainHomeViewModel>(SinglePuzzleDbContext.Capacity - 1);
                foreach (var item in SinglePuzzleDbContext)
                {
                    model.Add(new MainHomeViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = item.Description,
                        IsMultiPuzzle = false,
                        DateTime = item.CreateDT,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        Like = item.Like,
                        Title = item.Title
                    });
                }
                foreach (var item in MultiPuzzleDbContext)
                {
                    model.Add(new MainHomeViewModel
                    {
                        Image = item.Image,
                        Author = item.Author,
                        Slug = item.Slug,
                        Views = item.Views,
                        ImageID = item.ImageID,
                        Description = item.Description,
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
                return new List<MainHomeViewModel>();
            }
        }
    }
}
