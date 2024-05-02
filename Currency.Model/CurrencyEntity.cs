
using Currency.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Currency.Model
{
    [Table("Currency")]
    public class CurrencyEntity : BaseEntity
    {
        /// <summary>
        ///  Currency Name
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// Currency Code
        /// </summary>
        public string Code { get; set; } = "";
    }
}
