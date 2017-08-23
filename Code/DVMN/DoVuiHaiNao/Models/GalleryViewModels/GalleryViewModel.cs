using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.GalleryViewModels
{
    public class GalleryViewModel
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        public int Count { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        public IEnumerable<SingleGalleryViewModel> List { get;set;}
    }
    public class SingleGalleryViewModel
    {
        public string Title { get; set; }
        public string ALT { get; set; }
        public string Slug { get; set; }
        public string Source { get; set; }
        public bool IsMul { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
