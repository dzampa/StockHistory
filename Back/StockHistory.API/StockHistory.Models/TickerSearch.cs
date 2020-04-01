using Newtonsoft.Json;

namespace StockHistory.Models
{
    public class TickerSearch
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("exchange")]
        public string Exchange { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("hl")]
        public string HL { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
