using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHistory.Models
{
    [Table("PurchasesHistoric")]
    public class PurchasesHistoric
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IdPurchasesHistoric { get; set; }
        [Required]
        [StringLength(100)]
        public DateTime DtBusiness { get; set; }
        [Required]
        [StringLength(5)]
        public string TpBusiness { get; set; }
        [Required]
        [StringLength(100)]
        public string TpMarket { get; set; }
        [StringLength(10)]
        public DateTime? DtExp { get; set; }
        [Required]
        [StringLength(5)]
        public string Ticker { get; set; }
        [Required]
        [StringLength(100)]
        public string DescTicker { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal TotValue { get; set; }
        public int? QutFac { get; set; }
        [Required]
        public int IdBroker { get; set; }
    }
}
