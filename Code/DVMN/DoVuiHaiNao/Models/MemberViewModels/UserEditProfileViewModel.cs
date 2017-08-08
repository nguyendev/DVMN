using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.MemberViewModels
{
    public class UserEditProfileViewModel
    {
        public string Slug { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string About { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set;}
        public string Linkedin { get; set; }
        public string GooglePlus { get; set; }
    }
}
