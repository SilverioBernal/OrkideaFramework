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
        #region Atributos
        /// <summary>
        /// Numero Interno del documento
        /// </summary>
        [DataMember]
        public int DocEntry { set; get; }
        /// <summary>
        /// Número del documento de acuerdo a la serie
        /// </summary>
        [DataMember]
        public int DocNum { set; get; }
        /// <summary>
        /// Número del documento de acuerdo a la serie
        /// </summary>
        [DataMember]
        public int Serie { set; get; }
        /// <summary>
        /// Código del proveedor / cliente
        /// </summary>
        [DataMember]
        public string CardCode { set; get; }
        /// <summary>
        /// Nombre del proveedor / cliente
        /// </summary>
        [DataMember]
        public string CardName { set; get; }
        /// <summary>
        /// Número del documento referencia
        /// </summary>
        [DataMember]
        public string NumAtCard { set; get; }
        /// <summary>
        /// Fecha del documento
        /// </summary>
        [DataMember]
        public DateTime DocDate { set; get; }
        /// <summary>
        /// Fecha del documento
        /// </summary>
        [DataMember]
        public DateTime DocDueDate { set; get; }
        /// <summary>
        /// Fecha del documento
        /// </summary>
        [DataMember]
        public DateTime TaxDate { set; get; }
        /// <summary>
        /// Estado del documento (Cerrrado o Abierto)
        /// </summary>
        [DataMember]
        public string DocStatus { set; get; }
        /// <summary>
        /// Tipo de documento (Item o Servicio)
        /// </summary>
        [DataMember]
        public string Doctype { set; get; }
        /// <summary>
        /// Estado del inventario
        /// </summary>
        [DataMember]
        public string invntsttus { set; get; }
        /// <summary>
        /// El documento esta cancelado (true = si)
        /// </summary>
        [DataMember]
        public bool Canceled { set; get; }
        /// <summary>
        /// Fecha de la ultima actualizacion
        /// </summary>
        [DataMember]
        public DateTime UpdateDate { set; get; }
        /// <summary>
        /// Líneas de Documentos
        /// </summary>
        [DataMember]
        public List<MarketingDocumentLine> lines { set; get; }
        /// <summary>
        /// Tipo de objeto (13- Facturas, 22 - órdenes de compra, 17 - Órdenes de venta)
        /// </summary>
        [DataMember]
        public string Objtype { set; get; }

        [DataMember]
        public List<UserDefinedField> UserDefinedFields { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarketingDocument()
        {
            this.Canceled = false;
            this.CardCode = "";
            this.CardName = "";
            this.DocDate = DateTime.Now;
            this.DocEntry = 0;
            this.DocNum = 0;
            this.DocStatus = "";
            this.lines = new List<MarketingDocumentLine>();
            this.NumAtCard = "";
            this.invntsttus = "";
            this.Doctype = "";
            this.Objtype = "";
            this.UserDefinedFields = new List<UserDefinedField>();
        }
        #endregion
    }
}
