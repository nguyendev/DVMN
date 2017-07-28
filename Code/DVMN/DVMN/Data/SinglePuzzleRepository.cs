using DVMN.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public class SinglePuzzleRepository : ISinglePuzzle
    {
        protected readonly ApplicationDbContext _context;

        public SinglePuzzleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Add(SinglePuzzle model)
        {
            _context.Add(model);
            await Save();
        }

        public async Task<SinglePuzzle> Get(int? id)
        {
            return await _context.SinglePuzzle.Where(c => c.ID == id).SingleOrDefaultAsync();
        }
        public bool Exists(int id)
        {
            return _context.SinglePuzzle.Any(c => c.ID == id);
        }

        public async Task<List<SinglePuzzle>> GetAll()
        {
            return await _context.SinglePuzzle.ToListAsync();
        }

        public async Task Update(SinglePuzzle Entity)
        {
            _context.SinglePuzzle.Update(Entity);
            await Save();
        }

        public async Task Delete(int id)
        {
            var temp = await _context.SinglePuzzle.SingleOrDefaultAsync(m => m.ID == id);
            _context.SinglePuzzle.Remove(temp);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
