using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.Misc
{
    [DataContract(Namespace = "http://WSSAP")]
    public class AppConnData
    {
        [DataMember]
        public string adoConnString { get; set; }
        [DataMember]
        public string dataBaseName { get; set; }
        [DataMember]
        public string sapUser { get; set; }
        [DataMember]
        public string sapUserPassword { get; set; }
        [DataMember]
        public string wsAppKey { get; set; }
        [DataMember]
        public string wsSecret { get; set; }
    }
}
