using DoVuiHaiNao.Models.TagViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public interface ITagRepository
    {
        Task<TagSingleViewModel> GetSingle(string slug, int? page, int? pageSize);
        Task<IEnumerable<AllTagViewModel>> GetAllTag();
    }
}
