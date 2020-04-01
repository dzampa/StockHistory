using System.ComponentModel.DataAnnotations;

namespace StockHistory.Models
{
    public class TickerListTatic
    {
        public string Ticker { get; set; }
        [Required]
        [StringLength(100)]
        public string DescTicker { get; set; }
        [Required]
        public int PurchaseAmt { get; set; }
        [Required]
        public int SaleAmt { get; set; }
        [Required]
        public decimal PurchasePrc { get; set; }
        [Required]
        public decimal SalePrc { get; set; }
        [Required]
        public decimal AverPurcAmt { get; set; }
        [Required]
        public decimal AverSaleAmt { get; set; }
        [Required]
        public int NetAmt { get; set; }
    }
}
