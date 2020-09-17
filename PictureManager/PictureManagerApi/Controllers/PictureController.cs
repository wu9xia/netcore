using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PictureManagerApi.Framework.Utility;
using PictureManagerApi.Models;

namespace PictureManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PictureController : ControllerBase
    {
        private static IHttpContextAccessor contextAccessor;
        private IWebHostEnvironment hostingEnv;
        private readonly IConfiguration configuration;
        private readonly ILogger<PictureController> _logger;
        private Microsoft.AspNetCore.Http.HttpContext Current => contextAccessor.HttpContext;

        public PictureController(ILogger<PictureController> logger, IHttpContextAccessor _contextAccessor, IWebHostEnvironment env, IConfiguration _configuration)
        {
            _logger = logger;
            hostingEnv = env;
            configuration = _configuration;
            contextAccessor = _contextAccessor;
        }

        [HttpGet("/api/[controller]/GetPictures")]
        public dynamic GetPictures()
        {
            try
            {
                List<string> files = new List<string>();
                Models.PictureModel dics = ConfigurationManager.GetSection<Models.PictureModel>("Picture");
                if (dics != null && dics.PictureDics.Count > 0)
                {
                    List<PictureDic> dicsRec = dics.PictureDics;
                    foreach (PictureDic dic in dicsRec)
                    {
                        if (Directory.Exists(dic.Path))
                        {
                            List<string> pictures = Directory.GetFiles(dic.Path).ToList();
                            pictures.ForEach(pic =>
                            {
                                files.Add("https://" + Current.Request.Host.Value + "/" + dic.Path.Split('\\')[dic.Path.Split('\\').Length - 1] + "/" + Path.GetFileName(pic));
                            });
                        }

                    }

                }

                return new { success = true, result = files };
            }
            catch (Exception ex)
            {

            }
            return new { success = false };
        }
    }
}
