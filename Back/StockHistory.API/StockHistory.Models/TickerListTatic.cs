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
        public int QtdeCompra { get; set; }
        [Required]
        public int QtdeVenda { get; set; }
        [Required]
        public decimal ValorCompra { get; set; }
        [Required]
        public decimal ValorVenda { get; set; }
        [Required]
        public decimal ValorMedCompra { get; set; }
        [Required]
        public decimal ValorMedVenda { get; set; }
        [Required]
        public int QtdeLiq { get; set; }
    }
}
