using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Inventory
{
    [DataContract(Namespace = "http://WSSAP")]
    public class GenericItem
    {
        #region Atributos
        /// <summary>
        /// ID Producto
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }
       
        /// <summary>
        /// Descripción del artículo
        /// </summary>
        [DataMember]
        public string ItemName { set; get; }
       #endregion
    }
}
