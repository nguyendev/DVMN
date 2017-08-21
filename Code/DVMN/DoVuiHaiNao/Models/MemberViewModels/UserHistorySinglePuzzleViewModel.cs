using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Models.MemberViewModels
{
    public class UserHistorySinglePuzzleViewModel
    {
        public string Birthday { get; set; }
        public string Slug { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public DateTime? CreateRegister { get; set; }
        public string Website { get; set; }
        public string InfoShort { get; set; }
        public string FaceboookURL { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string GooglePlus { get; set; }
        public string Email { get; set; }
        public int Points { get; set; }
        public int SinglePuzzleTotal { get; set; }
        public int MultiPuzzleTotal { get; set; }

        //Page List
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
        public IEnumerable<UserHistorySinglePuzzle> ListSinglePuzzle { get; set; }
    }
    public class UserHistorySinglePuzzle
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Views { get ; set; }
        public string ShowTime { get; set; } 
        public DateTime? CreateDate { get; set;}
    }
}
