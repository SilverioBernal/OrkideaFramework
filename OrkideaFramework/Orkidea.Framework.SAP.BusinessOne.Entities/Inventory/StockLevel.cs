using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Inventory
{
    public class StockLevel
    {
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public double OnHand { get; set; }
        public double IsCommited { get; set; }
        public double OnOrder { get; set; }
        public double IsAvailable { get; set; }
    }
}
