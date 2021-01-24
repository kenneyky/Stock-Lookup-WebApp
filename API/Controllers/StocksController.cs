using System.Threading.Tasks;
using API.Models;
using IEXSharp;
using IEXSharp.Model.CoreData.Batch.Request;
using IEXSharp.Model.CoreData.Batch.Response;
using IEXSharp.Model.CoreData.StockProfiles.Response;
using IEXSharp.Model.Shared.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("stock-api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private IEXCloudClient iEXCloudClient;

        public StocksController(IConfiguration configuration)
        {
            _configuration = configuration;
            var secretToken = _configuration.GetValue<string>("SecretToken");
            var publishableToken = _configuration.GetValue<string>("PublishableToken");
            iEXCloudClient = new IEXCloudClient(publishableToken, secretToken, signRequest: false, useSandBox: false);
        }

        [HttpGet("{ticker}")]
        public async Task<ActionResult<Stock>> Get(string ticker)
        {
            var quote_response = await iEXCloudClient.StockPrices.QuoteAsync(ticker);

            //var company_response = await iEXCloudClient.StockProfiles.CompanyAsync(ticker);

            var logo_response = await iEXCloudClient.StockProfiles.LogoAsync(ticker);
            
            if (quote_response.Data == null || logo_response.Data == null)
                return BadRequest();

            var stock = DataToStock(quote_response.Data, logo_response.Data);
            return Ok(stock);
            
        }

        private Stock DataToStock(Quote quote, LogoResponse logo)
        {
                Stock stock = new Stock{
                    Name = quote.companyName,
                    Ticker = quote.symbol,
                    ImageURL = logo.url,
                    CurrentPrice = quote.latestPrice.Value,
                    YearHigh = quote.week52High.Value,
                    YearLow = quote.week52Low.Value,
                    PreviousClose = quote.previousClose.Value
                };

                return stock;
        }
        
    }
}