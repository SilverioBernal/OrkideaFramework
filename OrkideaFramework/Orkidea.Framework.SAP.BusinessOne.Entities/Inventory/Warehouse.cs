using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Inventory
{
    /// <summary>
    /// Encapsula los datos basicos de un almacen
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class Warehouse
    {
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
    }
}
