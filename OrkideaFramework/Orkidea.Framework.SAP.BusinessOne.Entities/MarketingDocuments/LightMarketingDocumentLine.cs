using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.MarketingDocuments
{
    [DataContract(Namespace = "http://WSSAP")]
    public class LightMarketingDocumentLine
    {
        public int docEntry { get; set; }
        [DataMember]
        public string itemCode { get; set; }
        [DataMember]
        public string itemName { get; set; }
        [DataMember]
        public double quantity { get; set; }
        [DataMember]
        public double price { get; set; }
        [DataMember]
        public double total { get; set; }
    }
}
