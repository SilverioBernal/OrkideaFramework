using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement
{
    /// <summary>
    /// Encapsula las excepciones generadas para que sean enviadas al servicio WCF.
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class GenericException
    {
        #region Propiedades

        /// <summary>
        /// ID del error enviado por la capa de negocios
        /// </summary>
        public int ErrorId { set; get; }
        /// <summary>
        /// Descripcion del error enviado por la capa de negocios
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// Número de error generado por SAP Business One
        /// </summary>
        public string ErrorNumber { set; get; }
        /// <summary>
        /// Proceso donde ocurrio el error
        /// </summary>
        public string Process { set; get; }

        #endregion
    }
}
