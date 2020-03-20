using Microsoft.EntityFrameworkCore;
using StockHistory.Models;

namespace StockHistory.Data
{
    public class StockHistoryAPIContext : DbContext
    {
        public StockHistoryAPIContext (DbContextOptions<StockHistoryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<PurchasesHistoric> PurchasesHistoric { get; set; }
        public DbSet<TickerTaticHistoric> TickerTaticHistoric { get; set; }
    }

}
