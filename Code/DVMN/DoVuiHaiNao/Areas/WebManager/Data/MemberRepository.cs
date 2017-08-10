using DoVuiHaiNao.Data;
using DoVuiHaiNao.Extension;
using DoVuiHaiNao.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Areas.WebManager.Data
{
    public class MemberRepository : IMemberRepository
    {
        protected readonly ApplicationDbContext _context;

        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Member> Get(string id)
        {
            return await _context.Member
                .SingleOrDefaultAsync(m => m.Id == id);
        }
        public bool Exists(string id)
        {
            return _context.Member.Any(c => c.Id == id);
        }

        public async Task<PaginatedList<Member>> GetAll(string sortOrder, string searchString,
    int? page, int? pageSize)
        {
            var members = from s in _context.Member
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                members = members.Where(s => s.FullName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "fullName":
                    members = members.OrderByDescending(s => s.FullName);
                    break;
                default:
                    members = members.OrderBy(s => s.CreateDT);
                    break;
            }
            return await PaginatedList<Member>.CreateAsync(members, page ?? 1, pageSize != null ? pageSize.Value : 10);
        }

        public async Task Update(Member model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var member = await _context.Member.SingleOrDefaultAsync(m => m.Id == id);
            _context.Member.Remove(member);
            if (member.IsDeleted)
                _context.Member.Remove(member);
            else
            {
                member.IsDeleted = true;
                _context.Member.Update(member);
            }
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Member> GetEdit(string ID)
        {
            var singlePuzzleDbContext = await _context.Member.SingleOrDefaultAsync(p => p.Id == ID);
            return singlePuzzleDbContext;
            //if (singlePuzzleDbContext != null)
            //{
            //    var tags = await _context.SingPuzzleTag.
            //        Include(p => p.Tag).
            //        Where(p => p.SinglePuzzleID == singlePuzzleDbContext.ID).
            //        ToListAsync();
            //    string tempTag = "";
            //    foreach (var item in tags)
            //    {
            //        tempTag = tempTag + item.Tag.Title + ", ";
            //    }
            //    EditSinglePuzzleViewModels editModel = new EditSinglePuzzleViewModels
            //    {
            //        ID = singlePuzzleDbContext.ID,
            //        Title = singlePuzzleDbContext.Title,
            //        Description = singlePuzzleDbContext.Description,
            //        AnswerA = singlePuzzleDbContext.AnswerA,
            //        AnswerB = singlePuzzleDbContext.AnswerB,
            //        AnswerC = singlePuzzleDbContext.AnswerC,
            //        AnswerD = singlePuzzleDbContext.AnswerD,
            //        Correct = singlePuzzleDbContext.Correct,
            //        Image = singlePuzzleDbContext.Image,
            //        ImageID = singlePuzzleDbContext.ImageID,
            //        IsYesNo = singlePuzzleDbContext.IsYesNo,
            //        Note = singlePuzzleDbContext.Note,
            //        Reason = singlePuzzleDbContext.Reason,
            //        Slug = singlePuzzleDbContext.Slug,
            //        Active = singlePuzzleDbContext.Active,
            //        Approved = singlePuzzleDbContext.Approved,
            //        IsDelete = singlePuzzleDbContext.IsDeleted,
            //        IsMMultiPuzzle = singlePuzzleDbContext.IsMMultiPuzzle,
            //        MultiPuzzleID = singlePuzzleDbContext.MMultiPuzzleID,
            //        TempTag = tempTag
            //    };
            //    return editModel;
            //}
            //return null;
        }
    }
}
