using DVMN.Extension;
using DVMN.Models;
using DVMN.Models.PuzzleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DVMN.Data
{
    public interface IPuzzleRepository
    {
        Task<ListSinglePuzzleViewModel> ListSinglePuzzle(int? page, int? pageSize);
        Task<ListMultiPuzzleViewModel> ListMultiPuzzle(int? page, int? pageSize);
        // Xem chi tiet cau do don

        Task<SingleSinglePuzzleViewModel> GetSingleSinglePuzzle(string slug);
        Task<SingleMultiPuzzleViewModel> GetSingleMultiPuzzle(string slug);
        // Da xem cau nay
        Task IsWatchedSingleSinglePuzzle(string slug);
        Task IsWatchedSingleMultiPuzzle(string slug);

        // Da tra loi cau hoi nay
        Task<bool> IsAnswerPuzzle(int PuzzleID, string AuthorID);

        Task SetIsAnsweredPuzzle(int PuzzleID, string AuthorID,bool IsMulti);

        // Tang diem khi tra loi dung
        Task IncreasePoint(string userID, int point);


        // Giam luot like
        Task<bool> VoteDownPuzzle(int puzzle, string UserId);
        Task<bool> VoteUpPuzzle(int puzzle, string UserId);
    }
}
