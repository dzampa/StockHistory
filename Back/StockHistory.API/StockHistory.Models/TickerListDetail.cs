namespace StockHistory.Models
{
    public class TickerListDetail
    {
        public string symbol { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string exchange { get; set; }
        public string[] typespecs { get; set; }
        public string country { get; set; }
    }
}
