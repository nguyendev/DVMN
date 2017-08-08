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

namespace DoVuiHaiNao.Areas.WebManager.Controllers
{
    [Area("WebManager")]
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
        public async Task<ActionResult> Index()
        {
            return View(await _context.Images.Include(p => p.Author).ToListAsync());
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
                            
                            var user = await GetCurrentUserAsync();
                            _context.Add(new Models.Images
                            {
                                CreateDT = System.DateTime.Now,
                                Name = fileName,
                                PicFull = "\\" + DIR_IMAGE + "\\1440x900\\" + fileName,
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

            // Remove in folder
            var physicalPath = Path.Combine(HostingEnvironment.WebRootPath, image.PicFull);
            if (System.IO.File.Exists(physicalPath))
            {
                // The files are not actually removed in this demo
                System.IO.File.Delete(physicalPath);
            }
            //physicalPath = Path.Combine(HostingEnvironment.WebRootPath, image.Pic115x175);
            //if (System.IO.File.Exists(physicalPath))
            //{
            //    // The files are not actually removed in this demo
            //    System.IO.File.Delete(physicalPath);
            //}
            //physicalPath = Path.Combine(HostingEnvironment.WebRootPath, image.Pic1300x500);
            //if (System.IO.File.Exists(physicalPath))
            //{
            //    // The files are not actually removed in this demo
            //    System.IO.File.Delete(physicalPath);
            //}
            //physicalPath = Path.Combine(HostingEnvironment.WebRootPath, image.Pic182x268);
            //if (System.IO.File.Exists(physicalPath))
            //{
            //    // The files are not actually removed in this demo
            //    System.IO.File.Delete(physicalPath);
            //}
            //physicalPath = Path.Combine(HostingEnvironment.WebRootPath, image.Pic268x268);
            //if (System.IO.File.Exists(physicalPath))
            //{
            //    // The files are not actually removed in this demo
            //    System.IO.File.Delete(physicalPath);
            //}
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

    }
}