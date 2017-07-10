using DVMN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Services
{
    public interface IMultiPuzzle
    {
        //List<PuzzleType> GetPuzzleType();
        Task<MultiPuzzle> Get(int? id);
        bool Exists(int id);
        Task<List<MultiPuzzle>> GetAll();
        Task Add(MultiPuzzle model);
        Task Update(MultiPuzzle model);
        Task Delete(int id);

    }
}
