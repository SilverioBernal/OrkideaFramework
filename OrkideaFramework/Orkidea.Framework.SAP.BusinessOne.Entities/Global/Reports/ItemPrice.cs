using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.Reports
{
    [DataContract(Namespace = "http://WSSAP")]
    public class ItemPrice
    {
        [DataMember]
        public string docDate { get; set; }
        [DataMember]
        public string itemCode { get; set; }
        [DataMember]
        public string itemName { get; set; }
        [DataMember]
        public double quantity { get; set; }
        [DataMember]
        public double price { get; set; }
    }
}
