using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.PuzzleViewModels
{
    public class ListMultiPuzzleViewModel
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

        public IEnumerable<MultiPuzzleViewModel> List { get; set; }
    }
    public class MultiPuzzleViewModel
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int? ImageID { get; set; }
        public string Description { get; set; }

        public Images Image { get; set; }
        public DateTime? DateTime { get; set; }

        public string ShowTime { get; set; }
        public Member Author { get; set; }
        public int Like { get; set; }
        public int Views { get; set; }
    }
}
