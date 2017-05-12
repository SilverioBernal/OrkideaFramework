using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    [DataContract(Namespace = "http://WSSAP")]
    public class BusinessPartnerAddress
    {
        [DataMember]
        public string cardCode { get; set; }
        [DataMember]
        public AddressType addressType { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string street { get; set; }
        [DataMember]
        public string block { get; set; }
        [DataMember]
        public string zipCode { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public string county { get; set; }
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string taxCode { get; set; }
        [DataMember]
        public string streetNo { get; set; }
        [DataMember]
        public bool defaultAddress { get; set; }
        [DataMember]
        public List<UserDefinedField> UserDefinedFields { get; set; }
    }
}
