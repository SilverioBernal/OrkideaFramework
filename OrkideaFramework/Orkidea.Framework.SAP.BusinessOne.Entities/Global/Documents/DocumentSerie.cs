using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.Global.Documents
{
    /// <summary>
    /// El objeto Documento, encapsula la información de documentos de marketing en SAP Business One
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class DocumentSerie
    {
        /// <summary>
        /// Numero Interno del documento
        /// </summary>
        [DataMember]
        public int serie { set; get; }
        /// <summary>
        /// Número del documento de acuerdo a la serie
        /// </summary>
        [DataMember]
        public int SeriesName { set; get; }
    }
}
