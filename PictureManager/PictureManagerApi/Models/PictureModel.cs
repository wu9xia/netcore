using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureManagerApi.Models
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class PictureModel
    {
        [JsonProperty(PropertyName = "pictureDics", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public List<PictureDic> PictureDics { get; set; }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class PictureDic
    {
        [JsonProperty(PropertyName = "path", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public string Path { get; set; }
    }
}
