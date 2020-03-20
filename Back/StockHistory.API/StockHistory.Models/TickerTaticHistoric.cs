using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockHistory.Models
{
    public class TickerTaticHistoric
    {
        [Key]
        public string Ticker { get; set; }
        public string DescTicker { get; set; }
        public int Qnt { get; set; }
        public decimal Custo { get; set; }
        public decimal PrcMedio { get; set; }
    }
}
