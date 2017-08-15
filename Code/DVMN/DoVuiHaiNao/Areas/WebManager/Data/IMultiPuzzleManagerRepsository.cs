using DoVuiHaiNao.Areas.WebManager.ViewModels.MultiPuzzleViewModels;
using DoVuiHaiNao.Areas.WebManager.ViewModels.SinglePuzzleViewModels;
using DoVuiHaiNao.Extension;
using DoVuiHaiNao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Services
{
    public interface IMultiPuzzleManagerRepsository
    {
        //List<PuzzleType> GetPuzzleType();
        Task<MultiPuzzle> Get(int? id);
        bool Exists(int id);
        Task<PaginatedList<MultiPuzzle>> GetAll(string sortOrder, string searchString,
    int? page, int? pageSize);
        Task Add(MultiPuzzle model);
        Task Update(EditMultiPuzzleViewModel model);
        Task Delete(int id);
        Task<EditMultiPuzzleViewModel> GetEdit(int? id);
        Task<PublishDatetimeSinglePuzzleViewModel> GetEditPublishDT(int? ID);
        Task UpdatePublishDT(PublishDatetimeSinglePuzzleViewModel model);
    }
}
