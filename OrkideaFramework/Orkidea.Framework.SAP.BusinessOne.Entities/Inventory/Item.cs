using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Inventory
{
    /// <summary>
    /// Encapsula los datos de un articulo
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class Item
    {
        #region Atributos
        /// <summary>
        /// ID Producto
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }
        /// <summary>
        /// Código de barras
        /// </summary>
        [DataMember]
        public string CodeBars { set; get; }
        /// <summary>
        /// Descripción del artículo
        /// </summary>
        [DataMember]
        public string ItemName { set; get; }
        /// <summary>
        ///Estado del artículo Valido o Inactivo para la fecha de consulta
        /// </summary>
        [DataMember]
        public string Status { set; get; }
        /// <summary>
        ///Clase Articulo 
        /// </summary>
        [DataMember]
        public string ItemType { set; get; }
        /// <summary>
        ///Grupo Artículo
        /// </summary>
        [DataMember]
        public string ItmsGrpCode { set; get; }
        /// <summary>
        /// Nombre Grupo Artículo
        /// </summary>
        [DataMember]
        public string ItmsGrpName { set; get; }
        /// <summary>
        /// Es de Inventario
        /// </summary>
        [DataMember]
        public bool InventoyItem { set; get; }
        /// <summary>
        /// Es de Venta
        /// </summary>
        [DataMember]
        public bool SalesItem { set; get; }
        /// <summary>
        /// Es de Compra
        /// </summary>
        [DataMember]
        public bool PurchaseItem { set; get; }
        /// <summary>
        /// Es artículo virtual
        /// </summary>
        [DataMember]
        public bool Panthom { set; get; }
        /// <summary>
        /// Unidad de medida de compra
        /// </summary>
        [DataMember]
        public string BuyUnitMsr { set; get; }
        /// <summary>
        /// Unidad de medida de empaque en compras
        /// </summary>
        [DataMember]
        public string PurPackMsr { set; get; }
        /// <summary>
        /// Artículos por unidad de compras
        /// </summary>
        [DataMember]
        public double NumInBuy { set; get; }
        /// <summary>
        /// Unidad de medida de ventas
        /// </summary>
        [DataMember]
        public string SalUnitMsr { set; get; }
        /// <summary>
        /// Unidad de medida de empaque en ventas
        /// </summary>
        [DataMember]
        public string SalPackMsr { set; get; }
        /// <summary>
        /// Artículos por unidad de ventas
        /// </summary>
        [DataMember]
        public double NumInSale { set; get; }
        /// <summary>
        /// Tipo de gestión del artículo
        /// </summary>
        [DataMember]
        public Gestion Gestionado { set; get; }
        /// <summary>
        /// Tipos de gestión que puede tener un artículo
        /// </summary>    
        public enum Gestion
        {
            /// <summary>
            /// Artículo gestionado por series
            /// </summary>
            Series,
            /// <summary>
            /// Artículo gestionado por lotes
            /// </summary>
            Lotes,
            /// <summary>
            /// Artículo sin método de gestión definido
            /// </summary>
            Ninguno
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Inicia los atributos de la clase
        /// </summary>
        public Item()
        {
            this.ItemCode = String.Empty;
            this.CodeBars = String.Empty;    
            this.ItemName = String.Empty;    
            this.Status = String.Empty;    
            this.ItemType = String.Empty;
            this.ItmsGrpCode = String.Empty;
            this.ItmsGrpName = String.Empty;
            this.InventoyItem = false;            
            this.SalesItem = false;
            this.PurchaseItem = false;
            this.Panthom = false;
            this.BuyUnitMsr = String.Empty;
            this.PurPackMsr = String.Empty;
            this.NumInBuy = 0;
            this.SalUnitMsr = String.Empty;
            this.SalPackMsr = String.Empty;
            this.NumInSale = 0;
            this.Gestionado = Gestion.Ninguno;            
        }
        #endregion
    }
}
