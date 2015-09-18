using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.DiApiClient.SecurityData;
using Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient
{
    public class BusinessPartnerData
    {
        #region Atributos
        /// <summary>
        /// Atributos de conexión a la base de datos
        /// </summary>
        private Database dataBase;
        /// <summary>
        /// Lector
        /// </summary>
        private IDataReader reader;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessPartnerData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
        }
        #endregion

        #region Métodos
        public List<BusinessPartner> GetAll()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  CardCode,CardName FROM OCRD T0 ");

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<BusinessPartner> partners = new List<BusinessPartner>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    BusinessPartner partner = new BusinessPartner();
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    
                    partners.Add(partner);
                }
            }
            return partners;
        }

        public List<BusinessPartner> GetList(CardType cardType)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  CardCode,CardName FROM OCRD T0 ");

            switch (cardType)
            {
                case CardType.Customer:
                    oSQL.Append(string.Format("where CardType = '{0}' ", "C"));
                    break;
                case CardType.Supplier:
                    oSQL.Append(string.Format("where CardType = '{0}' ", "S"));
                    break;
                case CardType.Lead:
                    oSQL.Append(string.Format("where CardType = '{0}' ", "L"));
                    break;
                default:
                    break;
            }

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<BusinessPartner> partners = new List<BusinessPartner>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    BusinessPartner partner = new BusinessPartner();
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();                    

                    partners.Add(partner);
                }
            }
            return partners;
        }

        /// <summary>
        /// Consulta un socio de negocios en SAP Business One
        /// </summary>
        /// <param name="cardCode">Codigo de socio de negocio</param>
        /// <returns>Socio con la información</returns>
        public BusinessPartner GetSingle(string cardCode)
        {
            BusinessPartner partner = new BusinessPartner();
            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT  CardCode,CardName, LicTradNum,Phone1, Phone2,Cellular, Fax,E_Mail ");
            oSQL.Append("FROM OCRD T0 ");
            oSQL.Append("WHERE T0.LicTradNum = @CardCode ");            

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            this.dataBase.AddInParameter(myCommand, "CardCode", DbType.String, cardCode);

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    partner.licTradNum = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    partner.phone1 = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    partner.phone2 = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    partner.cellular = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    partner.fax = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    partner.e_Mail = this.reader.IsDBNull(7) ? "" : this.reader.GetValue(7).ToString();
                }
            }
            return partner;
        }

        /// <summary>
        /// Método para la creacion de socios de negocio en SAP
        /// </summary>
        /// <param name="partner"></param>
        public void Add(BusinessPartner partner)
        {
            BusinessPartners bp; //= new BusinessPartners();

            bp = (BusinessPartners)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            bp.CardCode = partner.cardCode;
            bp.CardType = BoCardTypes.cCustomer;
            bp.FederalTaxID = partner.licTradNum;
            bp.CardName = partner.cardName;

            if (!string.IsNullOrEmpty(partner.address))
                bp.Address = partner.address;

            if (!string.IsNullOrEmpty(partner.debPayAcct))
                bp.DebitorAccount = partner.debPayAcct;

            if (partner.territory != null)
                bp.Territory = (int)partner.territory;


            if (partner.accCritria != null)
            {
                bp.AccrualCriteria = BoYesNoEnum.tNO;

                if (partner.accCritria == "Yes")
                    bp.AccrualCriteria = BoYesNoEnum.tYES;
            }


            if (!string.IsNullOrEmpty(partner.blockDunn))
            {
                bp.BlockDunning = BoYesNoEnum.tNO;

                if (partner.blockDunn == "Yes")
                    bp.BlockDunning = BoYesNoEnum.tYES;
            }

            if (!string.IsNullOrEmpty(partner.cardName))
                bp.CardName = partner.cardName;

            if (!string.IsNullOrEmpty(partner.cardFName))
                bp.CardForeignName = partner.cardFName;

            //if (!string.IsNullOrEmpty(socio.CardType))
            //    bp.CardType  = SAPbobsCOM.BoCardTypes.cCustomer; ooooooooojo

            if (!string.IsNullOrEmpty(partner.cellular))
                bp.Cellular = partner.cellular;

            if (!string.IsNullOrEmpty(partner.collecAuth))
            {
                bp.CollectionAuthorization = BoYesNoEnum.tNO;

                if (partner.collecAuth == "Yes")
                    bp.CollectionAuthorization = BoYesNoEnum.tYES;
            }

            if (partner.creditLine != 0)
                bp.CreditLimit = partner.creditLine;

            if (!string.IsNullOrEmpty(partner.currency))
                bp.Currency = partner.currency;


            if (!string.IsNullOrEmpty(partner.deferrTax))
            {
                bp.DeferredTax = BoYesNoEnum.tNO;

                if (partner.deferrTax == "Yes")
                    bp.DeferredTax = BoYesNoEnum.tYES;
            }

            if (!string.IsNullOrEmpty(partner.e_Mail))
                bp.EmailAddress = partner.e_Mail;

            if (!string.IsNullOrEmpty(partner.equ))
            {
                bp.Equalization = BoYesNoEnum.tNO;

                if (partner.equ == "Yes")
                    bp.Equalization = BoYesNoEnum.tYES;
            }

            if (!string.IsNullOrEmpty(partner.fax))
                bp.Fax = partner.fax;

            if (!string.IsNullOrEmpty(partner.licTradNum))
                bp.FederalTaxID = partner.licTradNum;


            if (!string.IsNullOrEmpty(partner.phone1))
                bp.Phone1 = partner.phone1;


            if (!string.IsNullOrEmpty(partner.phone2))
                bp.Phone2 = partner.phone2;


            if (bp.Add() != 0)
                throw new SAPException(SAPConnection.conn.company.GetLastErrorCode(), SAPConnection.conn.company.GetLastErrorDescription());

        }
        #endregion
    }
}
