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
        #region Atributos
        /// <summary>
        /// Número Interno del documento
        /// </summary>
        [DataMember]
        public int DocEntry { set; get; }
        /// <summary>
        /// Código del ítem
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }
        /// <summary>
        /// Cantidad
        /// </summary>
        [DataMember]
        public double Quantity { set; get; }
        /// <summary>
        /// Código del almacén
        /// </summary>
        [DataMember]
        public string WhsCode { set; get; }
        /// <summary>
        /// Nombre del almacén
        /// </summary>
        [DataMember]
        public string WhsName { set; get; }
        /// <summary>
        /// Descripción del artículo
        /// </summary>
        [DataMember]
        public string Dscription { set; get; }
        /// <summary>
        /// Estado de la línea (Abierto (O), Cerrado (C))
        /// </summary>
        [DataMember]
        public string LineStatus { set; get; }
        /// <summary>
        /// Número Interno referenciado para la creación con documento base
        /// </summary>
        [DataMember]
        public int BaseEntry { set; get; }
        /// <summary>
        /// Número de la línea del documento base
        /// </summary>
        [DataMember]
        public int BaseLine { set; get; }
        /// <summary>
        /// Tipo de documento referenciado (202 Órdenes de producción, 17 Pedido en ventas, 22 Orden de compra, 18 Factura de proveedores)
        /// </summary>
        [DataMember]
        public int BaseType { set; get; }
        /// <summary>
        /// Cantidad pendiente por recibir
        /// </summary>
        [DataMember]
        public double OpenCreQty { set; get; }
        /// <summary>
        /// Cantidad pendiente por recibir
        /// </summary>
        [DataMember]
        public double OpenQty { set; get; }
        /// <summary>
        /// Unidad de medida de ingreso
        /// </summary>
        [DataMember]
        public string unitMsr { set; get; }
        /// <summary>
        /// Cantidad de unidades por unidad de medida
        /// </summary>
        [DataMember]
        public double NumPerMsr { set; get; }
        /// <summary>
        /// Señala de que tipo de documento se copio la linea
        /// </summary>
        [DataMember]
        public int TargetType { set; get; }
        /// <summary>
        /// Refiere  al DocEntry del campo destino en la tabla base
        /// </summary>
        [DataMember]
        public int TrgetEntry { set; get; }
        /// <summary>
        /// Numero de linea
        /// </summary>
        [DataMember]
        public int LineNum { set; get; }

        [DataMember]
        public List<UserDefinedField> UserDefinedFields { get; set; }
        /// <summary>
        /// Lista de Lotes para el artículo en documento de Marketing
        /// </summary>
        [DataMember]
        public List<BatchNumber> BatchNumbers { set; get; }
        /// <summary>
        /// Lista de series para el artículo en documento de Marketing
        /// </summary>
        [DataMember]
        public List<SerialNumber> SerialNumbers { set; get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarketingDocumentLine()
        {
            this.DocEntry = 0;
            this.ItemCode = String.Empty;
            this.Quantity = 0;
            this.WhsCode = String.Empty;
            this.WhsName = String.Empty;
            this.Dscription = String.Empty;
            this.LineStatus = String.Empty;
            this.BaseEntry = 0;
            this.BaseLine = 0;
            this.BaseType = 0;
            this.OpenCreQty = 0;
            this.OpenQty = 0;
            this.unitMsr = String.Empty;
            this.NumPerMsr = 0;
            this.TargetType = 0;
            this.TrgetEntry = 0;
            this.LineNum = 0;
            this.BatchNumbers = new List<BatchNumber>();
            this.SerialNumbers = new List<SerialNumber>();
            this.UserDefinedFields = new List<UserDefinedField>();
        }
        #endregion
    }
}
