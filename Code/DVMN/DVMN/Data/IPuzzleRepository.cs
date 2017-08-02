using DVMN.Models.PuzzleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public interface IPuzzleRepository
    {
        // Xem chi tiet cau do don
        Task<SingleSinglePuzzleViewModel> GetSingleSinglePuzzle(string slug);
        Task<IEnumerable<SingleSinglePuzzleViewModel>> GetSingleMultiPuzzle(string slug);
        // Da xem cau nay
        Task IsWatchedSingleSinglePuzzle(string slug);
        Task IsWatchedSingleMultiPuzzle(string slug);

        // Da tra loi cau hoi nay
        Task<bool> IsAnswerPuzzle(int PuzzleID, string AuthorID);

        Task SetIsAnsweredPuzzle(int PuzzleID, string AuthorID);

        // Tang diem khi tra loi dung
        Task IncreasePoint(string userID, int point);
    }
}
