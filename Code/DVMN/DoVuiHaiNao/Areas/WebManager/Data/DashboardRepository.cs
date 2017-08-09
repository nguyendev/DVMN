﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoVuiHaiNao.Areas.WebManager.ViewModels.DashboardViewModels;
using DoVuiHaiNao.Data;
using Microsoft.EntityFrameworkCore;

namespace DoVuiHaiNao.Areas.WebManager.Data
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        public DashboardRepository (ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task<int> CountSingleQuestionInDay()
        {
            return await _context.SinglePuzzle
                .Where(p => p.CreateDT.Value.Date == DateTime.Now.Date)
                .Where(p => p.Approved == "A" && !p.IsDeleted)
                .CountAsync();
        }
        private async Task<int> CountSingleQuestionInWeek()
        {
            return await _context.SinglePuzzle
                .Where(p => p.CreateDT.Value.Day + 7 >= DateTime.Now.Day)
                .Where(p => p.CreateDT.Value.DayOfWeek <= DateTime.Now.DayOfWeek)
                .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
                .Where(p => p.CreateDT.Value.Year >= DateTime.Now.Year)
                .Where(p => p.Approved == "A" && !p.IsDeleted)
                .CountAsync();
        }
        private async Task<int> CountSingleQuestionInMonth()
        {
            return await _context.SinglePuzzle
                .Where(p => p.CreateDT.Value.Day <= DateTime.Now.Date.Day)
                .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
                .Where(p => p.CreateDT.Value.Year == DateTime.Now.Year)
                .Where(p => p.Approved == "A" && !p.IsDeleted)
                .CountAsync();
        }
        private async Task<int> CountSingleQuestionTotal()
        {
            return await _context.SinglePuzzle
                .Where(p => p.Approved == "A" && !p.IsDeleted)
                .CountAsync();
        }
        private async Task<int> CountMemberInDay()
        {
            return await _context.Users
                .Where(p => p.CreateDT.Value.Date == DateTime.Now.Date)
                .CountAsync();
        }
        private async Task<int> CountMemberInWeek()
        {
            return await _context.Users
                .Where(p => p.CreateDT.Value.Day + 7 > DateTime.Now.Day)
                .Where(p => p.CreateDT.Value.DayOfWeek <= DateTime.Now.DayOfWeek)
                .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
                .Where(p => p.CreateDT.Value.Year == DateTime.Now.Year)
                .CountAsync();
        }
        private async Task<int> CountMemberInMonth()
        {
            return await _context.Users
                .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
                .Where(p => p.CreateDT.Value.Year == DateTime.Now.Year)
                .CountAsync();
        }
        private async Task<int> CountMemberTotal()
        {
            return await _context.Users
                .CountAsync();
        }
        private async Task<int> CountMultiQuestionInDay()
        {
            return await _context.MultiPuzzle
               .Where(p => p.CreateDT.Value.Date == DateTime.Now.Date)
               .Where(p => p.Approved == "A" && !p.IsDeleted)
               .CountAsync();
        }
        private async Task<int> CountMultiQuestionInWeek()
        {
            return await _context.MultiPuzzle
                 .Where(p => p.CreateDT.Value.Day + 7 >= DateTime.Now.Day)
                 .Where(p => p.CreateDT.Value.DayOfWeek <= DateTime.Now.DayOfWeek)
                 .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
                 .Where(p => p.CreateDT.Value.Year >= DateTime.Now.Year)
                 .Where(p => p.Approved == "A" && !p.IsDeleted)
                 .CountAsync();
        }
        private async Task<int> CountMultiQuestionInMonth()
        {
            return await _context.MultiPuzzle
               .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
               .Where(p => p.CreateDT.Value.Year == DateTime.Now.Year)
               .Where(p => p.Approved == "A" && !p.IsDeleted)
               .CountAsync();
        }
        private async Task<int> CountMultiQuestionTotal()
        {
            return await _context.MultiPuzzle
               .Where(p => p.Approved == "A" && !p.IsDeleted)
               .CountAsync();
        }
        private async Task<int> CountLikePuzzleInDay()
        {
            return await _context.HistoryLikePuzzle
               .Where(p => p.CreateDT.Value.Date == DateTime.Now.Date)
               .CountAsync();
        }
        private async Task<int> CountLikePuzzleInWeek()
        {
            return await _context.HistoryLikePuzzle
               .Where(p => p.CreateDT.Value.Day + 7 >= DateTime.Now.Day)
               .Where(p => p.CreateDT.Value.DayOfWeek <= DateTime.Now.DayOfWeek)
               .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
               .Where(p => p.CreateDT.Value.Year == DateTime.Now.Year)
               .CountAsync();
        }
        private async Task<int> CountLikePuzzleInMonth()
        {
            return await _context.HistoryLikePuzzle
               .Where(p => p.CreateDT.Value.Month == DateTime.Now.Month)
                .Where(p => p.CreateDT.Value.Year == DateTime.Now.Year)
               .CountAsync();

        }
        private async Task<int> CountLikePuzzleTotal()
        {
            return await _context.HistoryLikePuzzle
               .CountAsync();
        }

        public async Task<DashboardViewModels> GetIndex()
        {
            DashboardViewModels model = new DashboardViewModels
            {
                CountSingleQuestionInDay = await CountSingleQuestionInDay(),
                CountSingleQuestionInWeek = await CountSingleQuestionInWeek(),
                CountSingleQuestionInMonth = await CountSingleQuestionInMonth(),
                CountSingleQuestionTotal = await CountSingleQuestionTotal(),
                CountMemberInDay = await CountMemberInDay(),
                CountMemberInWeek = await CountMemberInWeek(),
                CountMemberInMonth = await CountMemberInMonth(),
                CountMemberTotal = await CountMemberTotal(),
                CountMultiQuestionInDay = await CountMultiQuestionInDay(),
                CountMultiQuestionInWeek = await CountMultiQuestionInWeek(),
                CountMultiQuestionInMonth = await CountMultiQuestionInMonth(),
                CountMultiQuestionTotal = await CountMultiQuestionTotal(),
                CountLikePuzzleInDay = await CountLikePuzzleInDay(),
                CountLikePuzzleInWeek = await CountLikePuzzleInWeek(),
                CountLikePuzzleInMonth = await CountLikePuzzleInMonth(),
                CountLikePuzzleTotal = await CountLikePuzzleTotal(),

            };
            return model;
        }
        
    }
}
