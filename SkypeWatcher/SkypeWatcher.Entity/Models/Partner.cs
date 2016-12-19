using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeWatcher.Entity.Models
{
    public class Partner
    {
        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string Name => "Ekaterina Romanova";

        /// <summary>
        /// Gets or sets credit avalible
        /// </summary>
        public bool IsBlocked => false;

        /// <summary>
        /// Summa per minute
        /// </summary>
        public double PayPalValue => 8.15;

        public double PayPalCount { get; set; }
    }
}
