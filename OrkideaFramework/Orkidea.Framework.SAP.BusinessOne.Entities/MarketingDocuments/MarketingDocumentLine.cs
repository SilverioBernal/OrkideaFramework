using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Orkidea.Framework.SAP.BusinessOne.Entities.Inventory;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.MarketingDocuments
{
    /// <summary>
    /// Encapsula el contenido de un documento de marketing de SAP Business One
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class MarketingDocumentLine
        {
        #region Fields        
        [DataMember]
        public int docEntry { set; get; }
        
        [DataMember]
        public string itemCode { set; get; }

        [DataMember]
        public string dscription { set; get; }
                
        [DataMember]
        public string whsCode { set; get; }
        
        [DataMember]
        public string whsName { set; get; }

        [DataMember]
        public double price { set; get; }

        [DataMember]
        public string taxCode { set; get; }

        [DataMember]
        public string ocrCode { set; get; }

        [DataMember]
        public double quantity { set; get; }        
        
        [DataMember]
        public string lineStatus { set; get; }
        
        [DataMember]
        public int baseEntry { set; get; }
        
        [DataMember]
        public int baseLine { set; get; }
        
        [DataMember]
        public int baseType { set; get; }
        
        [DataMember]
        public double openCreQty { set; get; }
        
        [DataMember]
        public double openQty { set; get; }
        
        [DataMember]
        public string unitMsr { set; get; }
        
        [DataMember]
        public double numPerMsr { set; get; }
        
        [DataMember]
        public int targetType { set; get; }
        
        [DataMember]
        public int trgetEntry { set; get; }
        
        [DataMember]
        public int lineNum { set; get; }

        [DataMember]
        public List<UserDefinedField> userDefinedFields { get; set; }
        
        [DataMember]
        public List<BatchNumber> batchNumbers { set; get; }
        
        [DataMember]
        public List<SerialNumber> serialNumbers { set; get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarketingDocumentLine()
        {            
            this.batchNumbers = new List<BatchNumber>();
            this.serialNumbers = new List<SerialNumber>();
            this.userDefinedFields = new List<UserDefinedField>();
        }
        #endregion
    }
}
