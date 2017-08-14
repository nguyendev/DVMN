using DoVuiHaiNao.Models.PostViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public interface IPostRepository
    {
        Task<ListPostViewModel> GetListPost(int? page, int? pageSize);
        Task IncreaseView(string slug);
        Task<SinglePostViewModel> GetSinglePost(string slug);
    }
}
