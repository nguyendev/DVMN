using DoVuiHaiNao.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models
{
    public class MultiPuzzle : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string Slug { get; set; }
        [Required]
        public int NumberQuestion { get; set; }

        public int? ImageID { get; set; }
        public Images Image { get; set; }
        public int Like { get; set; }
        public float Level { get; set; }
        public int Views { get; set; }
        public List<Comment> Comment { get; set; }
        public List<SinglePuzzle> SinglePuzzleDetails { get; set; }
    }
}
