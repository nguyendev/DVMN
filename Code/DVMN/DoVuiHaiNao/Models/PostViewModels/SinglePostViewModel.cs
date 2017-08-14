using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.PostViewModels
{
    public class SinglePostViewModel
    {
        public Member Author { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public int? ImageID { get; set; }
        public Images Image { get; set; }
        public IEnumerable<SinglePuzzleTag> Tags { get; set; }
        public int Views { get; set; }
        public int Like { get; set; }
        public string DateTime { get; set; }
        public IEnumerable<SimplePost> RelatedPuzzle { get; set; }
    }
    public class SimplePost
    {
        public string Title { get; set; }
        public string Slug { get; set; }

    }
}
