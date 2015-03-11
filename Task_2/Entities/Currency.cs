using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entities
{
    public class Currency
    {
        #region properties

        public string unit { get; set; }

        public decimal conversion_factor { get; set; }

        #endregion
    }
}
