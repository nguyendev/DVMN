using DoVuiHaiNao.Areas.WebManager.ViewModels.SinglePuzzleViewModels;
using DoVuiHaiNao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoVuiHaiNao.Data
{
    public interface ISinglePuzzle
    {
        Task<SinglePuzzle> Get(int? id);
        bool Exists(int id);
        Task<List<SinglePuzzle>> GetAll();
        Task Add(CreateSinglePuzzleViewModel model);
        Task Update(SinglePuzzle model);
        Task Delete(int id);
    }
}
