using DoVuiHaiNao.Extension;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Models.MemberViewModels;
using DoVuiHaiNao.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
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

        public async Task<UserHistorySinglePuzzleViewModel> GetHistoryListSinglePuzzle(string slug, int? page, int? pageSize)
        {
            var member = await _context.Users.SingleOrDefaultAsync(p => p.Slug == slug);
            int SinglePuzzleTotal = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && !p.IsMultiPuzzle).CountAsync();
            int MultiPuzzleTotal = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && p.IsMultiPuzzle).CountAsync();

            // lay danh sach cac cot cau do trong lich su cua nguoi dung
            var historySinglePuzzle = await _context.HistoryAnswerPuzzle
                .Where(p => p.AuthorID == member.Id && !p.IsMultiPuzzle)
                .ToListAsync();

            // lay danh sach nay tuong ung voi bang cau do tuong ung
            List<SinglePuzzle> singlePuzzleDbContext = new List<SinglePuzzle>(); 
            foreach (var item in historySinglePuzzle)
            {
                var singlePuzzle = _context.SinglePuzzle
                    .Include(p => p.Image)
                    .SingleOrDefault(p => p.ID == item.PuzzleID);
                singlePuzzleDbContext.Add(singlePuzzle);
                
            }

            // tien hanh phan trang
            var pagelist = PaginatedList<SinglePuzzle>.Create(singlePuzzleDbContext, page ?? 1, pageSize != null ? pageSize.Value : 10);


            //tien hanh du tat ca vao bang danh sach cau do
            List<UserHistorySinglePuzzle> listSinglePuzzle = new List<UserHistorySinglePuzzle>();
            foreach (var item in pagelist)
            {
                UserHistorySinglePuzzle temp = new UserHistorySinglePuzzle
                {
                    Title = item.Title,
                    Views = item.Views,
                    Slug = item.Slug,
                    ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                    CreateDate = item.CreateDT
                };
                listSinglePuzzle.Add(temp);
            }
            UserHistorySinglePuzzleViewModel model = new UserHistorySinglePuzzleViewModel
            {
                FaceboookURL = member.Facebook,
                FullName = member.FullName,
                SinglePuzzleTotal = SinglePuzzleTotal,
                MultiPuzzleTotal = MultiPuzzleTotal,
                Points = member.Score,
                Image = member.Picture65x65,
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                ListSinglePuzzle = listSinglePuzzle.OrderByDescending(p => p.CreateDate),
                TotalPages = pagelist.TotalPages,
            };
            return model;
        }
        public async Task<UserHistoryMultiPuzzleViewModel> GetHistoryListMultiPuzzle(string slug, int? page, int? pageSize)
        {
            var member = await _context.Users.SingleOrDefaultAsync(p => p.Slug == slug);
            int SinglePuzzleTotal = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && !p.IsMultiPuzzle).CountAsync();
            int MultiPuzzleTotal = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && p.IsMultiPuzzle).CountAsync();

            // lay danh sach cac cot cau do trong lich su cua nguoi dung
            var historySinglePuzzle = await _context.HistoryAnswerPuzzle
                .Where(p => p.AuthorID == member.Id && p.IsMultiPuzzle)
                .ToListAsync();

            // lay danh sach nay tuong ung voi bang cau do tuong ung
            List<MultiPuzzle> multiPuzzleDbContext = new List<MultiPuzzle>();
            foreach (var item in historySinglePuzzle)
            {
                var multiPuzzle = _context.MultiPuzzle
                    .Include(p => p.Image)
                    .SingleOrDefault(p => p.ID == item.PuzzleID);
                multiPuzzleDbContext.Add(multiPuzzle);

            }

            // tien hanh phan trang
            var pagelist = PaginatedList<MultiPuzzle>.Create(multiPuzzleDbContext, page ?? 1, pageSize != null ? pageSize.Value : 10);


            //tien hanh du tat ca vao bang danh sach cau do
            List<UserHistoryMultiPuzzle> listMultiPuzzle = new List<UserHistoryMultiPuzzle>();
            foreach (var item in pagelist)
            {
                UserHistoryMultiPuzzle temp = new UserHistoryMultiPuzzle
                {
                    Title = item.Title,
                    Views = item.Views,
                    Slug = item.Slug,
                    ShowTime = DateTimeExtension.CurrentDay(item.CreateDT.Value),
                    CreateDate = item.CreateDT
                };
                listMultiPuzzle.Add(temp);
            }
            UserHistoryMultiPuzzleViewModel model = new UserHistoryMultiPuzzleViewModel
            {
                FaceboookURL = member.Facebook,
                FullName = member.FullName,
                SinglePuzzleTotal = SinglePuzzleTotal,
                MultiPuzzleTotal = MultiPuzzleTotal,
                Points = member.Score,
                Image = member.Picture65x65,
                Count = pagelist.Count,
                PageIndex = pagelist.PageIndex,
                PageSize = pagelist.PageSize,
                ListMultiPuzzle = listMultiPuzzle.OrderByDescending(p => p.CreateDate),
                TotalPages = pagelist.TotalPages,
            };
            return model;
        }

        public async Task<UserProfileViewModel> GetProfile(string slug)
        {
            var member = await _context.Users.SingleOrDefaultAsync(p => p.Slug == slug);
            int SinglePuzzleTotal = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && !p.IsMultiPuzzle).CountAsync();
            int SinglePuzzleToday = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && !p.IsMultiPuzzle && p.CreateDT.Value.Date == DateTime.Now.Date).CountAsync();
            int SinglePuzzleMonth = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && !p.IsMultiPuzzle && p.CreateDT.Value.Month == DateTime.Now.Month && p.CreateDT.Value.Year == DateTime.Now.Year).CountAsync();

            int MultiPuzzleTotal = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && p.IsMultiPuzzle).CountAsync();
            int MultiPuzzleToday = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && p.IsMultiPuzzle && p.CreateDT.Value.Date == DateTime.Now.Date).CountAsync();
            int MultiPuzzleMonth = await _context.HistoryAnswerPuzzle.Where(p => p.AuthorID == member.Id && p.IsMultiPuzzle && p.CreateDT.Value.Month == DateTime.Now.Month && p.CreateDT.Value.Year == DateTime.Now.Year).CountAsync();


            UserProfileViewModel profile = new UserProfileViewModel
            {
                FaceboookURL = member.Facebook,
                FullName = member.FullName,
                SinglePuzzleTotal = SinglePuzzleTotal,
                SinglePuzzleToday = SinglePuzzleToday,
                SinglePuzzleMonth = SinglePuzzleMonth,
                MultiPuzzleToday = MultiPuzzleToday,
                MultiPuzzleMonth = MultiPuzzleMonth,
                MultiPuzzleTotal = MultiPuzzleTotal,
                Points = member.Score,
                Image = member.Picture65x65,
                Slug = slug
            };
            return profile;
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

        public async Task<bool> IsCurrentUser(string userid, string slug)
        {
            var userInBrowser  = await _context.Users.SingleOrDefaultAsync(p => p.Slug == slug);
            if (userInBrowser.Id == userid)
                return true;
            return false;
        }

        public async Task<UserEditProfileViewModel> GetEditProfile(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.Id == id);
            UserEditProfileViewModel editProfile = new UserEditProfileViewModel
            {
                Id = id,
                About = user.About,
                Email = user.Email,
                Facebook = user.Facebook,
                FullName = user.FullName,
                GooglePlus = user.GooglePlus,
                Image = user.Picture65x65,
                Linkedin = user.Linkedin,
                Twitter = user.Twitter,
                Website = user.Website,
                Slug = user.Slug
            };
            return editProfile;
        }

        public async Task SaveEditProfile(UserEditProfileViewModel editProfile)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.Id == editProfile.Id);
            user.FullName = editProfile.FullName;
            user.About = editProfile.About;
            user.Email = editProfile.Email;
            user.Facebook = editProfile.Facebook;
            user.GooglePlus = editProfile.GooglePlus;
            user.Linkedin = editProfile.Linkedin;
            user.Twitter = editProfile.Twitter;
            user.Website = editProfile.Website;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
