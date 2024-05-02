using Currency.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

namespace Currency.SQLServer.DAL
{
    public class WebDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly WebDbContextConnectionStrings _connectionStringOptions;
        public WebDbContext(IOptions<WebDbContextConnectionStrings> connectionStringOptions)
        {
            this._connectionStringOptions = connectionStringOptions.Value;
        }
        public DbSet<CurrencyEntity> Currencies { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionStringOptions.ConnectionStrings["Default"]);
        }
    }

    public class WebDbContextConnectionStrings
    {
        public Dictionary<string, string> ConnectionStrings { get; set; } = new Dictionary<string, string>();   
    }
}
