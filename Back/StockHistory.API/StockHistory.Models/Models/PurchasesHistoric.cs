using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockHistory.Models
{
    [Table("PurchasesHistoric")]
    public class PurchasesHistoric
    {
        [Key]
        public int PurchasesHistoricId { get; set; }
        [Required]
        [StringLength(100)]
        public DateTime DtNegocio { get; set; }
        [Required]
        [StringLength(5)]
        public string TpNegocio { get; set; }
        [Required]
        [StringLength(100)]
        public string TpMercado { get; set; }
        [StringLength(10)]
        public DateTime? DtPrzVcto { get; set; }
        [Required]
        [StringLength(5)]
        public string Ticker { get; set; }
        [Required]
        [StringLength(100)]
        public string DescTicker { get; set; }
        [Required]
        public int Quantidade { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public decimal ValorTot { get; set; }
        public int? FatorCotacao { get; set; }
        [Required]
        public int IdCorretora { get; set; }
    }
}
