using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.MarketingDocuments
{
    [DataContract(Namespace = "http://WSSAP")]
    public class LightMarketingDocument
    {
        #region Fields
        [DataMember]
        public int docEntry { set; get; }

        [DataMember]
        public int docNum { set; get; }        

        [DataMember]
        public string cardCode { set; get; }
        
        [DataMember]
        public string cardName { set; get; }
        
        [DataMember]
        public DateTime docDate { set; get; }

        [DataMember]
        public DateTime docDueDate { set; get; }

        [DataMember]
        public string docStatus { set; get; }     
   
        [DataMember]
        public List<LightMarketingDocumentLine> lines { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public LightMarketingDocument()
        {
            this.lines = new List<LightMarketingDocumentLine>();            
        }
        #endregion
    }
}
