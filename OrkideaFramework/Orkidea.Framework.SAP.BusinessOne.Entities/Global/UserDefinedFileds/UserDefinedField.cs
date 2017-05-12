using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds
{
    [DataContract(Namespace = "http://WSSAP")]
    public class UserDefinedField
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public UdfType type { get; set; }
        [DataMember]
        public string value { get; set; }
    }
}
