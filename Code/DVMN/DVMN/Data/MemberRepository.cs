using DVMN.Extension;
using DVMN.Models;
using DVMN.Models.MemberViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public class MemberRepository: IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ListMemberViewModel> GetAllMember(int? page, int? pageSize)
        {
            var member = await _context.Users.ToListAsync();
            var pagelist = PaginatedList<Member>.Create(member, page ?? 1, pageSize != null ? pageSize.Value : 10);


            List<MemberViewModel> list = new List<MemberViewModel>();
            foreach (var item in pagelist)
            {
                MemberViewModel temp = new MemberViewModel
                {
                    FullName = item.FullName,
                    Slug = item.Slug,
                    Image = item.Picture65x65,
                    Score = item.Score,
                    Facebook = item.Facebook,
                    
                };
                list.Add(temp);
            }
            ListMemberViewModel model = new ListMemberViewModel
            {
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                List = list,
                TotalPages = pagelist.TotalPages
            };
            return model;
        }


        public async Task<ListTopMemberViewModel> GetTopMember(int? page, int? pageSize)
        {
            var member = await _context.Users.ToListAsync();
            var pagelist = PaginatedList<Member>.Create(member, page ?? 1, pageSize != null ? pageSize.Value : 10);

            List<TopMemberViewModel> list = new List<TopMemberViewModel>();
            foreach (var item in pagelist)
            {
                TopMemberViewModel temp = new TopMemberViewModel
                {
                    FullName = item.FullName,
                    Slug = item.Slug,
                    Image = item.Picture65x65,
                    Score = item.Score,
                    Facebook = item.Facebook,

                };
                list.Add(temp);
            }
            ListTopMemberViewModel model = new ListTopMemberViewModel
            {
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                List = list.OrderByDescending(p => p.Score),
                TotalPages = pagelist.TotalPages
            };
            return model;
        }
    }
}
