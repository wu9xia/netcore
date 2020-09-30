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

        [HttpPost("/api/[controller]/GetPictures")]
        public dynamic GetPictures([FromBody] PictureModel model)
        {
            try
            {
                List<string> files = new List<string>();
                Models.PictureModel dics = ConfigurationManager.GetSection<Models.PictureModel>("Picture");

                FileInfo[] arrFi = null;
                if (dics != null && dics.PictureDics.Count > 0)
                {
                    List<PictureDic> dicsRec = dics.PictureDics;
                    foreach (PictureDic dic in dicsRec)
                    {
                        if (Directory.Exists(dic.Path))
                        {
                            //Sort Files
                            DirectoryInfo di = new DirectoryInfo(dic.Path);

                            arrFi = di.GetFiles("*.*");
                            SortAsFileName(ref arrFi);

                            
                            List<string> pictures = Directory.GetFiles(dic.Path).ToList();
                            arrFi.Skip((model.Page - 1) * model.PageSize).Take(model.PageSize).ToList().ForEach(pic =>
                            {
                                files.Add("https://" + Current.Request.Host.Value + "/" + dic.Path.Split('\\')[dic.Path.Split('\\').Length - 1] + "/" + pic.Name);
                            });
                            //List<string> pictures = Directory.GetFiles(dic.Path).ToList();
                            //pictures.ForEach(pic =>
                            //{
                            //    files.Add("https://" + Current.Request.Host.Value + "/" + dic.Path.Split('\\')[dic.Path.Split('\\').Length - 1] + "/" + Path.GetFileName(pic));
                            //});
                        }

                    }

                }
                int FileCount = 0;
                if (arrFi != null) FileCount = arrFi.Length;
                return new { success = true, result = files, count = FileCount };
            }
            catch (Exception ex)
            {

            }
            return new { success = false };
        }

        #region private method
        /// <summary>
        　　/// C#按文件名排序（顺序）
        　　/// </summary>
        　　/// <param name="arrFi">待排序数组</param>
        private void SortAsFileName(ref FileInfo[] arrFi)
        {
            Array.Sort(arrFi, delegate (FileInfo x, FileInfo y) { return x.Name.CompareTo(y.Name); });
        }


        #endregion
    }
}
