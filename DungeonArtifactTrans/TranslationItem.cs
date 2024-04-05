using System.ComponentModel;
using Newtonsoft.Json;

namespace catrice.DungeonArtifactTrans
{
    public struct TranslationItem
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }

        [JsonProperty("translation")]
        public string Translation { get; set; }

        [JsonProperty("context")]
        public object Context { get; set; }
        
        [DefaultValue(false)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool IsReplaced { get; set; }
    }
}