using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement
{
    /// <summary>
    /// Encapsula las excepciones enviadas desde la capa de negocio
    /// </summary>
    public class BusinessException : Exception
    {
        #region Atributos
        /// <summary>
        /// ID del error enviado por la capa de negocios
        /// </summary>
        public int ErrorId { set; get; }
        /// <summary>
        /// Mensaje de error asociado
        /// </summary>
        public string Message { set; get; }
        #endregion

        #region Constructores
        /// <summary>
        /// Inicializa las propiedades del objeto
        /// </summary>
        /// <param name="IdError">ID del Error</param>     
        /// <param name="Mensaje">Mensaje</param>
        public BusinessException(int ErrorId, string Message)
        {
            this.ErrorId = ErrorId;
            this.Message = Message;
        }
        #endregion

    }
}
