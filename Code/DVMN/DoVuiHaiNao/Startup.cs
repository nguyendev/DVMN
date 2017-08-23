using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DoVuiHaiNao.Data;
using DoVuiHaiNao.Models;
using DoVuiHaiNao.Services;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.ResponseCompression;
using DoVuiHaiNao.Areas.WebManager.Data;

namespace DoVuiHaiNao
{
    public class Startup
    {
        private readonly IHostingEnvironment env;
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            this.env = env;
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMemoryCache();
            //if (env.IsDevelopment())
            //{
            //    services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //}
            //else
            //{ 
            //services.AddResponseCaching();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("VPSConnection")));
            //}
            services.AddIdentity<Member, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.
                AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new Microsoft.AspNetCore.Mvc.CacheProfile()
                    {
                        Duration = 60
                    });
                options.CacheProfiles.Add("Never",
                    new Microsoft.AspNetCore.Mvc.CacheProfile()
                    {
                        Location = Microsoft.AspNetCore.Mvc.ResponseCacheLocation.None,
                        NoStore = true
                    });
            })
            .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
            {
                options.MimeTypes = new[]
                {
            // Default
            "text/plain",
            "text/css",
            "application/javascript",
            "text/html",
            "application/xml",
            "text/xml",
            "application/json",
            "text/json",
            // Custom
            "image/svg+xml"
        };
            });


            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IMultiPuzzleManagerRepsository, MultiPuzzleManagerRepository>();
            services.AddScoped<IHomeRepository, HomeRepository>();
            services.AddScoped<ISinglePuzzleManagerRepository, SinglePuzzleManagerRepository>();
            services.AddScoped<IPuzzleRepository, PuzzleRepository>();
            services.AddScoped<ISidebarRepository, SidebarRepository>();
            services.AddScoped<IMemberManagerRepository, MemberManagerRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostManagerRepository, PostManagerRepository>();
            services.AddScoped<IGalleryRepository, GalleryRepository>();
            // Add Kendo UI services to the services container
            services.AddKendo();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseResponseCompression();
            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();
                    headers.CacheControl = new CacheControlHeaderValue()
                    {
                        MaxAge = TimeSpan.FromSeconds(63072000),
                        

                    };
                    // Cache static file for 1 year
                    //if (!string.IsNullOrEmpty(context.Context.Request.Query["v"]))
                    //{
                    //    context.Context.Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
                    //    context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
                    //}
                }
            });
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            // IMPORTANT: This session call MUST go before UseMvc()
            app.UseSession();


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                LoginPath = "/quan-ly-web/dang-nhap",
                AuthenticationScheme = "Cookies",

                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            if (env.IsDevelopment())
            {
                //connect facebook localhost
                app.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = "107378839934718",
                    AppSecret = "03a1fbbb98f50059c29c7d2bc209ec21"
                });
                

            }
            else
            {
                //connect facebook in vps
                app.UseFacebookAuthentication(new FacebookOptions()
                {
                    AppId = "283779168758487",
                    AppSecret = "16cbd9eafd6d2b5f6c1fb8b2fda3b1c6"
                });
            }
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areaRoute",
                    template: "{area:exists}/{controller=Admin}/{action=Index}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // Configure Kendo UI
            app.UseKendo(env);
            //app.UseResponseCaching();
            //app.Run(async (context) =>
            //{
            //    context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
            //    {
            //        Public = true,
            //        MaxAge = TimeSpan.FromSeconds(10)
            //    };
            //    context.Response.Headers[HeaderNames.Vary] = new string[] { "Accept-Encoding" };

            //    await context.Response.WriteAsync("Hello World! " + DateTime.UtcNow);
            //});
        }
    }
}
