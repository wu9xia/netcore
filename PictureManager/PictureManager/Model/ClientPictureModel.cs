using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureManager.Model
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class ClientPictureModel
    {
        [JsonProperty(PropertyName = "success", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public bool success { get; set; }
        [JsonProperty(PropertyName = "result", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public Dictionary<string, List<string>> result { get; set; }

        [JsonProperty(PropertyName = "count", DefaultValueHandling = DefaultValueHandling.Include, NullValueHandling = NullValueHandling.Include)]
        public int Count { get; set; }
    }
}
