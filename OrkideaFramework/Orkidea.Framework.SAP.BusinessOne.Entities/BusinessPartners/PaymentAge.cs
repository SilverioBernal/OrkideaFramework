using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    [DataContract(Namespace = "http://WSSAP")]
    public class PaymentAge
    {
        [DataMember]
        public string cardCode { get; set; }
        [DataMember]
        public string cardName { get; set; }
        [DataMember]
        public string docNum { get; set; }
        [DataMember]
        public string docDate { get; set; }
        [DataMember]
        public string docDueDate { get; set; }
        [DataMember]
        public double pendingToPay { get; set; }
        [DataMember]
        public double up30 { get; set; }
        [DataMember]
        public double up60 { get; set; }
        [DataMember]
        public double up90 { get; set; }
        [DataMember]
        public double up120 { get; set; }
        [DataMember]
        public double up9999 { get; set; }

    }
}
