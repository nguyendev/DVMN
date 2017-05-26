using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DVMN.Data;
using DVMN.Services;

namespace DVMN.Controllers
{
    public class SitemapController : Controller
    {
        private readonly ApplicationDbContext _blogService;
        public SitemapController(ApplicationDbContext blogService)
        {
            _blogService = blogService;
        }

        [Route("sitemap")]
        public async Task<ActionResult> SitemapAsync()
        {
            string baseUrl = "https://developingsoftware.com/";

            // get a list of published articles
            //var posts = await _blogService.PostService.GetPostsAsync();

            // get last modified date of the home page
            var siteMapBuilder = new SitemapBuilder();

            // add the home page to the sitemap
            siteMapBuilder.AddUrl(baseUrl, modified: DateTime.UtcNow, changeFrequency: ChangeFrequency.Weekly, priority: 1.0);

            // add the blog posts to the sitemap
            //foreach (var post in posts)
            //{
            //    siteMapBuilder.AddUrl(baseUrl + post.Slug, modified: post.Published, changeFrequency: null, priority: 0.9);
            //}

            // generate the sitemap xml
            string xml = siteMapBuilder.ToString();
            return Content(xml, "text/xml");
        }
    }
}