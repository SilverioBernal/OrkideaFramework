using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    /// <summary>
    /// Representa un socio de negocios en SAP Business One
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class BusinessPartnerGroup
    {
        [DataMember]
        public int groupCode { get; set; }
        [DataMember]
        public string groupName { get; set; }
        [DataMember]
        public string groupType { get; set; }
    }
}
