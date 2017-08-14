using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoVuiHaiNao.Models.PostViewModels;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Services;
using DoVuiHaiNao.Extension;
using DoVuiHaiNao.Models;

namespace DoVuiHaiNao.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ListPostViewModel> GetListPost(int? page, int? pageSize)
        {
            var listPost = _context.Post.Include(p => p.Author)
                                                 .Include(p => p.Image)
                                                 .Where(p => !p.IsDeleted)
                                                 .Where(p => p.CreateDT <= DateTime.Now)
                                                 .Where(p => p.Approved == Global.APPROVED);


            var pagelist = await PaginatedList<Post>.CreateAsync(listPost.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10);


            List<SimpleSinglePostViewModel> list = new List<SimpleSinglePostViewModel>();
            foreach (var item in pagelist)
            {
                SimpleSinglePostViewModel temp = new SimpleSinglePostViewModel
                {
                    Image = item.Image,
                    Author = item.Author,
                    Slug = item.Slug,
                    Views = item.Views,
                    Content = item.Content,
                    ImageID = item.ImageID,
                    Description = SEOExtension.GetStringToLength(item.Description, SEOExtension.MaxDescriptionNormal),
                    DateTime = item.CreateDT,
                    ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                    Like = item.Like,
                    Title = item.Title
                };
                list.Add(temp);
            }
            ListPostViewModel model = new ListPostViewModel
            {
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                List = list,
                TotalPages = pagelist.TotalPages
            };
            return model;
        }

        public async Task IncreaseView(string slug)
        {
            var item = await _context.Post.SingleOrDefaultAsync(p => p.Slug == slug);
            if (item != null)
            {
                item.Views++;
                _context.Post.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SinglePostViewModel> GetSinglePost(string slug)
        {
            var single = await _context.Post
                .Include(p => p.Image)
                .Include(p => p.Author)
                .Where(p => !p.IsDeleted)
                .Where(p => p.CreateDT <= DateTime.Now)
                .Where(p => p.Approved == Global.APPROVED)
                .SingleOrDefaultAsync(p => p.Slug == slug);
            var bestSingle = await _context.Post
                .Take(4)
                .OrderByDescending(p => p.Like)
                .ToListAsync();
            List<SimplePost> listbestPuzzle = new List<SimplePost>(3);
            foreach (var item in bestSingle)
            {
                listbestPuzzle.Add(new SimplePost { Slug = item.Slug, Title = item.Title });
            }
            var tags = await _context.SingPuzzleTag
               .Include(p => p.SinglePuzzle)
               .Include(p => p.Tag)
               .Where(p => p.SinglePuzzleID == single.ID)
               .ToListAsync();


            SinglePostViewModel model = new SinglePostViewModel
            {
                ID = single.ID,
                ImageID = single.ImageID,
                Like = single.Like,
                Slug = single.Slug,
                Views = single.Views,
                Title = single.Title,
                Tags = tags,
                Author = single.Author,
                Description = single.Description,
                Image = single.Image,
                DateTime = DateTimeExtension.CurrentDay(single.CreateDT.Value),
                RelatedPuzzle = listbestPuzzle,
                Content = single.Content
            };
            return model;
        }


    }
}
