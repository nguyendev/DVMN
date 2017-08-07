using DVMN.Models.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public interface IMemberRepository
    {
        Task<ListMemberViewModel> GetAllMember(int ? page, int? size);
        Task<ListTopMemberViewModel> GetTopMember(int? page, int? pageSize);

        Task<UserProfileViewModel> GetProfile(string slug);

        Task<UserHistorySinglePuzzleViewModel> GetHistoryListSinglePuzzle(string slug, int? page, int? pageSize);

        Task<UserHistoryMultiPuzzleViewModel> GetHistoryListMultiPuzzle(string slug, int? page, int? pageSize);
        Task<bool> IsCurrentUser(string userid, string slug);

        Task<UserEditProfileViewModel> GetEditProfile(string id);
        Task SaveEditProfile(UserEditProfileViewModel editProfile);
    }
}
