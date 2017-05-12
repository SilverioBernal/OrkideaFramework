using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.Administration
{
    [DataContract(Namespace = "http://WSSAP")]
    public class WorkflowConfirmationDocument
    {
        [DataMember]
        public int wddCode { get; set; }
        [DataMember]
        public string remarks { get; set; }
    }
}
