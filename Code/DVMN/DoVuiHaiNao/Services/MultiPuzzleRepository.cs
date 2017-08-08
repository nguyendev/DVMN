using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Data;
using Microsoft.EntityFrameworkCore;

namespace DoVuiHaiNao.Services
{
    public class MultiPuzzleRepository : IMultiPuzzle
    {
        protected readonly ApplicationDbContext _context;
        public MultiPuzzleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(MultiPuzzle model)
        {
            _context.Add(model);
            await Save();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MultiPuzzle> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MultiPuzzle>> GetAll()
        {
            return await _context.MultiPuzzle.ToListAsync();
        }

        //public Task<List<MultiPuzzle>> GetAll()
        //{
        //    return _context.Puzzle.ToListAsync();

        //}

        public Task Update(MultiPuzzle model)
        {
            throw new NotImplementedException();
        }

        //public List<PuzzleType> GetPuzzleType()
        //{
        //    return _context.PuzzleType.ToList();
        //}

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
