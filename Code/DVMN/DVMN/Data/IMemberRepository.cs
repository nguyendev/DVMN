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
    }
}
