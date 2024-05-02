using Currency.Model;
using Currency.Model.DataTansferObjects;
using Currency.SQLServer.DAL.Repositories;
using System.Net.Http.Json;

namespace Currency.Service
{
    public class CurrencyService
    {
        private readonly CurrencyRepository _currencyRepository;
        private HttpClient _httpClient;
        private string _currencyListUrl = "https://api.frankfurter.app/currencies";
        public CurrencyService(CurrencyRepository currencyRepository) {
          this._currencyRepository = currencyRepository;
          this._httpClient = new HttpClient();
        }

        public async Task<IEnumerable<CurrencyDto>> GetCurrencyList()
        {
            var currencyMap = await this._httpClient.GetFromJsonAsync<Dictionary<string, string>>(this._currencyListUrl);
            return currencyMap?.ToList()?.Select(x => new CurrencyDto { Code = x.Key, Name = x.Value}) ?? Enumerable.Empty<CurrencyDto>();
        }

        public IEnumerable<CurrencyDto> GetSavedCurrencyList()
        {
           return this._currencyRepository.Get(x => true)
                .Select(x => new CurrencyDto { Code = x.Code, Name = x.Name, Id = x.Id})
                .ToList();
        }

        public async Task<bool> Validate(CurrencyDto currencyDto)
        {
            var list =  await this.GetCurrencyList();
            return list?.Any(c => string.Equals(c.Code, currencyDto.Code, StringComparison.OrdinalIgnoreCase)) ?? false;
        }

        public async Task<CurrencyDto?> Add(CurrencyDto currency) {
            var affectedRowCount = 0;
            CurrencyEntity? currencyEntity = null;
            if( await this.Validate(currency))
            {
               currencyEntity = this._currencyRepository.Insert(new Model.CurrencyEntity() { Code = currency.Code, Name = currency.Name });
               affectedRowCount = this._currencyRepository.SaveChanges();
            }
            return affectedRowCount > 0 && currencyEntity != null ? new CurrencyDto { Code = currencyEntity.Code, Name = currencyEntity.Name, Id = currencyEntity.Id } : null;
        }

        public async Task<CurrencyDto?> Update(CurrencyDto currency)
        {
            var updated = this._currencyRepository.Get(x => x.Id == currency.Id).FirstOrDefault();
            var savedCount = 0;
            if(updated != null && await this.Validate(currency))
            {
                updated.Code = currency.Code;
                updated.Name = currency.Name;
                this._currencyRepository.Update(updated);
                savedCount = this._currencyRepository.SaveChanges();
            }

            return savedCount > 0 && updated != null ? new CurrencyDto { Code = updated.Code, Name = updated.Name, Id = updated.Id} : null;
        }

        public CurrencyDto? Delete(long id) {
            var deleted = this._currencyRepository.Get(x => x.Id ==  id).FirstOrDefault();
            var deletedCount = 0;  
            if(deleted != null)
            {
                this._currencyRepository.Delete(deleted);
                deletedCount = this._currencyRepository.SaveChanges();
            }

            return deletedCount > 0 && deleted != null ? new CurrencyDto { Code = deleted.Code, Id = deleted.Id, Name = deleted.Name } : null;
        }
    }
}
