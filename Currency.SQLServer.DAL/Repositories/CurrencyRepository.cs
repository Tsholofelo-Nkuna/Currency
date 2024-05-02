using Currency.Model;
using Currency.SQLServer.DAL.Repositories.Base;

namespace Currency.SQLServer.DAL.Repositories
{
    public class CurrencyRepository : GenericRepository<CurrencyEntity>
    {
        public CurrencyRepository(WebDbContext context) : base(context) { }
    }
}
