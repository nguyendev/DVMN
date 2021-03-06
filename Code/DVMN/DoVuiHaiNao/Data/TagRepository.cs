﻿using DoVuiHaiNao.Extension;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Models.HomeViewModels;
using DoVuiHaiNao.Models.TagViewModels;
using DoVuiHaiNao.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public class TagRepository : ITagRepository
    {
        public readonly ApplicationDbContext _context;
        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TagSingleViewModel> GetSingle(string slug, int? page, int? pageSize)
        {
            var tag = await _context.Tag.SingleOrDefaultAsync(p => p.Slug == slug);
            var tagSingle = await _context.SingPuzzleTag.Where(p => p.TagID == tag.ID).ToListAsync();
            try
            {
                List<SinglePuzzle> SinglePuzzleDbContext = new List<SinglePuzzle>();
                foreach (var item in tagSingle)
                {
                    SinglePuzzle singlePuzzle = await _context.SinglePuzzle
                    .Include(p => p.Image)
                    .Include(p => p.Author)
                    .Include(p => p.MultiPuzzle)
                    .Where( p => p.CreateDT < DateTime.Now)
                    .SingleOrDefaultAsync(p => p.ID == item.SinglePuzzleID);
                    if(singlePuzzle != null)
                        SinglePuzzleDbContext.Add(singlePuzzle);
                }
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
                        MultiPuzzle = item.MultiPuzzle,
                        MultiPuzzleID = item.MultiPuzzleID,
                        ImageID = item.ImageID,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                        IsMultiPuzzle = item.IsMMultiPuzzle,
                        DateTime = item.CreateDT,
                        ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                        Like = item.Like,
                        Title = item.Title,
                    });
                }
                TagSingleViewModel searchModel = new TagSingleViewModel
                {
                    Count = pagelist.Count,
                    PageIndex = pagelist.PageIndex,
                    PageSize = pagelist.PageSize,
                    List = list.OrderByDescending(p => p.DateTime),
                    TotalPages = pagelist.TotalPages,
                    Slug = slug,
                    Title = tag.Title
                };
                return searchModel;
            }
            catch
            {
                return new TagSingleViewModel();
            }
        }

        public async Task<IEnumerable<AllTagViewModel>> GetAllTag()
        {
            var result = _context.SingPuzzleTag.Select(m => new { m.TagID }).Distinct().ToList();
            List<AllTagViewModel> model = new List<AllTagViewModel>();
            foreach (var item in result)
            {
                var number = await _context.SingPuzzleTag.Include(p => p.Tag).Where(dt => dt.TagID == item.TagID).CountAsync();
                
                var tagDetail = await _context.SingPuzzleTag.Include(p => p.Tag).Where(dt => dt.TagID == item.TagID).FirstOrDefaultAsync();
                if (number > 20)
                {
                    model.Add(new AllTagViewModel { FontSize = Fonsize.BIGGEST, Slug = tagDetail.Tag.Slug, Title = tagDetail.Tag.Title });
                }
                else if (number > 10)
                {
                    model.Add(new AllTagViewModel { FontSize = Fonsize.BIG, Slug = tagDetail.Tag.Slug, Title = tagDetail.Tag.Title });
                }
                else if (number > 5)
                {
                    model.Add(new AllTagViewModel { FontSize = Fonsize.NORMAL, Slug = tagDetail.Tag.Slug, Title = tagDetail.Tag.Title });
                }
                else
                {
                    model.Add(new AllTagViewModel { FontSize = Fonsize.SMALL, Slug = tagDetail.Tag.Slug, Title = tagDetail.Tag.Title });
                }

            }
            return model;
        }
    }
}
