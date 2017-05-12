using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.Administration
{
    [DataContract(Namespace = "http://WSSAP")]
    public class AuthorizationStatus
    {
        [DataMember]
        public int wddCode { get; set; }
        [DataMember]
        public int wtmCode { get; set; }
        [DataMember]
        public int ownerId { get; set; }
        [DataMember]
        public string ownerName { get; set; }
        [DataMember]
        public int docEntry { get; set; }
        [DataMember]
        public int docNum { get; set; }
        [DataMember]
        public int objType { get; set; }
        [DataMember]
        public DateTime docDate { get; set; }
        [DataMember]
        public int currStep { get; set; }
        [DataMember]
        public string currStepName { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string remarks { get; set; }
        [DataMember]
        public int userSing { get; set; }
        [DataMember]
        public DateTime createDate { get; set; }
        [DataMember]
        public int createTime { get; set; }
        [DataMember]
        public string isDraft { get; set; }
        [DataMember]
        public int maxReqr { get; set; }
        [DataMember]
        public int maxRejReqr { get; set; }
    }
}
