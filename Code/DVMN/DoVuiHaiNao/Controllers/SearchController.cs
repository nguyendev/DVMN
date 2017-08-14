using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoVuiHaiNao.Data;

namespace DoVuiHaiNao.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchRepository _repository;
        private readonly ISidebarRepository _sidebarRepository;

        public SearchController(ISearchRepository repository,
            ISidebarRepository sidebarRepository)
        {
            _repository = repository;
            _sidebarRepository = sidebarRepository;
        }
        [Route("/tim-kiem/")]
        public async Task<IActionResult> Search(string search,int? page)
        {
            ViewData["listSinglePuzzle"] = await _repository.GetSearch(search,page,10);
            ViewData["Search"] = search;
            ViewData["sidebar"] = await _sidebarRepository.GetAllSibar();
            return View();
        }
    }
}