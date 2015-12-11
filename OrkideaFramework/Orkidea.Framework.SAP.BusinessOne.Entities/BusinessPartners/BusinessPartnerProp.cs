using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    [DataContract(Namespace = "http://WSSAP")]
    public class BusinessPartnerProp
    {
        [DataMember]
        public int groupCode { get; set; }
        [DataMember]
        public string groupName { get; set; }
    }
}
