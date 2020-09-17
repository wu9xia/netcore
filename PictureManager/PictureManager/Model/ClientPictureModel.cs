﻿using Newtonsoft.Json;
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
        public List<string> result { get; set; }
    }
}