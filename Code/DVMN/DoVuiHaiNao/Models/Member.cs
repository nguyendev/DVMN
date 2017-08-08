using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DoVuiHaiNao.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class Member : IdentityUser
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public string PictureSmall { get; set; }
        public string Picture65x65 { get; set; }
        public string Slug { get; set; }
        public string PictureBig { get; set; }
        public string DateofBirth { get; set; }
        public string Facebook { get; set; }

        public string GooglePlus { get; set; }
        public string Linkedin { get; set; }

        public string Twitter { get; set; }
        public string IdentityFacebook { get; set; }
        public int Score { get; set; }
        public string Website { get; set; }
    }
}
