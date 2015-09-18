using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Inventory
{
    /// <summary>
    /// Encapsula la informacion referente al manejo de lotes en un documento
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class BatchNumber
    {
        /// <summary>
        /// Código que identifica el lote
        /// </summary>
        [DataMember]
        public string DistNumber { set; get; }
        /// <summary>
        /// Cantidad del lote
        /// </summary>
        [DataMember]
        public double Quantity { set; get; }
        /// <summary>
        /// Fecha de Vencimiento
        /// </summary>
        [DataMember]
        public DateTime ExpDate { set; get; }
    }
}
