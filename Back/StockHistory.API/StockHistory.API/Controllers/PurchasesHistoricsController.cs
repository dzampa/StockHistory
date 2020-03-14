using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockHistory.API.Data;
using StockHistory.API.Models;

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
        ///       "codNegociacao": "XPPR11",
        ///       "especifAtivo": "FII XP PROP CI",
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
        ///       "codNegociacao": "XPPR11",
        ///       "especifAtivo": "FII XP PROP CI",
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
