using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.MemberViewModels
{
    public class UserProfileViewModel
    {
        public string Slug { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string CreateRegister { get; set; }
        public string Website { get; set; }
        public string InfoShort { get; set; }
        public string FaceboookURL { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string GooglePlus { get; set; }
        public string Email { get; set; }
        
        public int Points { get; set; }
        public int VisitorToday { get; set; }
        public int VisitorMonth { get; set; }
        public int VisitorTotal { get; set; }

        public int SinglePuzzleToday { get; set; }

        public int SinglePuzzleMonth { get; set; }

        public int SinglePuzzleTotal { get; set; }

        public int MultiPuzzleToday { get; set; }

        public int MultiPuzzleMonth { get; set; }

        public int MultiPuzzleTotal { get; set; }
    }
}
