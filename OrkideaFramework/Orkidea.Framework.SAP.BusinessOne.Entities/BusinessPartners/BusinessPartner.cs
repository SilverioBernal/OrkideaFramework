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
        #region Fields
        #region Basic Data
        [DataMember]
        public string cardCode { set; get; }

        [DataMember]
        public CardType cardType { set; get; }

        [DataMember]
        public string cardName { set; get; }

        [DataMember]
        public string cardFName { set; get; }

        [DataMember]
        public int groupCode { set; get; }

        [DataMember]
        public string licTradNum { set; get; }

        [DataMember]
        public string currency { set; get; }

        [DataMember]
        public double? dNotesBal { get; set; }

        [DataMember]
        public double? ordersBal { get; set; }
        #endregion

        #region General tab
        [DataMember]
        public string phone1 { set; get; }

        [DataMember]
        public string phone2 { set; get; }

        [DataMember]
        public string cellular { set; get; }

        [DataMember]
        public string fax { set; get; }

        [DataMember]
        public string e_Mail { set; get; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public int slpCode { set; get; }

        [DataMember]
        public string agentCode { set; get; }

        [DataMember]
        public string cntctPrsn { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public int? territory { get; set; }
        #endregion

        #region Contact persons tab
        [DataMember]
        public List<ContactEmployee> contactPersons { get; set; }
        #endregion

        #region Addresses tab
        [DataMember]
        public List<BusinessPartnerAddress> addresses { get; set; }
        #endregion

        #region Payment temrs tab
        [DataMember]
        public int? groupNum { get; set; }

        [DataMember]
        public double? intrstRate { get; set; }

        [DataMember]
        public int? listNum { get; set; }

        [DataMember]
        public double? discount { get; set; }

        [DataMember]
        public double? creditLine { get; set; }

        [DataMember]
        public double? debitLine { get; set; }

        [DataMember]
        public string dunTerm { get; set; }

        [DataMember]
        public double? balance { get; set; }
        #endregion

        #region Accounting tab

        #region General subtab
        [DataMember]
        public string debPayAcct { get; set; }

        [DataMember]
        public bool blockDunn { get; set; }
        #endregion

        #region Tax subtab
        [DataMember]
        public bool wtLiable { get; set; }

        [DataMember]
        public List<BusinessPartnerWithholdingTax> withholdingTaxes { get; set; }
        #endregion

        #endregion

        #region Propierties tab
        [DataMember]
        public bool qryGroup1 { get; set; }
        [DataMember]
        public bool qryGroup2 { get; set; }
        [DataMember]
        public bool qryGroup3 { get; set; }
        [DataMember]
        public bool qryGroup4 { get; set; }
        [DataMember]
        public bool qryGroup5 { get; set; }
        [DataMember]
        public bool qryGroup6 { get; set; }
        [DataMember]
        public bool qryGroup7 { get; set; }
        [DataMember]
        public bool qryGroup8 { get; set; }
        [DataMember]
        public bool qryGroup9 { get; set; }
        [DataMember]
        public bool qryGroup10 { get; set; }
        [DataMember]
        public bool qryGroup11 { get; set; }
        [DataMember]
        public bool qryGroup12 { get; set; }
        [DataMember]
        public bool qryGroup13 { get; set; }
        [DataMember]
        public bool qryGroup14 { get; set; }
        [DataMember]
        public bool qryGroup15 { get; set; }
        [DataMember]
        public bool qryGroup16 { get; set; }
        [DataMember]
        public bool qryGroup17 { get; set; }
        [DataMember]
        public bool qryGroup18 { get; set; }
        [DataMember]
        public bool qryGroup19 { get; set; }
        [DataMember]
        public bool qryGroup20 { get; set; }
        [DataMember]
        public bool qryGroup21 { get; set; }
        [DataMember]
        public bool qryGroup22 { get; set; }
        [DataMember]
        public bool qryGroup23 { get; set; }
        [DataMember]
        public bool qryGroup24 { get; set; }
        [DataMember]
        public bool qryGroup25 { get; set; }
        [DataMember]
        public bool qryGroup26 { get; set; }
        [DataMember]
        public bool qryGroup27 { get; set; }
        [DataMember]
        public bool qryGroup28 { get; set; }
        [DataMember]
        public bool qryGroup29 { get; set; }
        [DataMember]
        public bool qryGroup30 { get; set; }
        [DataMember]
        public bool qryGroup31 { get; set; }
        [DataMember]
        public bool qryGroup32 { get; set; }
        [DataMember]
        public bool qryGroup33 { get; set; }
        [DataMember]
        public bool qryGroup34 { get; set; }
        [DataMember]
        public bool qryGroup35 { get; set; }
        [DataMember]
        public bool qryGroup36 { get; set; }
        [DataMember]
        public bool qryGroup37 { get; set; }
        [DataMember]
        public bool qryGroup38 { get; set; }
        [DataMember]
        public bool qryGroup39 { get; set; }
        [DataMember]
        public bool qryGroup40 { get; set; }
        [DataMember]
        public bool qryGroup41 { get; set; }
        [DataMember]
        public bool qryGroup42 { get; set; }
        [DataMember]
        public bool qryGroup43 { get; set; }
        [DataMember]
        public bool qryGroup44 { get; set; }
        [DataMember]
        public bool qryGroup45 { get; set; }
        [DataMember]
        public bool qryGroup46 { get; set; }
        [DataMember]
        public bool qryGroup47 { get; set; }
        [DataMember]
        public bool qryGroup48 { get; set; }
        [DataMember]
        public bool qryGroup49 { get; set; }
        [DataMember]
        public bool qryGroup50 { get; set; }
        [DataMember]
        public bool qryGroup51 { get; set; }
        [DataMember]
        public bool qryGroup52 { get; set; }
        [DataMember]
        public bool qryGroup53 { get; set; }
        [DataMember]
        public bool qryGroup54 { get; set; }
        [DataMember]
        public bool qryGroup55 { get; set; }
        [DataMember]
        public bool qryGroup56 { get; set; }
        [DataMember]
        public bool qryGroup57 { get; set; }
        [DataMember]
        public bool qryGroup58 { get; set; }
        [DataMember]
        public bool qryGroup59 { get; set; }
        [DataMember]
        public bool qryGroup60 { get; set; }
        [DataMember]
        public bool qryGroup61 { get; set; }
        [DataMember]
        public bool qryGroup62 { get; set; }
        [DataMember]
        public bool qryGroup63 { get; set; }
        [DataMember]
        public bool qryGroup64 { get; set; }


        #endregion

        #region Remarks tab
        [DataMember]
        public string freeText { get; set; }
        #endregion

        #region UDF's
        [DataMember]
        public List<UserDefinedField> userDefinedFields { get; set; }
        #endregion 
        #endregion

        #region Constructor
        public BusinessPartner()
        {            
            this.contactPersons = new List<ContactEmployee>();
            this.addresses = new List<BusinessPartnerAddress>();
            this.withholdingTaxes = new List<BusinessPartnerWithholdingTax>();
            this.userDefinedFields = new List<UserDefinedField>();

        } 
        #endregion
    }
}
