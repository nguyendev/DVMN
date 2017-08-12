using DoVuiHaiNao.Extension;
using DoVuiHaiNao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Areas.WebManager.Data
{
    public interface IMemberManagerRepository
    {
        Task<Member> Get(string id);
        bool Exists(string id);
        Task<PaginatedList<Member>> GetAll(string sortOrder, string searchString,
    int? page, int? pageSize);
        Task Update(Member model);
        Task Delete(string id);

        Task<Member> GetEdit(string ID);
    }
}
