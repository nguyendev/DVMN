﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.SidebarViewModels
{
    public class TopPuzzleViewModel
    {
        public string Title { get; set; }
        public int Like { get; set; }
        public string Slug { get; set; }
        public bool IsMultiPuzzle { get; set; }
        public string Description { get; set; }
    }
}
