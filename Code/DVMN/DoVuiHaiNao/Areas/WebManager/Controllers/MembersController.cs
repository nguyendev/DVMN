using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoVuiHaiNao.Areas.WebManager.Data;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Areas.WebManager.ViewModels;
using DoVuiHaiNao.Extension;
using Microsoft.AspNetCore.Authorization;

namespace DoVuiHaiNao.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    [Authorize(Roles = "Admin, Manager")]
    public class MembersController : Controller
    {
        private readonly IMemberManagerRepository _repository;

        public MembersController(IMemberManagerRepository repository)
        {
            _repository = repository;    
        }

        // GET: WebManager/Members
        [Route("/quan-ly-web/thanh-vien")]
        public async Task<IActionResult> Index(string sortOrder,
 string currentFilter,
    string searchString,
    int? page, int? pageSize)
        {
            List<NumberItem> SoLuong = new List<NumberItem>
            {
                new NumberItem { Value = 10},
                new NumberItem { Value = 20},
                new NumberItem { Value = 50},
                new NumberItem { Value = 100},
            };
            ViewData["SoLuong"] = SoLuong;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["FullNameParm"] = String.IsNullOrEmpty(sortOrder) ? "fullName" : "";
            ViewData["CurrentSize"] = pageSize;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            PaginatedList<Member> temp = await _repository.GetAll(sortOrder, searchString, page, pageSize);
            return View(temp);
        }

        // GET: WebManager/Members/Details/5
        [Route("/quan-ly-web/thanh-vien/chi-tiet/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _repository.Get(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }
        [Route("/quan-ly-web/thanh-vien/chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _repository.Get(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: WebManager/Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/quan-ly-web/thanh-vien/chinh-sua/{id}")]
        public async Task<IActionResult> Edit(string id, [Bind("FullName,About,PictureSmall,Picture65x65,Slug,PictureBig,DateofBirth,Facebook,GooglePlus,Linkedin,Twitter,IdentityFacebook,Score,Website,CreateDT,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(member);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: WebManager/Members/Delete/5
        [Route("/quan-ly-web/thanh-vien/xoa/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _repository.Get(id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }
        [Route("/quan-ly-web/thanh-vien/xoa/{id}")]
        // POST: WebManager/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _repository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool MemberExists(string id)
        {
            return _repository.Exists(id);
        }
    }
}
