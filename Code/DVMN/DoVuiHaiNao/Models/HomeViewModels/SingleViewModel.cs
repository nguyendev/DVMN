using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.HomeViewModels
{
    public class SingleViewModel
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

        public bool IsMultiPuzzle { get; set; }
    }
}
