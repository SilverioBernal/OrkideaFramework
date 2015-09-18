using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement
{
    /// <summary>
    /// Excepción generada en el procesamiento contra SAP
    /// </summary>
    public class SAPException : Exception
    {
        #region Propiedades
        /// <summary>
        /// Número de error generado por SAP Business One
        /// </summary>
        public int ErrorNumber { set; get; }
        /// <summary>
        /// Descripción de la incidencia generada en SAP
        /// </summary>
        public string Description { set; get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ErrorNumber">Número de error generado por SAP Business One</param>
        /// <param name="Description">Descripción de la incidencia generada en SAP</param>
        public SAPException(int ErrorNumber, string Description)
        {
            this.ErrorNumber = ErrorNumber;
            this.Description = Description;
        }
        #endregion

    }
}
