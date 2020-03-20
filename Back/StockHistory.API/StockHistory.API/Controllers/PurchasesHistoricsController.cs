using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockHistory.Data;
using StockHistory.Models;

namespace StockHistory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesHistoricsController : ControllerBase
    {
        private readonly StockHistoryAPIContext _context;

        public PurchasesHistoricsController(StockHistoryAPIContext context)
        {
            _context = context;
        }

        // GET: api/PurchasesHistorics
        /// <summary>
        /// Get all Historics  
        /// </summary> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchasesHistoric>>> GetPurchasesHistoric()
        {
            return await _context.PurchasesHistoric.ToListAsync();
        }

        // GET: api/PurchasesHistorics/5
        /// <summary>
        /// Get a specific register.
        /// </summary>
        /// <param name="id"></param>   
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchasesHistoric>> GetPurchasesHistoric(int id)
        {
            var purchasesHistoric = await _context.PurchasesHistoric.FindAsync(id);

            if (purchasesHistoric == null)
            {
                return NotFound();
            }

            return purchasesHistoric;
        }
        // GET: api/PurchasesHistorics/ByTicker/IVVB11
        /// <summary>
        /// Get a specific register by Ticker name.
        /// </summary>
        /// <param name="ticker"></param>   
        [HttpGet("ByTicker/{ticker}")]
        public async Task<ActionResult<List<PurchasesHistoric>>> GetPurchasesHistoricByTicker(string ticker)
        {
            var purchasesHistoric = await _context.PurchasesHistoric.Where(x => x.Ticker.Contains(ticker)).ToListAsync();

            if (purchasesHistoric == null)
            {
                return NotFound();
            }

            return purchasesHistoric;
        }

        // GET: api/PurchasesHistorics/TickerListTatic/IVVB11
        /// <summary>
        /// Get a specific register by Ticker name.
        /// </summary>
        /// <param name="ticker"></param>   
        [HttpGet("ByTicker/TaticList/{ticker}")]
        public async Task<ActionResult<List<TickerListTatic>>> GetPurchasesHistoricTickerTaticList(string ticker = null)
        {
            var BuyHistoric = await _context.TickerTaticHistoric.FromSqlRaw(
                                                                         "SELECT DISTINCT " +
                                                                         "    Ticker " +
                                                                         "    , DescTicker " +
                                                                         "    , sum(Quantidade) as Qnt " +
                                                                         "    , sum(Preco * Quantidade) as Custo " +
                                                                         "    , sum(Preco * Quantidade) / sum(Quantidade) as PrcMedio " +
                                                                         "FROM " +
                                                                         "    PurchasesHistoric " +
                                                                         "WHERE " +
                                                                         "    TpNegocio = 'C' " +
                                                                         $"    AND Ticker LIKE '%{ticker}%' "+
                                                                         "GROUP BY " +
                                                                         "    Ticker " +
                                                                         "    , DescTicker " +
                                                                         "ORDER BY " +
                                                                         "    Ticker"
                                                                         ).ToListAsync();

            #region pqnaofunfa
            /*var y = await _context.PurchasesHistoric
                .Where(g => g.TpNegocio == "C" && g.Ticker.Contains(ticker))
                .Select(p => new { 
                    p.Ticker,
                    p.DescTicker,
                    p.Quantidade,
                    p.Preco
                })
                .OrderBy(g => g.Ticker)
                .GroupBy(g => new { 
                    g.Ticker,
                    g.DescTicker
                })
                .Select(y => new
                {
                    Ticker = y.Select(s => s.Ticker),
                    DescTicker = y.Select(s => s.DescTicker),
                    Qnt = y.Sum(s => s.Quantidade),
                    Custo = y.Sum(s => s.Preco * s.Quantidade),
                    PrcMedio = y.Sum(s => s.Preco * s.Quantidade) / y.Sum(s => s.Quantidade)
                })
                .ToListAsync() ;*/
            #endregion

            var SellHistoric = await _context.TickerTaticHistoric.FromSqlRaw(
                                                                         "SELECT DISTINCT " +
                                                                         "    Ticker " +
                                                                         "    , DescTicker " +
                                                                         "    , sum(Quantidade) as Qnt " +
                                                                         "    , sum(Preco * Quantidade) as Custo " +
                                                                         "    , sum(Preco * Quantidade) / sum(Quantidade) as PrcMedio " +
                                                                         "FROM " +
                                                                         "    PurchasesHistoric " +
                                                                         "WHERE " +
                                                                         "    TpNegocio = 'V' " +
                                                                         $"    AND Ticker LIKE '%{ticker}%' " +
                                                                         "GROUP BY " +
                                                                         "    Ticker " +
                                                                         "    , DescTicker " +
                                                                         "ORDER BY " +
                                                                         "    Ticker"
                                                                         ).ToListAsync();

            if (BuyHistoric == null || SellHistoric == null)
            {
                return NotFound();
            }
                       
            List<TickerListTatic> tickerListTatic = new List<TickerListTatic>();

            foreach (var item in BuyHistoric)
            {
                TickerListTatic listTatic = new TickerListTatic();
                int SQnt = 0;
                decimal SCusto = 0;
                decimal SPrcMedio = 0;

                if (SellHistoric.Count > 0)
                {
                    SQnt = SellHistoric.First(x => x.Ticker == item.Ticker).Qnt;
                    SCusto = SellHistoric.First(x => x.Ticker == item.Ticker).Custo;
                    SPrcMedio = SellHistoric.First(x => x.Ticker == item.Ticker).PrcMedio;
                }

                listTatic.Ticker = item.Ticker;
                listTatic.DescTicker = item.DescTicker;
                listTatic.QtdeCompra = item.Qnt;
                listTatic.QtdeVenda = SQnt;
                listTatic.ValorCompra = Math.Round(item.Custo, 2);
                listTatic.ValorVenda= Math.Round(SCusto, 2);
                listTatic.ValorMedCompra = Math.Round(item.PrcMedio, 2);
                listTatic.ValorMedVenda = Math.Round(SPrcMedio, 2);
                listTatic.QtdeLiq = item.Qnt - SQnt;

                tickerListTatic.Add(listTatic);
            }

            return tickerListTatic;
        }

        // PUT: api/PurchasesHistorics/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD./// <summary>
        /// <summary>
        /// Update a specific register.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /purchasesHistoric
        ///     {
        ///       "purchasesHistoricId": 1,
        ///       "dtNegocio": "2020-01-14T00:00:00",
        ///       "tpNegocio": "C",
        ///       "tpMercado": "Mercado a Vista",
        ///       "dtPrzVcto": null,
        ///       "Ticker": "XPPR11",
        ///       "DescTicker": "FII XP PROP CI",
        ///       "quantidade": 1,
        ///       "preco": 116,
        ///       "valorTot": 116,
        ///       "fatorCotacao": null,
        ///       "idCorretora": 308
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="purchasesHistoric"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchasesHistoric(int id, PurchasesHistoric purchasesHistoric)
        {
            if (id != purchasesHistoric.PurchasesHistoricId)
            {
                return BadRequest();
            }

            _context.Entry(purchasesHistoric).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchasesHistoricExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PurchasesHistorics
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Create a specific register.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /purchasesHistoric
        ///     {
        ///       "purchasesHistoricId": 1,
        ///       "dtNegocio": "2020-01-14T00:00:00",
        ///       "tpNegocio": "C",
        ///       "tpMercado": "Mercado a Vista",
        ///       "dtPrzVcto": null,
        ///       "Ticker": "XPPR11",
        ///       "DescTicker": "FII XP PROP CI",
        ///       "quantidade": 1,
        ///       "preco": 116,
        ///       "valorTot": 116,
        ///       "fatorCotacao": null,
        ///       "idCorretora": 308
        ///     }
        ///
        /// </remarks>
        /// <param name="purchasesHistoric"></param>
        /// <returns>A newly created</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        [HttpPost]
        public async Task<ActionResult<PurchasesHistoric>> PostPurchasesHistoric(PurchasesHistoric purchasesHistoric)
        {
            _context.PurchasesHistoric.Add(purchasesHistoric);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchasesHistoric", new { id = purchasesHistoric.PurchasesHistoricId }, purchasesHistoric);
        }

        // DELETE: api/PurchasesHistorics/5
        /// <summary>
        /// Deletes a specific Register.
        /// </summary>
        /// <param name="id"></param>  
        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchasesHistoric>> DeletePurchasesHistoric(int id)
        {
            var purchasesHistoric = await _context.PurchasesHistoric.FindAsync(id);
            if (purchasesHistoric == null)
            {
                return NotFound();
            }

            _context.PurchasesHistoric.Remove(purchasesHistoric);
            await _context.SaveChangesAsync();

            return purchasesHistoric;
        }

        private bool PurchasesHistoricExists(int id)
        {
            return _context.PurchasesHistoric.Any(e => e.PurchasesHistoricId == id);
        }
    }
}
