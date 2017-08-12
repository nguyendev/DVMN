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
    }
}
