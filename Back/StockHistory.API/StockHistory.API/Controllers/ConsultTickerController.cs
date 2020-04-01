using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockHistory.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StockHistory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultTickerController : ControllerBase
    {

        static HttpClient client = new HttpClient();
        private string TickerListAddress = "https://symbol-search.tradingview.com/symbol_search/?";
        private string TickerDetailAddress = "https://scanner.tradingview.com/brazil/scan";

        // POST: api/PurchasesHistorics/ticker        
        /// <summary>
        /// Get a specific ticker.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /purchasesHistoric
        ///     {
        ///       "text" : "PETR",
        ///       "exchange" : null,
        ///       "type" : null,
        ///       "hl" : "true",
        ///       "lang" : "pt",
        ///       "domain" : "production"
        ///     }
        ///
        /// </remarks>
        /// <param name="tickerSearch"></param>
        /// <returns>A list of Ticker</returns>
        /// <response code="201">Returns a list of Ticker</response>
        /// <response code="400">If the item is null</response>
        /// <response code="401">Unauthorized</response>  
        [Authorize("Bearer")]
        [Route("/TickerList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<List<TickerListDetail>> PostTickerListDatail(TickerSearch tickerSearch)
        {
            var tURL = $"{TickerListAddress}text={tickerSearch.Text}&exchange={tickerSearch.Exchange}&type={tickerSearch.Type}" +
                $"&hl={tickerSearch.HL}&lang={tickerSearch.Lang}&domain={tickerSearch.Domain}";

            List<TickerListDetail> tickerLists = new List<TickerListDetail>();

            HttpResponseMessage response = await client.GetAsync(tURL);
            if (response.IsSuccessStatusCode)
            {
                tickerLists = await response.Content.ReadAsAsync<List<TickerListDetail>>();
            }
            return tickerLists;
        }

        // POST: api/PurchasesHistorics/ticker        
        /// <summary>
        /// Get a specific ticker.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /purchasesHistoric
        ///     {
        ///     "symbols": {
        ///         "tickers": [
        ///           "BMFBOVESPA:IVVB11"
        ///         ],
        ///         "query": {
        ///           "types": []
        ///         }
        ///     },
        ///       "columns": [
        ///         "Recommend.All",
        ///         "RSI",
        ///         "RSI[1]",
        ///         "Stoch.K",
        ///         "Stoch.D",
        ///         "Stoch.K[1]",
        ///         "Stoch.D[1]",
        ///         "CCI20",
        ///         "CCI20[1]",
        ///         "ADX",
        ///         "ADX+DI",
        ///         "ADX-DI",
        ///         "ADX+DI[1]",
        ///         "ADX-DI[1]",
        ///         "AO",
        ///         "AO[1]",
        ///         "Mom",
        ///         "Mom[1]",
        ///         "MACD.macd",
        ///         "MACD.signal",
        ///         "Rec.Stoch.RSI",
        ///         "Stoch.RSI.K",
        ///         "Rec.WR",
        ///         "W.R",
        ///         "Rec.BBPower",
        ///         "BBPower",
        ///         "Rec.UO",
        ///         "UO",
        ///         "EMA5",
        ///       	"open",
        ///         "close",
        ///         "volume",
        ///         "SMA5",
        ///         "EMA10",
        ///         "SMA10",
        ///         "EMA20",
        ///         "SMA20",
        ///         "EMA30",
        ///         "SMA30",
        ///         "EMA50",
        ///         "SMA50",
        ///         "EMA100",
        ///         "SMA100",
        ///         "EMA200",
        ///         "SMA200",
        ///         "Rec.Ichimoku",
        ///         "Ichimoku.BLine",
        ///         "Rec.VWMA",
        ///         "VWMA",
        ///         "Rec.HullMA9",
        ///         "HullMA9"
        ///       ]
        ///     }
        ///
        /// </remarks>
        /// <param name="tickerDetail"></param>
        /// <returns>A list of Ticker</returns>
        /// <response code="201">Returns a list of Ticker</response>
        /// <response code="400">If the item is null</response> 
        /// <response code="401">Unauthorized</response> 
        [Authorize("Bearer")]
        [Route("/TickerDetail")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<TickerSearchDetail> PostSearchDetail(TickerDetail tickerDetail)
        {
            var tURL = $"{TickerDetailAddress}";

            TickerSearchDetail tickerSearchDetails = new TickerSearchDetail();

            HttpResponseMessage response = await client.PostAsJsonAsync(tURL,tickerDetail);
            if (response.IsSuccessStatusCode)
            {
                tickerSearchDetails = await response.Content.ReadAsAsync<TickerSearchDetail>();
            }
            return tickerSearchDetails;
        }


    }
}
