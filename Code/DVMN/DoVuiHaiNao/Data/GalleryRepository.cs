using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoVuiHaiNao.Models.GalleryViewModels;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Extension;
using DoVuiHaiNao.Models;

namespace DoVuiHaiNao.Data
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly ApplicationDbContext _context;
        public GalleryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<GalleryViewModel> GetAll(int? page, int? pageSize)
        {
            var applicationDbContext = _context.Images;

            var pagelist = await PaginatedList<Images>.CreateAsync(applicationDbContext.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10);


            
            List <SingleGalleryViewModel> listGallery = new List<SingleGalleryViewModel>();
            foreach (var item in pagelist)
            {
                bool isExits = _context.SinglePuzzle.Any(p => p.ImageID == item.ID);
                if (isExits)
                {
                    try
                    {
                        var link = await _context.SinglePuzzle
                            .Where( p => p.CreateDT < DateTime.Now)
                            .Where( p => p.Approved == "A")
                            .Where( p => !p.IsDeleted)
                            .SingleOrDefaultAsync(p => p.ImageID == item.ID);
                        if(link!= null)
                        { 
                            SingleGalleryViewModel single = new SingleGalleryViewModel
                            {
                                Title = link.Title,
                                Source = item.Pic640x480,
                                ALT = item.ALT,
                                Slug = link.Slug,
                                CreateTime = item.CreateDT,
                                IsMul = link.IsMMultiPuzzle
                            };
                            listGallery.Add(single);
                        }

                    }
                    catch { }
                }
                
            }
            GalleryViewModel model = new GalleryViewModel
            {
                List = listGallery.OrderBy(p => p.CreateTime),
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                TotalPages = pagelist.TotalPages
            };
            return model;
        }
    }
}
