using Newtonsoft.Json;

namespace StockHistory.Models.Models
{
    public class TickerDetail
    {
        [JsonProperty("symbols")]
        public Symbols Symbols { get; set; }

        [JsonProperty("columns")]
        public string[] Columns { get; set; }
    }
    public partial class Symbols
    {
        [JsonProperty("tickers")]
        public string[] Tickers { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }
    }

    public partial class Query
    {
        [JsonProperty("types")]
        public object[] Types { get; set; }
    }
}
