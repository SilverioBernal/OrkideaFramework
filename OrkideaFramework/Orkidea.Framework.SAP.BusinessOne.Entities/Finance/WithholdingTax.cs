using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Finance
{
    [DataContract(Namespace = "http://WSSAP")]
    public class WithholdingTax
    {
        [DataMember]
        public string wtCode { get; set; }
        [DataMember]
        public string wtName { get; set; }
    }
}
