using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StockHistory.Models.Models
{
    public class TickerSearch
    {
        [Required]
        [StringLength(100)]
        public string text { get; set; }
        [StringLength(100)]
        public string exchange { get; set; }
        [StringLength(100)]
        public string type { get; set; }
        [Required]
        [StringLength(100)]
        public string hl { get; set; }
        [Required]
        [StringLength(100)]
        public string lang { get; set; }
        [Required]
        [StringLength(100)]
        public string domain { get; set; }
    }
}
