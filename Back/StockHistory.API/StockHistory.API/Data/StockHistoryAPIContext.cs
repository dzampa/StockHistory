using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockHistory.API.Models;

namespace StockHistory.API.Data
{
    public class StockHistoryAPIContext : DbContext
    {
        public StockHistoryAPIContext (DbContextOptions<StockHistoryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<StockHistory.API.Models.PurchasesHistoric> PurchasesHistoric { get; set; }
    }
}
