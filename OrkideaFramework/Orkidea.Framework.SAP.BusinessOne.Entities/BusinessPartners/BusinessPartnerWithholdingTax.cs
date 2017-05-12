using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    [DataContract(Namespace = "http://WSSAP")]
    public class BusinessPartnerWithholdingTax
    {
        [DataMember]
        public string cardCode { get; set; }
        [DataMember]
        public string wtCode { get; set; }        
    }
}
