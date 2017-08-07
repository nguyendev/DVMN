using DVMN.Extension;
using DVMN.Models;
using DVMN.Models.HomeViewModels;
using DVMN.Models.SearchViewModel;
using DVMN.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ApplicationDbContext _context;
        public SearchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SearchViewModel> GetSearch(string search, int? page, int? pageSize)
        {

            var SinglePuzzleDbContext = await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => p.Title.Contains(search))
                .OrderByDescending(p => p.CreateDT)
                .ToListAsync();
            var pagelist = PaginatedList<SinglePuzzle>.Create(SinglePuzzleDbContext, page ?? 1, pageSize != null ? pageSize.Value : 10);
            List<SingleViewModel> list = new List<SingleViewModel>();
            foreach (var item in pagelist)
            {
                list.Add(new SingleViewModel
                {
                    Image = item.Image,
                    Author = item.Author,
                    Slug = item.Slug,
                    Views = item.Views,
                    ImageID = item.ImageID,
                    Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                    IsMultiPuzzle = item.IsMMultiPuzzle,
                    DateTime = item.CreateDT,
                    ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                    Like = item.Like,
                    Title = item.Title
                });
            }
            SearchViewModel searchModel = new SearchViewModel
            {
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                List = list,
                TotalPages = pagelist.TotalPages
            };
            return searchModel;

        }

    }
}
