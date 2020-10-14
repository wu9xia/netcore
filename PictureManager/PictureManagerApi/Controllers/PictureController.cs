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

                            arrFi = di.GetFiles("*.jpg|*.ico|*.png");
                            SortAsFileName(ref arrFi);


                            //List<string> pictures = Directory.GetFiles(dic.Path).ToList();
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

        [HttpPost("/api/[controller]/GetLoopPictures")]
        public dynamic GetLoopPictures([FromBody] PictureModel model)
        {
            try
            {
                Dictionary<string, List<string>> dicPictures = new Dictionary<string, List<string>>();
                List<string> files = new List<string>();
                Models.PictureModel dics = ConfigurationManager.GetSection<Models.PictureModel>("Picture");
                FileInfo[] arrFi = null;
                if (dics != null && dics.PictureDics.Count > 0)
                {
                    //List<DirectoryInfo> diList = null;
                    if (string.IsNullOrEmpty(model.Folder))
                    {
                        //diList = GetAllDirectory(dics.PictureDics[0].Path);
                        Dictionary<string, List<string>> di = GetCurrentDirectoryIncludeFiles(dics.PictureDics[0].Path, dics.PictureDics[0].Path, model);
                        return new { success = true, result = di, count = di["folder"].Count + di["files"].Count};
                    }
                    else
                    {
                        //diList = GetAllDirectory(model.Folder);
                        Dictionary<string, List<string>> di = GetCurrentDirectoryIncludeFiles(model.Folder, dics.PictureDics[0].Path, model);
                        return new { success = true, result = di, count = di["folder"].Count + di["files"].Count };
                    }
                    
                }
                return new { success = true, count = 0 };
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


        #region 查询目录
        /// <summary>
        /// 获取当前目录下的文件夹和文件，不嵌套
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="configPath">配置路径</param>
        /// <param name="model">Model</param>
        /// <returns></returns>
        private Dictionary<string, List<string>> GetCurrentDirectoryIncludeFiles(string rootPath, string configPath, PictureModel model)
        {

            if (!Directory.Exists(rootPath))
                return new Dictionary<string, List<string>>();

            Dictionary<string, List<string>> filesIncludeFolder = new Dictionary<string, List<string>>();
            DirectoryInfo directoryCurrent = new DirectoryInfo(rootPath);
            var childArray = directoryCurrent.GetDirectories();
            //查询文件夹
            List<string> folderList = new List<string>();
            childArray.ToList().ForEach(d =>
            {
                folderList.Add("https://" + Current.Request.Host.Value + "/pictures" + d.FullName.Substring(configPath.Length).Replace("\\", "//"));
            });
            filesIncludeFolder.Add("folder", folderList);
            //查询图片
            //查找数量减去文件夹的数量
            FileInfo[] arrFi = null;
            arrFi = directoryCurrent.GetFiles("*.*")
                .Where(fn=>fn.FullName.EndsWith(".jpg") || fn.FullName.EndsWith(".ico") || fn.FullName.EndsWith(".png"))
                .Skip((model.Page - 1) * (model.PageSize - folderList.Count)).Take(model.PageSize).ToArray();
            SortAsFileName(ref arrFi);
            List<string> fileList = new List<string>();
            arrFi.ToList().ForEach(d =>
            {
                fileList.Add("https://" + Current.Request.Host.Value + "/pictures" + d.FullName.Substring(configPath.Length).Replace("\\", "//"));
            });
            filesIncludeFolder.Add("files", fileList);

            return filesIncludeFolder;
        }

        /// <summary>
        /// 找出全部的子文件夹
        /// </summary>
        /// <param name="rootPath">根目录</param>
        /// <returns></returns>
        private List<DirectoryInfo> GetAllDirectory(string rootPath)
        {
            if (!Directory.Exists(rootPath))
                return new List<DirectoryInfo>();

            List<DirectoryInfo> directoryList = new List<DirectoryInfo>();//容器
            DirectoryInfo directory = new DirectoryInfo(rootPath);//root文件夹
            directoryList.Add(directory);

            return GetChild(directoryList, directory);


        }

        /// <summary>
        /// 找出所有文件夹--子目录--放入集合
        /// </summary>
        /// <param name="directoryList"></param>
        /// <param name="directoryCurrent"></param>
        /// <returns></returns>
        private List<DirectoryInfo> GetChild(List<DirectoryInfo> directoryList, DirectoryInfo directoryCurrent)
        {
            var childArray = directoryCurrent.GetDirectories();
            if (childArray != null && childArray.Length > 0)
            {
                directoryList.AddRange(childArray);
                foreach (var child in childArray)
                {
                    GetChild(directoryList, child);
                }
            }
            return directoryList;
        }
        #endregion
        #endregion
    }
}
