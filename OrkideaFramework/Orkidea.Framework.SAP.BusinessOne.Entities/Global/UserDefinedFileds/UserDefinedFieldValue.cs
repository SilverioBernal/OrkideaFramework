using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds
{
    [DataContract(Namespace = "http://WSSAP")]
    public class UserDefinedFieldValue
    {
        [DataMember]
        public string fldValue { get; set; }
        [DataMember]
        public string descr { get; set; }
    }
}
