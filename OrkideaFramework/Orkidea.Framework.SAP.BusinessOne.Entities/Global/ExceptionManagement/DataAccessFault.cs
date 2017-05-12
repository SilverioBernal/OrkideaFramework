using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement
{
    [DataContract(Namespace = "http://WSSAP")]
    public class DataAccessFault
    {
        #region Constructores
        /// <summary>
        /// Inicializacion de atributos
        /// </summary>
        public DataAccessFault()
        {
            this.ErrorSAP = "NA";
            this.Description = "";
            this.ErrorID = "";
        }
        #endregion Constructores

        #region Propiedades
        /// <summary>
        /// Descripción del error
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// Número de error asociado en el Servicio
        /// </summary>
        [DataMember]
        public string ErrorID { get; set; }
        /// <summary>
        /// Número de Error en SAP
        /// </summary>
        [DataMember]
        public string ErrorSAP { get; set; }

        #endregion Propiedades
    }
}
