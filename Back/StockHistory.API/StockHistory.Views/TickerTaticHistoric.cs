using System.ComponentModel.DataAnnotations;

namespace StockHistory.Models
{
    public class TickerTaticHistoric
    {
        [Key]
        public string Ticker { get; set; }
        public string DescTicker { get; set; }
        public int Amount { get; set; }
        public decimal Cost { get; set; }
        public decimal AverCost { get; set; }
    }
}
