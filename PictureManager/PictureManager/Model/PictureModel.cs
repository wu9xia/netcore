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
        [JsonProperty(PropertyName = "pictureDics", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public List<PictureDic> PictureDics { get; set; }
        [JsonProperty(PropertyName = "page", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public int Page { get; set; } = 1;
        [JsonProperty(PropertyName = "pageSize", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public int PageSize { get; set; } = 10;
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class PictureDic
    {
        [JsonProperty(PropertyName = "path", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }
    }
}
