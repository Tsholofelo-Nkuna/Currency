using Currency.Model.DataTansferObjects;
using Currency.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Currency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        // GET: api/<CurrencyController>
        private readonly CurrencyService _currencyService; 
        public CurrencyController(CurrencyService currencyService) { 
            this._currencyService = currencyService;
        }
        [HttpGet]
        public async Task<IEnumerable<CurrencyDto>> Get()
        {
            return  await this._currencyService.GetCurrencyList();
        }

        // GET api/<CurrencyController>/Saved
        [HttpGet("Saved")]
        public IEnumerable<CurrencyDto> Get(int id)
        {
            return this._currencyService.GetSavedCurrencyList();
        }

        // POST api/<CurrencyController>
        [HttpPost]
        public async Task<CurrencyDto?> Post([FromBody] CurrencyDto currency)
        {
            return await this._currencyService.Add(currency);
        }

        // PUT api/<CurrencyController>
        [HttpPut]
        public async Task<CurrencyDto?> Put([FromBody] CurrencyDto value)
        {
            return await this._currencyService.Update(value);
        }

        // DELETE api/<CurrencyController>/5
        [HttpDelete("{id}")]
        public CurrencyDto? Delete(int id)
        {
            return _currencyService.Delete(id);
        }
    }
}
