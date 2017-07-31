using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Models
{
    public class Images : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string ALT { get; set; }
        public string Title { get; set; }
        public string Pic250x188 { get; set; }
        public string PicFull { get; set; }
    }
}
