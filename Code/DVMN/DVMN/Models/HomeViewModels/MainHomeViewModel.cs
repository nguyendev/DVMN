using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models.HomeViewModels
{
    public class MainHomeViewModel
    {
        public MainHomeViewModel(Image image, Member actor)
        {
            Image = image;
            Actor = actor;
        }
        public string Slug { get; set; }

        public string Description { get; set; }

        public Image Image { get; set; }

        public Member Actor { get; set; }
    }
}
