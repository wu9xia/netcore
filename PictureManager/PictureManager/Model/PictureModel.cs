using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureManager.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class PictureModel
    {
        /// <summary>
        /// 指定当前查询目录
        /// </summary>
        [JsonProperty(PropertyName = "folder", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Folder { get; set; }
        [JsonProperty(PropertyName = "pictureDics", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public List<PictureDic> PictureDics { get; set; }
        [JsonProperty(PropertyName = "page", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public int Page { get; set; } = 1;
        [JsonProperty(PropertyName = "pageSize", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public int PageSize { get; set; } = 10;
        [JsonProperty(PropertyName = "module", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Module { get; set; }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class PictureDic
    {
        [JsonProperty(PropertyName = "path", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }
    }
}
