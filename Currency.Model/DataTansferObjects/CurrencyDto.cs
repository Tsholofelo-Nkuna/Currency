using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency.Model.DataTansferObjects
{
    public class CurrencyDto
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
