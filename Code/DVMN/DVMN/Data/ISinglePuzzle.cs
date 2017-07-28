using DVMN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public interface ISinglePuzzle
    {
        Task<SinglePuzzle> Get(int? id);
        bool Exists(int id);
        Task<List<SinglePuzzle>> GetAll();
        Task Add(SinglePuzzle model);
        Task Update(SinglePuzzle model);
        Task Delete(int id);
    }
}
