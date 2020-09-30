using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PictureManagerApi.Framework.Utility;
using PictureManagerApi.Middleware;
using Newtonsoft.Json;

namespace PictureManagerApi
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddWebApiConventions();
            services.AddControllers();
            
            services.AddControllersWithViews();
            //services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseFileProvider();
            app.UseStaticFiles();

            //¼ÓÔØ¾²Ì¬Í¼Æ¬Ä¿Â¼
            Models.PictureModel dics = ConfigurationManager.GetSection<Models.PictureModel>("Picture");
            foreach (var dic in dics.PictureDics)
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                                    dic.Path
                                    ),
                    RequestPath = new PathString("/" + dic.Path.Split('\\')[dic.Path.Split('\\').Length - 1]),
                });
            }

            app.UseHttpsRedirection();
            //string basePath = @"D:\private\pictures";
            //List<string> pictures = LoadPicture(basePath);

            app.UseRouting();
           
            app.UseAuthorization();

            app.UseCors(config =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyMethod();
                config.AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private List<string> LoadPicture(string basePath)
        {
            var pictures = new List<string>();
            string[] files = System.IO.Directory.GetFiles(basePath);
            pictures = files.ToList<string>();
            return pictures;
        }
    }
}
