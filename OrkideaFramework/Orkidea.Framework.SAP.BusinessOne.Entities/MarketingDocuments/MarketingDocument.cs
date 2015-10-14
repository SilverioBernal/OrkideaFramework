using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.MarketingDocuments
{
    /// <summary>
    /// El objeto Documento, encapsula la información de documentos de marketing en SAP Business One
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class MarketingDocument
    {
        #region Fields        
        [DataMember]
        public int docEntry { set; get; }
        
        [DataMember]
        public int docNum { set; get; }
        
        [DataMember]
        public int serie { set; get; }
        
        [DataMember]
        public string cardCode { set; get; }

        [DataMember]
        public DateTime docDate { set; get; }

        [DataMember]
        public DateTime docDueDate { set; get; }

        [DataMember]
        public DateTime taxDate { set; get; }

        [DataMember]
        public string cardName { set; get; }
        
        [DataMember]
        public string numAtCard { set; get; }                
        
        [DataMember]
        public string docStatus { set; get; }
        
        [DataMember]
        public string doctype { set; get; }
        
        [DataMember]
        public string invntsttus { set; get; }
        
        [DataMember]
        public bool canceled { set; get; }
        
        [DataMember]
        public DateTime updateDate { set; get; }               
        
        [DataMember]
        public string objtype { set; get; }

        [DataMember]
        public string shipToCode { get; set; }

        [DataMember]
        public string payToCode { get; set; }

        [DataMember]
        public int? groupNum { get; set; }

        [DataMember]
        public string comments { get; set; }

        [DataMember]
        public int? slpCode { get; set; }

        [DataMember]
        public List<MarketingDocumentLine> lines { set; get; }

        [DataMember]
        public List<UserDefinedField> userDefinedFields { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarketingDocument()
        {
            this.canceled = false;
            this.cardCode = "";
            this.cardName = "";
            this.docDate = DateTime.Now;
            this.docEntry = 0;
            this.docNum = 0;
            this.docStatus = "";
            this.lines = new List<MarketingDocumentLine>();
            this.numAtCard = "";
            this.invntsttus = "";
            this.doctype = "";
            this.objtype = "";
            this.userDefinedFields = new List<UserDefinedField>();
        }
        #endregion
    }
}
