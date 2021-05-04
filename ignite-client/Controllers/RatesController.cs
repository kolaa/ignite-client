using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using ignite_client.Model;
using Apache.Ignite.Core.Client.Cache;
using System.Net.Http;
using System.Text.Json;

namespace ignite_client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {        
        private readonly ILogger<RatesController> _logger;
        private readonly ICacheClient<DateTime, Rates> _ratesCache;

        private static readonly HttpClient Client = new();

        public RatesController(ILogger<RatesController> logger, ICacheClient<System.DateTime, Rates> ratesCache)
        {
            _logger = logger;
            _ratesCache = ratesCache;
        }

        [HttpGet]
        public async Task<Rates> Get()
        {
            var date = DateTime.Today;
           
            var cRates = await _ratesCache.TryGetAsync(date);

            if (cRates.Success)
                return cRates.Value;

            var r = await Client.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js");
            r.EnsureSuccessStatusCode();
            
            var responseBody = await r.Content.ReadAsStringAsync();

            var rates = JsonSerializer.Deserialize<Rates>(responseBody);
            
            await _ratesCache.PutIfAbsentAsync(date, rates);

            return rates;

        }
    }
}
