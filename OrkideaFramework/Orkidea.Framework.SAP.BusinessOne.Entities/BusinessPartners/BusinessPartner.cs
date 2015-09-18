using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners
{
    /// <summary>
    /// Representa un socio de negocios en SAP Business One
    /// </summary>
    [DataContract(Namespace = "http://WSSAP")]
    public class BusinessPartner
    {
        /// <summary>
        /// Código del socio de negocios
        /// </summary>
        [DataMember]
        public string cardCode { set; get; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public CardType cardType { set; get; }
        /// <summary>
        /// Nombre del socio de negocios
        /// </summary>
        [DataMember]
        public string cardName { set; get; }
        /// <summary>
        /// Sigla del socio de negocios
        /// </summary>
        [DataMember]
        public string cardFName { set; get; }
        [DataMember]
        public string debPayAcct { get; set; }
        [DataMember]
        public int? territory { get; set; }
        [DataMember]
        public string accCritria { get; set; }
        [DataMember]
        public string blockDunn { get; set; }
        [DataMember]
        public string collecAuth { get; set; }
        [DataMember]
        public double creditLine { get; set; }
        [DataMember]
        public string deferrTax { get; set; }
        /// <summary>
        /// Cédula o NIT
        /// </summary>
        /// <summary>
        /// Nombre del socio de negocios
        /// </summary>
        [DataMember]
        public string currency { set; get; }
        /// <summary>
        /// Nombre del socio de negocios
        /// </summary>
        [DataMember]
        public string groupCode { set; get; }
        [DataMember]
        public string licTradNum { set; get; }
        [DataMember]
        public string address { get; set; }
        /// <summary>
        /// Teléfono 1
        /// </summary>
        [DataMember]
        public string phone1 { set; get; }
        /// <summary>
        /// Teléfono 2
        /// </summary>
        [DataMember]
        public string phone2 { set; get; }
        /// <summary>
        /// Teléfono Celular
        /// </summary>
        [DataMember]
        public string cellular { set; get; }
        /// <summary>
        /// Fax
        /// </summary>
        [DataMember]
        public string fax { set; get; }
        /// <summary>
        /// E_Mail
        /// </summary>
        [DataMember]
        public string e_Mail { set; get; }
        [DataMember]
        public string equ { get; set; }
        /// <summary>
        /// Nombre del socio de negocios
        /// </summary>
        [DataMember]
        public string slpCode { set; get; }
        [DataMember]
        public string freeText { set; get; }

        [DataMember]
        public List<UserDefinedField> UserDefinedFields { get; set; }

        /// <summary>
        /// Inicializa atributos
        /// </summary>
        public BusinessPartner()
        {
            this.cardCode = String.Empty;
            this.cardName = String.Empty;
            this.licTradNum = String.Empty;
            this.phone1 = String.Empty;
            this.phone2 = String.Empty;
            this.cellular = String.Empty;
            this.fax = String.Empty;
            this.e_Mail = String.Empty;
            this.UserDefinedFields = new List<UserDefinedField>();
        }
    }
}
