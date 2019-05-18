// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

using Newtonsoft.Json;

namespace BagongTipan.UWP
{
    public class Contents
    {
        [JsonProperty("libro")]
        public string BookTitle { get; set; }

        [JsonProperty("kabanata")]
        public string Chapter { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("verse")]
        public string Verse { get; set; }
    }
}
