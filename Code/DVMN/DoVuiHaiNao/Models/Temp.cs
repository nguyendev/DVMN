﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models
{
    public class Temp
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Image { get; set; }
        public bool IsYesNo { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }

        public string TempTag { get; set; }
        public int Correct { get; set; }
        public string Reason { get; set; }
    }
}
