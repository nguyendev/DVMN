﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoVuiHaiNao.Models.SidebarViewModels;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Services;

namespace DoVuiHaiNao.Data
{
    public class SidebarRepository : ISidebarRepository
    {
        private readonly ApplicationDbContext _context;

        public SidebarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AllSidebarViewModel> GetAllSibar()
        {
            IEnumerable<TopUserViewModel> topUser = await GetTopMemberPuzzle();
            IEnumerable<TopTagViewModel> topTag = await GetTopTagPuzzle();
            IEnumerable<TopPuzzleViewModel> recentPuzzle = await GetTopRecentPuzzle();
            AllSidebarViewModel getAllSibar = new AllSidebarViewModel
            {
                TopTag = topTag,
                TopUser = topUser,
                RecentPuzzle = recentPuzzle
            };
            getAllSibar.CountSinglePuzzle = _context.SinglePuzzle.Where(p => !p.IsMMultiPuzzle).Count();
            getAllSibar.CountMultiPuzzle = _context.MultiPuzzle.Count();
            getAllSibar.CountMemberPuzzle = _context.Users.Count();
            return getAllSibar;

        }

        public async Task<IEnumerable<TopUserViewModel>> GetTopMemberPuzzle()
        {
            try
            {
                var topUser = await _context.Users.OrderByDescending(p => p.Score).Take(5).ToListAsync();
                List<TopUserViewModel> model = new List<TopUserViewModel>(topUser.Capacity - 1);
                foreach (var item in topUser)
                {
                    model.Add(new TopUserViewModel
                    {
                        FullName = item.FullName,
                        Image = item.Picture65x65,
                        Point = item.Score,
                        Slug = item.Slug
                    });
                }
                return model;
            }
            catch 
            {
                return new List<TopUserViewModel>();
            }
            
        }

        public async Task<IEnumerable<TopTagViewModel>> GetTopTagPuzzle()
        {
            try
            {
                var result = _context.SingPuzzleTag.Select(m => new {m.TagID }).Distinct().Take(10);
                List<TopTagViewModel> model = new List<TopTagViewModel>();
                foreach (var item in result)
                {
                    var temp = await _context.SingPuzzleTag.Include(p => p.Tag).Where(dt => dt.TagID == item.TagID).Select(dt => new { dt.Tag }).Distinct().FirstOrDefaultAsync();
                    model.Add(new TopTagViewModel
                    {
                        Name = temp.Tag.Title,
                        Slug = temp.Tag.Slug
                    });
                }
                return model;
            }
            catch
            {
                return new List<TopTagViewModel>();
            }
        }

        public async Task<IEnumerable<TopPuzzleViewModel>> GetTopRecentPuzzle()
        {
            var SinglePuzzleDbContext = await _context.SinglePuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => !p.IsMMultiPuzzle)
                .Where(p => p.CreateDT < DateTime.Now)
                .OrderByDescending(p => p.Like)
                .Take(5)
                .ToListAsync();
            var MultiPuzzleDbContext = await _context.MultiPuzzle
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => p.CreateDT < DateTime.Now)
                .OrderByDescending(p => p.Like)
                .Take(3)
                .ToListAsync();
            try
            {
                List<TopPuzzleViewModel> model = new List<TopPuzzleViewModel>(SinglePuzzleDbContext.Capacity + MultiPuzzleDbContext.Capacity - 2);
                foreach (var item in SinglePuzzleDbContext)
                {
                    model.Add(new TopPuzzleViewModel
                    {
                        Slug = item.Slug,
                        Like = item.Like,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionSEO),
                        IsMultiPuzzle = false,
                        Title = item.Title
                    });
                }
                foreach (var item in MultiPuzzleDbContext)
                {
                    model.Add(new TopPuzzleViewModel
                    {
                        Slug = item.Slug,
                        Like = item.Like,
                        Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionSEO),
                        Title = item.Title,
                        IsMultiPuzzle = true
                    });
                }
                return model.OrderByDescending(p => p.Like);

            }
            catch
            {
                return new List<TopPuzzleViewModel>();
            }
        }
    }
}

