using Microsoft.EntityFrameworkCore;

namespace StockHistory.Data
{
    public class StockHistoryAPIContext : DbContext
    {
        public StockHistoryAPIContext (DbContextOptions<StockHistoryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.PurchasesHistoric> PurchasesHistoric { get; set; }
    }
}
