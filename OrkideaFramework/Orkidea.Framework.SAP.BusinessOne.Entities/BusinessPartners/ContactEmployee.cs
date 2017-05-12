using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    [DataContract(Namespace = "http://WSSAP")]
    public class ContactEmployee
    {
        [DataMember]
        public string cardCode { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string middleName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string position { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public string telephone1 { get; set; }
        [DataMember]
        public string telephone2 { get; set; }
        [DataMember]
        public string cellolar { get; set; }
        [DataMember]
        public string e_mail { get; set; }
        [DataMember]
        public bool defaultContact { get; set; }
    }
}
