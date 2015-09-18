using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Inventory
{
    /// <summary>
    /// Encapsula la informacion referente al manejo de seriales en un documento
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class SerialNumber
    {
        /// <summary>
        /// Número de Serie del fabricante
        /// </summary>
        [DataMember]
        public string MnfSerial { set; get; }
        /// <summary>
        /// Número de Serie
        /// </summary>
        [DataMember]
        public string DisNumber { set; get; }
        /// <summary>
        /// Código que identifica el lote
        /// </summary>
        [DataMember]
        public string LotNumber { set; get; }
        /// <summary>
        /// Fecha de expiración para el ítem
        /// </summary>
        [DataMember]
        public DateTime ExpDate { set; get; }
        /// <summary>
        /// Fecha de fabricación del fabricante para el lote
        /// </summary>
        [DataMember]
        public DateTime MnfDate { set; get; }
        /// <summary>
        /// Código del artículo en SAP Business One
        /// </summary>
        [DataMember]
        public string ItemCode { set; get; }
        /// <summary>
        /// Estado del serial, disponible o no disponible
        /// </summary>
        [DataMember]
        public string Status { set; get; }
    }
}
