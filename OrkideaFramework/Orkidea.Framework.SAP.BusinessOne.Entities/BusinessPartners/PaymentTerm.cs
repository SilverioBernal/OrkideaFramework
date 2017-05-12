using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    [DataContract(Namespace = "http://WSSAP")]
    public class PaymentTerm
    {
        [DataMember]
        public int groupNum { get; set; }
        
        [DataMember]
        public string pymntGroup { get; set; }

        [DataMember]
        public int extraDays { get; set; }

        [DataMember]
        public int extraMonth { get; set; }
    }
}
