using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Data;
using System.Net.Http;
using Microsoft.Extensions.FileProviders;
using ImageSharp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DoVuiHaiNao.Areas.WebManager.ViewModels;
using System;
using DoVuiHaiNao.Extension;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoVuiHaiNao.Areas.WebManager.Controllers
{
    [Area("WebManager")]
    [Authorize(Roles = "Admin, Manager")]
    public class MediaController : Controller
    {
        public static string DIR_IMAGE = "images";
        private ApplicationDbContext _context;
        private UserManager<Member> _userManager;
        private readonly IFileProvider _fileProvider;
        public IHostingEnvironment HostingEnvironment { get; set; }

        public MediaController(IHostingEnvironment hostingEnvironment,
            ApplicationDbContext context,
            UserManager<Member> userManager)
        {
            HostingEnvironment = hostingEnvironment;
            _fileProvider = hostingEnvironment.WebRootFileProvider;
            _context = context;
            _userManager = userManager;
        }
        [Route("/quan-ly-web/thu-vien")]
        public async Task<ActionResult> Index(string sortOrder,
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
            ViewData["NameParm"] = String.IsNullOrEmpty(sortOrder) ? "name" : "";
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
            var images = from s in _context.Images
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                images = images.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    images = images.OrderBy(s => s.Name);
                    break;
                default:
                    images = images.OrderByDescending(s => s.CreateDT);
                    break;
            }

            return View(await PaginatedList<Images>.CreateAsync(images.AsNoTracking(), page ?? 1, pageSize != null ? pageSize.Value : 10));
        }
        [Route("/quan-ly-web/thu-vien/them-moi")]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<ActionResult> Save(IEnumerable<IFormFile> files)
        {
            // The Name of the Upload component is "files"
            if (files != null)
            {
                foreach (var file in files)
                {
                    var fileContent = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

                    // Some browsers send file names with full path.
                    // We are only interested in the file name.
                    var fileName = Path.GetFileName(fileContent.FileName.Trim('"'));
                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE, fileName);
                    if (file.Length > 0)
                    {
                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                            // Image.Load(string path) is a shortcut for our default type. Other pixel formats use Image.Load<TPixel>(string path))
                        }

                        using (Image<Rgba32> image = ImageSharp.Image.Load(physicalPath))
                        {
                            var folderSave = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE + "\\1440X900", fileName);
                            image.Resize(1440, 900)
                                 .Save(folderSave); // automatic encoder selected based on extension.
                            folderSave = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE + "\\640X480", fileName);
                            image.Resize(640, 480)
                                 .Save(folderSave); // automatic encoder selected based on extension.

                            var user = await GetCurrentUserAsync();
                            _context.Add(new Models.Images
                            {
                                CreateDT = System.DateTime.Now,
                                Name = fileName,
                                PicFull = "\\" + DIR_IMAGE + "\\1440x900\\" + fileName,
                                Pic640x480 = "\\" + DIR_IMAGE + "\\640x480\\" + fileName,
                                Active = "A",
                                Approved = "A",
                                AuthorID = user.Id,
                                IsDeleted = false
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        [HttpPost]
        [Route("/quan-ly-web/thu-vien/xoa/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            // Remove in database
            var image = await _context.Images.SingleOrDefaultAsync(p => p.ID == id);
            if (image == null)
                return NotFound();
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            string root = HostingEnvironment.WebRootPath;            // Remove in folder
            string physicalPath = Path.Combine(root + "\\images\\", image.Name);
            if (System.IO.File.Exists(physicalPath))
            {
                // The files are not actually removed in this demo
                System.IO.File.Delete(physicalPath);
            }
            string physicalPathPicFull = Path.Combine(root, image.PicFull);
            if (System.IO.File.Exists(physicalPathPicFull))
            {
                // The files are not actually removed in this demo
                System.IO.File.Delete(physicalPathPicFull);
            }
            string physicalPathPic640x480 = Path.Combine(root, image.Pic640x480);
            if (System.IO.File.Exists(physicalPathPic640x480))
            {
                // The files are not actually removed in this demo
                System.IO.File.Delete(physicalPathPic640x480);
            }
            return RedirectToAction("Index");
        }
        private async Task<Member> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
        public ActionResult Remove(string[] fileNames)
        {
            // The parameter of the Remove action must be called "fileNames"

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, DIR_IMAGE, fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        System.IO.File.Delete(physicalPath);
                    }
                }
            }

            // Return an empty string to signify success
            return Content("");
        }
        [Route("/quan-ly-web/thu-vien/chi-tiet/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .Include(i => i.Author)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }


        // GET: WebManager/Images/Edit/5
        [Route("/quan-ly-web/thu-vien/chinh-sua/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _context.Images.SingleOrDefaultAsync(m => m.ID == id);
            if (images == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", images.AuthorID);
            return View(images);
        }

        // POST: WebManager/Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("/quan-ly-web/thu-vien/chinh-sua/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ALT,Title,PicFull,Pic640x480,Approved,IsDeleted,Note")] Images images)
        {
            if (id != images.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    images.UpdateDT = DateTime.Now;
                    _context.Update(images);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagesExists(images.ID))
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
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", images.AuthorID);
            return View(images);
        }
        [Route("/quan-ly-web/thu-vien/xoa/{id}")]
        // GET: WebManager/Images/Delete/5
        public async Task<IActionResult> Delete(int? id, string temp=null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .Include(i => i.Author)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (images == null)
            {
                return NotFound();
            }

            return View(images);
        }

        private bool ImagesExists(int id)
        {
            return _context.Images.Any(e => e.ID == id);
        }

    }
}