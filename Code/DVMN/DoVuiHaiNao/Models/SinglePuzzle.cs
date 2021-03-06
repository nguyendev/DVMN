﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models
{
    public class SinglePuzzle : Base
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        [MaxLength(50)]
        public string Slug { get; set; }
        public int? ImageID { get; set; }
        public Images Image { get; set; }
        public int Type { get; set; }
        public bool IsYesNo { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public int Correct { get; set; }
        public string Reason { get; set; }
        public int Views { get; set; }
        public List<Comment> Comment { get; set; }
        public int Like { get; set; }
        public float Level { get; set; }
        public bool IsMMultiPuzzle { get; set; }
        public int? MultiPuzzleID { get; set; }
        public MultiPuzzle MultiPuzzle { get; set; }
    }
}
