using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockHistory.Models
{
    public partial class TickerSearchDetail
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }

        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("s")]
        public string S { get; set; }

        [JsonProperty("d")]
        public double[] D { get; set; }
    }
}
