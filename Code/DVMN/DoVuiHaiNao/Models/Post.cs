using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        [MaxLength(50)]
        public string Slug { get; set; }
        public int? ImageID { get; set; }
        public Images Image { get; set; }
        public int Views { get; set; }
        public List<Comment> Comment { get; set; }
        public int Like { get; set; }
    }
}
