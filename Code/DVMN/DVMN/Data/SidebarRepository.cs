using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DVMN.Models.SidebarViewModels;
using Microsoft.EntityFrameworkCore;

namespace DVMN.Data
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
            AllSidebarViewModel getAllSibar = new AllSidebarViewModel
            {
                TopTag = topTag,
                TopUser = topUser
            };
            return getAllSibar;

        }

        public async Task<IEnumerable<TopUserViewModel>> GetTopMemberPuzzle()
        {
            var topUser = await _context.Users.OrderByDescending(p => p.Score).Take(5).ToListAsync();
            List<TopUserViewModel> model = new List<TopUserViewModel>(topUser.Capacity - 1);
            foreach (var item in topUser)
            {
                model.Add(new TopUserViewModel
                {
                    FullName = item.FullName,
                    Image = item.PictureSmall,
                    Point = item.Score,
                    Slug = item.Slug
                });
            }
            return model;
        }

        public async Task<IEnumerable<TopTagViewModel>> GetTopTagPuzzle()
        {
            var topTags = await _context.SingPuzzleTag
                .Include( p => p.Tag)
                .OrderByDescending(p => p.Tag).Take(5).ToListAsync();
            List<TopTagViewModel> model = new List<TopTagViewModel>(topTags.Capacity - 1);
            foreach (var item in topTags)
            {
                model.Add(new TopTagViewModel
                {
                   Name = item.Tag.Title,
                   Slug = item.Tag.Slug
                });
            }
            return model;
        }
    }
}
