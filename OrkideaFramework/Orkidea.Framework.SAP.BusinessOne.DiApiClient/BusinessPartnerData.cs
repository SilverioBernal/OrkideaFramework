using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.DiApiClient.SecurityData;
using Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners;
using Orkidea.Framework.SAP.BusinessOne.Entities.Finance;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.Reports;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;
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
        #region Fields
        /// <summary>
        /// Atributos de conexión a la base de datos
        /// </summary>
        private Database dataBase;
        /// <summary>
        /// Lector
        /// </summary>
        private IDataReader reader;

        private string connStr;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BusinessPartnerData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
            this.connStr = "SAP";
        }

        public BusinessPartnerData(string connStringName)
        {
            this.dataBase = DatabaseFactory.CreateDatabase(connStringName);
            this.connStr = connStringName;
        }
        #endregion

        #region Methods
        public List<GenericBusinessPartner> GetAll()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  CardCode,CardName FROM OCRD T0 where WtLiable= 'Y'");

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<GenericBusinessPartner> partners = new List<GenericBusinessPartner>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    GenericBusinessPartner partner = new GenericBusinessPartner();
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    partners.Add(partner);
                }
            }
            return partners;
        }

        public List<GenericBusinessPartner> GetList(CardType cardType)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  CardCode, CardName, listNum FROM OCRD T0 ");

            switch (cardType)
            {
                case CardType.Customer:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y' ", "C"));
                    break;
                case CardType.Supplier:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y' ", "S"));
                    break;
                case CardType.Lead:
                    oSQL.Append(string.Format("where CardType = '{0}' ", "L"));
                    break;
                default:
                    break;
            }

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<GenericBusinessPartner> partners = new List<GenericBusinessPartner>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    GenericBusinessPartner partner = new GenericBusinessPartner();
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    partner.listNum = this.reader.IsDBNull(1) ? 1 : int.Parse(this.reader.GetValue(2).ToString());
                    partners.Add(partner);
                }
            }
            return partners;
        }

        public List<GenericBusinessPartner> GetList(CardType cardType, string[] cardCodes)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  CardCode, CardName, listNum FROM OCRD T0 ");

            StringBuilder cardCodeTarget = new StringBuilder();

            for (int i = 0; i < cardCodes.Length; i++)
            {
                if (i.Equals(cardCodes.Length - 1))
                    cardCodeTarget.Append(string.Format("'{0}'", cardCodes[i]));
                else
                    cardCodeTarget.Append(string.Format("'{0}', ", cardCodes[i]));
            }

            switch (cardType)
            {
                case CardType.Customer:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y'  and cardcode in ({1}) ", "C", cardCodeTarget.ToString()));
                    break;
                case CardType.Supplier:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y'  and cardcode in ({1}) ", "S", cardCodeTarget.ToString()));
                    break;
                case CardType.Lead:
                    oSQL.Append(string.Format("where CardType = '{0}'  and cardcode in ({1}) ", "L", cardCodeTarget.ToString()));
                    break;
                default:
                    break;
            }

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<GenericBusinessPartner> partners = new List<GenericBusinessPartner>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    GenericBusinessPartner partner = new GenericBusinessPartner();
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    partner.listNum = this.reader.IsDBNull(1) ? 1 : int.Parse(this.reader.GetValue(2).ToString());
                    partners.Add(partner);
                }
            }
            return partners;
        }

        public List<GenericBusinessPartner> GetList(CardType cardType, string slpCode)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  CardCode, CardName, listNum FROM OCRD T0 ");

            switch (cardType)
            {
                case CardType.Customer:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y' and slpCode = {1} ", "C", slpCode));
                    break;
                case CardType.Supplier:
                    oSQL.Append(string.Format("where CardType = '{0}' and  WtLiable= 'Y' and slpCode = {1} ", "S", slpCode));
                    break;
                case CardType.Lead:
                    oSQL.Append(string.Format("where CardType = '{0}'  and slpCode = {1} ", "L", slpCode));
                    break;
                default:
                    break;
            }

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<GenericBusinessPartner> partners = new List<GenericBusinessPartner>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    GenericBusinessPartner partner = new GenericBusinessPartner();
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    partner.listNum = this.reader.IsDBNull(1) ? 1 : int.Parse(this.reader.GetValue(2).ToString());
                    partners.Add(partner);
                }
            }
            return partners;
        }

        public List<ContactEmployee> GetContactList(string cardCode)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT name, firstName, middleName, lastName, title, position, address, tel1, tel2, cellolar, e_maill ");
            oSQL.Append("FROM OCPR T0 ");
            oSQL.Append(string.Format("WHERE cardCode = '{0}'", cardCode));

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<ContactEmployee> contacts = new List<ContactEmployee>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    ContactEmployee contact = new ContactEmployee();
                    contact.name = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    contact.firstName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    contact.middleName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    contact.lastName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    contact.title = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                    contact.position = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    contact.address = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    contact.telephone1 = this.reader.IsDBNull(7) ? "" : this.reader.GetValue(7).ToString();
                    contact.telephone2 = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                    contact.cellolar = this.reader.IsDBNull(9) ? "" : this.reader.GetValue(9).ToString();
                    contact.e_mail = this.reader.IsDBNull(10) ? "" : this.reader.GetValue(10).ToString();

                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        public List<BusinessPartnerAddress> GetAddressList(string cardCode, AddressType addressType)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append(string.Format("SELECT Address, Street FROM CRD1 T0 WHERE cardCode = '{0}' ", cardCode));

            switch (addressType)
            {
                case AddressType.ShipTo:
                    oSQL.Append(string.Format("AND  adresType = '{0}' ", "S"));
                    break;
                case AddressType.BillTo:
                    oSQL.Append(string.Format("AND  adresType = '{0}' ", "B"));
                    break;
                default:
                    break;
            }

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<BusinessPartnerAddress> contacts = new List<BusinessPartnerAddress>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    BusinessPartnerAddress contact = new BusinessPartnerAddress();
                    //contact.addressType = this.reader.GetValue(0).ToString() == "S" ? AddressType.ShipTo : AddressType.BillTo;
                    contact.address = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    contact.street = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    contacts.Add(contact);
                }
            }
            return contacts;
        }

        public List<BusinessPartnerDunninTerm> GetDunninTermList()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT termCode, termName FROM ODUT T0 ");

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<BusinessPartnerDunninTerm> dunningTerms = new List<BusinessPartnerDunninTerm>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    BusinessPartnerDunninTerm dunningTerm = new BusinessPartnerDunninTerm();
                    dunningTerm.termCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    dunningTerm.termName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    dunningTerms.Add(dunningTerm);
                }
            }
            return dunningTerms;
        }

        public List<BusinessPartnerWithholdingTax> GetBusinessPartnerWithholdingTaxList(string cardCode)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT wtCode ");
            oSQL.Append("FROM CRD4 T0 ");
            oSQL.Append(string.Format("WHERE cardCode = '{0}'", cardCode));

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<BusinessPartnerWithholdingTax> withholdingTaxes = new List<BusinessPartnerWithholdingTax>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    BusinessPartnerWithholdingTax withholdingTax = new BusinessPartnerWithholdingTax();
                    withholdingTax.wtCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    withholdingTax.cardCode = cardCode;

                    withholdingTaxes.Add(withholdingTax);
                }
            }
            return withholdingTaxes;
        }

        public List<PaymentAge> GetPaymentAgeList(string cardCode)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");

            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("select case when transType = -3 then 'CB' when transType = 13 then 'FA' when transType = 14 then 'RC' when transType = 15 then 'NE' when transType = 16 then 'DV' when transType = 18 then 'TT' ");
            oSQL.Append("when transType = 19 then 'TP' when transType = 20 then 'EP' when transType = 21 then 'DM' when transType = 24 then 'PR' when transType = 25 then 'DP' when transType = 30 then 'AS' ");
            oSQL.Append("when transType = 46 then 'PP' when transType = 59 then 'EM' when transType = 60 then 'SM' when transType = 67 then 'IM' when transType = 69 then 'DI' when transType = 162 then 'RI' ");
            oSQL.Append("when transType = 202 then 'OF' when transType = 204 then 'AN' when transType = 321 then 'ID' end seriesName, ");
            oSQL.Append(string.Format("docNum, '{0}' cardCode, (select cardname from ocrd where cardcode = '{1}') cardName, ", cardCode, cardCode));
            oSQL.Append("refDate docDate, dueDate docDueDate, -balDueCred pendingToPay, DATEDIFF(dd,dueDate,getdate()) pendingTime, numAtCard, ");
            oSQL.Append("CASE when DATEDIFF(dd,dueDate,getdate()) <= 15 then -balDueCred else 0 End c15, ");
            oSQL.Append("CASE when DATEDIFF(dd,dueDate,getdate()) between 16 and 30 then -balDueCred else 0 End c30, ");
            oSQL.Append("CASE when DATEDIFF(dd,dueDate,getdate()) between 31 and 60 then -balDueCred else 0 End c60, ");
            oSQL.Append("CASE when DATEDIFF(dd,dueDate,getdate()) between 61 and 90 then -balDueCred else 0 End c90, ");
            oSQL.Append("CASE when DATEDIFF(dd,dueDate,getdate()) between 91 and 120 then -balDueCred else 0 End c120, ");
            oSQL.Append("CASE when DATEDIFF(dd,dueDate,getdate()) > 120 then -balDueCred else 0 End c121 ");
            oSQL.Append("from (SELECT MAX(T0.[TransType])transType, MAX(T0.[BaseRef]) docNum, MAX(T0.[RefDate]) refDate, MAX(T0.[DueDate]) dueDate, ");
            oSQL.Append("MAX(T0.[BalDueCred]) + SUM(T1.[ReconSum]) BalDueCred, MAX(T0.[LineMemo]) LineMemo, MAX(T5.[NumAtCard]) numAtCard, MAX(T4.[DunTerm]) DunTerm ");
            oSQL.Append("FROM  [dbo].[JDT1] T0  ");
            oSQL.Append("INNER  JOIN [dbo].[ITR1] T1  ON  T1.[TransId] = T0.[TransId]  AND  T1.[TransRowId] = T0.[Line_ID]   ");
            oSQL.Append("INNER  JOIN [dbo].[OITR] T2  ON  T2.[ReconNum] = T1.[ReconNum]   ");
            oSQL.Append("INNER  JOIN [dbo].[OJDT] T3  ON  T3.[TransId] = T0.[TransId]   ");
            oSQL.Append("INNER  JOIN [dbo].[OCRD] T4  ON  T4.[CardCode] = T0.[ShortName]    ");
            oSQL.Append("LEFT OUTER  JOIN [dbo].[B1_JournalTransSourceView] T5  ON  T5.[ObjType] = T0.[TransType]  AND  T5.[DocEntry] = T0.[CreatedBy]  ");
            oSQL.Append("AND  (T5.[TransType] <> 'I'  OR  (T5.[TransType] = 'I'  AND  T5.[InstlmntID] = T0.[SourceLine] ))  ");
            oSQL.Append(string.Format("WHERE T0.[RefDate] <= ('{0}')   AND  T4.[CardType] = 'C'  AND  T4.[Balance] <> 0  AND  T4.[CardCode] = ('{1}') AND  T2.[ReconDate] > ('{2}')  AND  T1.[IsCredit] = 'C'   ", today, cardCode, today));
            oSQL.Append("GROUP BY T0.[TransId], T0.[Line_ID], T0.[BPLName] HAVING MAX(T0.[BalFcCred]) <>- SUM(T1.[ReconSumFC])  OR  MAX(T0.[BalDueCred]) <>- SUM(T1.[ReconSum])   ");
            oSQL.Append("UNION ALL ");
            oSQL.Append("SELECT MAX(T0.[TransType]), MAX(T0.[BaseRef]), MAX(T0.[RefDate]), MAX(T0.[DueDate]), - MAX(T0.[BalDueDeb]) - SUM(T1.[ReconSum]), MAX(T0.[LineMemo]), MAX(T5.[NumAtCard]), MAX(T4.[DunTerm]) ");
            oSQL.Append("FROM  [dbo].[JDT1] T0  ");
            oSQL.Append("INNER  JOIN [dbo].[ITR1] T1  ON  T1.[TransId] = T0.[TransId]  AND  T1.[TransRowId] = T0.[Line_ID]   ");
            oSQL.Append("INNER  JOIN [dbo].[OITR] T2  ON  T2.[ReconNum] = T1.[ReconNum]   ");
            oSQL.Append("INNER  JOIN [dbo].[OJDT] T3  ON  T3.[TransId] = T0.[TransId]   ");
            oSQL.Append("INNER  JOIN [dbo].[OCRD] T4  ON  T4.[CardCode] = T0.[ShortName]    ");
            oSQL.Append("LEFT OUTER  JOIN [dbo].[B1_JournalTransSourceView] T5  ON  T5.[ObjType] = T0.[TransType]  AND  T5.[DocEntry] = T0.[CreatedBy]  ");
            oSQL.Append("AND  (T5.[TransType] <> 'I'  OR  (T5.[TransType] = 'I'  AND  T5.[InstlmntID] = T0.[SourceLine] ))  ");
            oSQL.Append(string.Format("WHERE T0.[RefDate] <= ('{0}') AND  T4.[CardType] = 'C'  AND  T4.[Balance] <> 0  AND  T4.[CardCode] = ('{1}')  AND  T2.[ReconDate] > ('{2}')  AND  T1.[IsCredit] = 'D'   ", today, cardCode, today));
            oSQL.Append("GROUP BY T0.[TransId], T0.[Line_ID], T0.[BPLName] HAVING MAX(T0.[BalFcDeb]) <>- SUM(T1.[ReconSumFC])  OR  MAX(T0.[BalDueDeb]) <>- SUM(T1.[ReconSum])   ");
            oSQL.Append("UNION ALL ");
            oSQL.Append("SELECT MAX(T0.[TransType]), MAX(T0.[BaseRef]), MAX(T0.[RefDate]), MAX(T0.[DueDate]), MAX(T0.[BalDueCred]) - MAX(T0.[BalDueDeb]), MAX(T0.[LineMemo]), MAX(T3.[NumAtCard]), MAX(T2.[DunTerm]) ");
            oSQL.Append("FROM  [dbo].[JDT1] T0  ");
            oSQL.Append("INNER  JOIN [dbo].[OJDT] T1  ON  T1.[TransId] = T0.[TransId]   ");
            oSQL.Append("INNER  JOIN [dbo].[OCRD] T2  ON  T2.[CardCode] = T0.[ShortName]    ");
            oSQL.Append("LEFT OUTER  JOIN [dbo].[B1_JournalTransSourceView] T3  ON  T3.[ObjType] = T0.[TransType]  AND  T3.[DocEntry] = T0.[CreatedBy]  ");
            oSQL.Append("AND  (T3.[TransType] <> 'I'  OR  (T3.[TransType] = 'I'  AND  T3.[InstlmntID] = T0.[SourceLine] ))  ");
            oSQL.Append(string.Format("WHERE T0.[RefDate] <= ('{0}')  AND  T2.[CardType] = 'C'  AND  T2.[Balance] <> 0  AND  T2.[CardCode] = ('{1}')  ", today, cardCode));
            oSQL.Append("AND  (T0.[BalDueCred] <> T0.[BalDueDeb]  OR  T0.[BalFcCred] <> T0.[BalFcDeb] ) ");
            oSQL.Append("AND NOT EXISTS (SELECT U0.[TransId], U0.[TransRowId] FROM  [dbo].[ITR1] U0  INNER  JOIN [dbo].[OITR] U1  ON  U1.[ReconNum] = U0.[ReconNum] ");
            oSQL.Append(string.Format("WHERE T0.[TransId] = U0.[TransId]  AND  T0.[Line_ID] = U0.[TransRowId]  AND  U1.[ReconDate] > ('{0}')   GROUP BY U0.[TransId], U0.[TransRowId])   ", today));
            oSQL.Append("GROUP BY T0.[TransId], T0.[Line_ID], T0.[BPLName]) a ");

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<PaymentAge> documents = new List<PaymentAge>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    PaymentAge document = new PaymentAge();
                    document.seriesName = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    document.docNum = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    document.cardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    document.cardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    document.docDate = DateTime.Parse(this.reader.GetValue(4).ToString()).ToString("yyyy-MM-dd");
                    document.docDueDate = DateTime.Parse(this.reader.GetValue(5).ToString()).ToString("yyyy-MM-dd");
                    document.pendingToPay = this.reader.IsDBNull(6) ? 0 : double.Parse(this.reader.GetValue(6).ToString());
                    document.pendingTime = this.reader.IsDBNull(7) ? 0 : double.Parse(this.reader.GetValue(7).ToString());
                    document.numAtCard = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                    document.up15 = this.reader.IsDBNull(9) ? 0 : double.Parse(this.reader.GetValue(9).ToString());
                    document.up30 = this.reader.IsDBNull(10) ? 0 : double.Parse(this.reader.GetValue(10).ToString());
                    document.up60 = this.reader.IsDBNull(11) ? 0 : double.Parse(this.reader.GetValue(11).ToString());
                    document.up90 = this.reader.IsDBNull(12) ? 0 : double.Parse(this.reader.GetValue(12).ToString());
                    document.up120 = this.reader.IsDBNull(13) ? 0 : double.Parse(this.reader.GetValue(13).ToString());
                    document.up9999 = this.reader.IsDBNull(14) ? 0 : double.Parse(this.reader.GetValue(14).ToString());

                    documents.Add(document);
                }
            }
            return documents;
        }

        public List<PaymentAge> GetInvoicePaymentAgeList(string cardCode)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("select seriesName, docNum, CardCode,cardName,DocDate,DocDueDate,DocTotal - PaidToDate pendingToPay, DATEDIFF(dd,DocDueDate,getdate()) pendingTime, numAtCard, ");
            oSQL.Append("CASE when DATEDIFF(dd,DocDueDate,getdate()) <= 30 then DocTotal - PaidToDate else 0 End c30, ");
            oSQL.Append("CASE when DATEDIFF(dd,DocDueDate,getdate()) between 31 and 60 then DocTotal - PaidToDate else 0 End c60, ");
            oSQL.Append("CASE when DATEDIFF(dd,DocDueDate,getdate()) between 61 and 90 then DocTotal - PaidToDate else 0 End c90, ");
            oSQL.Append("CASE when DATEDIFF(dd,DocDueDate,getdate()) between 91 and 120 then DocTotal - PaidToDate else 0 End c120, ");
            oSQL.Append("CASE when DATEDIFF(dd,DocDueDate,getdate()) > 120 then DocTotal - PaidToDate else 0 End c121 ");
            oSQL.Append(string.Format("from OINV a inner join NNM1 b on a.series = b.series where a.cardcode = '{0}' and PaidToDate != DocTotal", cardCode));

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<PaymentAge> documents = new List<PaymentAge>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    PaymentAge document = new PaymentAge();
                    document.seriesName = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    document.docNum = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    document.cardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    document.cardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    document.docDate = DateTime.Parse(this.reader.GetValue(4).ToString()).ToString("yyyy-MM-dd");
                    document.docDueDate = DateTime.Parse(this.reader.GetValue(5).ToString()).ToString("yyyy-MM-dd");
                    document.pendingToPay = this.reader.IsDBNull(6) ? 0 : double.Parse(this.reader.GetValue(6).ToString());
                    document.pendingTime = this.reader.IsDBNull(7) ? 0 : double.Parse(this.reader.GetValue(7).ToString());
                    document.numAtCard = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                    document.up30 = this.reader.IsDBNull(9) ? 0 : double.Parse(this.reader.GetValue(9).ToString());
                    document.up60 = this.reader.IsDBNull(10) ? 0 : double.Parse(this.reader.GetValue(10).ToString());
                    document.up90 = this.reader.IsDBNull(11) ? 0 : double.Parse(this.reader.GetValue(11).ToString());
                    document.up120 = this.reader.IsDBNull(12) ? 0 : double.Parse(this.reader.GetValue(12).ToString());
                    document.up9999 = this.reader.IsDBNull(13) ? 0 : double.Parse(this.reader.GetValue(13).ToString());

                    documents.Add(document);
                }
            }
            return documents;
        }

        public List<BusinessPartnerProp> GetBusinessPartnerPropList()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("select GroupCode, GroupName from OCQG order by GroupCode ");

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<BusinessPartnerProp> bpProps = new List<BusinessPartnerProp>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    BusinessPartnerProp prop = new BusinessPartnerProp();
                    prop.groupCode = int.Parse(this.reader.GetValue(0).ToString());
                    prop.groupName = this.reader.GetValue(1).ToString();

                    bpProps.Add(prop);
                }
            }
            return bpProps;
        }

        public List<ItemPrice> GetBusinessPartnerLastPricesList(string cardCode, DateTime from, DateTime to)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("select distinct a.DocDate, b.ItemCode, c.ItemName, sum(b.Quantity) Quantity, b.Price from ordr a ");
            oSQL.Append("inner join RDR1 b on a.DocEntry = b.DocEntry ");
            oSQL.Append("inner join OITM c on b.ItemCode = c.ItemCode ");
            oSQL.Append(string.Format("where a.cardcode = '{0}' ", cardCode));
            oSQL.Append(string.Format("and a.DocDate between '{0}' and '{1}' ", from.ToString("yyyy-MM-dd"), to.ToString("yyyy-MM-dd")));
            oSQL.Append("group by a.DocDate, b.ItemCode, c.ItemName, b.Price ");
            oSQL.Append("order by a.DocDate desc, b.ItemCode");

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<ItemPrice> prices = new List<ItemPrice>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    ItemPrice price = new ItemPrice();
                    price.docDate = DateTime.Parse(this.reader.GetValue(0).ToString()).ToString("yyyy-MM-dd");
                    price.itemCode = this.reader.GetValue(1).ToString();
                    price.itemName = this.reader.GetValue(2).ToString();
                    price.quantity = double.Parse(this.reader.GetValue(3).ToString());
                    price.price = double.Parse(this.reader.GetValue(4).ToString());

                    prices.Add(price);
                }
            }
            return prices;
        }

        public BusinessPartner GetSingle(string cardCode)
        {
            UtilitiesData utilities = new UtilitiesData(this.connStr);
            BusinessPartner partner = new BusinessPartner();
            StringBuilder oSQL = new StringBuilder();
            List<UserDefinedField> ocrdUdfs = utilities.GetUserDefinedFieldList("OCRD");

            #region Query
            oSQL.Append("SELECT ");
            oSQL.Append("CardCode   ,CardType  ,CardName  ,CardFName ,GroupCode ,LicTradNum,Currency ");
            oSQL.Append(",Phone1    ,Phone2    ,Cellular  ,Fax       ,E_Mail    ,Password  ,SlpCode   ,AgentCode ,CntctPrsn ,Address   ,Territory ");
            oSQL.Append(",GroupNum  ,IntrstRate,ListNum   ,Discount  ,CreditLine,DebtLine  ,DunTerm   ,DebPayAcct,BlockDunn ,WTLiable ");
            oSQL.Append(",QryGroup1 ,QryGroup2 ,QryGroup3 ,QryGroup4 ,QryGroup5 ,QryGroup6 ,QryGroup7 ,QryGroup8 ,QryGroup9 ,QryGroup10 ");
            oSQL.Append(",QryGroup11,QryGroup12,QryGroup13,QryGroup14,QryGroup15,QryGroup16,QryGroup17,QryGroup18,QryGroup19,QryGroup20 ");
            oSQL.Append(",QryGroup21,QryGroup22,QryGroup23,QryGroup24,QryGroup25,QryGroup26,QryGroup27,QryGroup28,QryGroup29,QryGroup30 ");
            oSQL.Append(",QryGroup31,QryGroup32,QryGroup33,QryGroup34,QryGroup35,QryGroup36,QryGroup37,QryGroup38,QryGroup39,QryGroup40 ");
            oSQL.Append(",QryGroup41,QryGroup42,QryGroup43,QryGroup44,QryGroup45,QryGroup46,QryGroup47,QryGroup48,QryGroup49,QryGroup50 ");
            oSQL.Append(",QryGroup51,QryGroup52,QryGroup53,QryGroup54,QryGroup55,QryGroup56,QryGroup57,QryGroup58,QryGroup59,QryGroup60 ");
            oSQL.Append(",QryGroup61,QryGroup62,QryGroup63,QryGroup64,Free_Text ");
            oSQL.Append(",ISNULL(Balance, 0) Balance, ISNULL(DNotesBal, 0) DNotesBal, ISNULL(OrdersBal, 0) OrdersBal");

            foreach (UserDefinedField item in ocrdUdfs)
            {
                oSQL.Append(string.Format(", U_{0} ", item.name));
            }

            oSQL.Append("FROM OCRD with (nolock) ");
            oSQL.Append("WHERE cardCode = @CardCode ");

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            this.dataBase.AddInParameter(myCommand, "CardCode", DbType.String, cardCode);
            #endregion

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    #region Basic Data
                    partner.cardCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    partner.cardType = this.reader.GetValue(1).ToString() == "C" ? CardType.Customer : (this.reader.GetValue(1).ToString() == "S" ? CardType.Supplier : CardType.Lead);
                    partner.cardName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    partner.cardFName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    partner.groupCode = int.Parse(this.reader.GetValue(4).ToString());
                    partner.licTradNum = this.reader.IsDBNull(5) ? "" : this.reader.GetValue(5).ToString();
                    partner.currency = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                    partner.dNotesBal = this.reader.IsDBNull(94) ? 0 : double.Parse(this.reader.GetValue(94).ToString());
                    partner.ordersBal = this.reader.IsDBNull(95) ? 0 : double.Parse(this.reader.GetValue(95).ToString());
                    #endregion

                    #region General tab
                    partner.phone1 = this.reader.IsDBNull(7) ? "" : this.reader.GetValue(7).ToString();
                    partner.phone2 = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                    partner.cellular = this.reader.IsDBNull(9) ? "" : this.reader.GetValue(9).ToString();
                    partner.fax = this.reader.IsDBNull(10) ? "" : this.reader.GetValue(10).ToString();
                    partner.e_Mail = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    partner.password = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    partner.slpCode = int.Parse(this.reader.GetValue(13).ToString());
                    partner.agentCode = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                    partner.cntctPrsn = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                    partner.address = this.reader.IsDBNull(16) ? "" : this.reader.GetValue(16).ToString();
                    partner.territory = this.reader.IsDBNull(17) ? 0 : (int)this.reader.GetValue(17);
                    #endregion

                    #region Payment temrs tab
                    partner.groupNum = this.reader.IsDBNull(18) ? 0 : int.Parse(this.reader.GetValue(18).ToString());
                    partner.intrstRate = this.reader.IsDBNull(19) ? 0 : double.Parse(this.reader.GetValue(19).ToString());
                    partner.listNum = this.reader.IsDBNull(20) ? 0 : int.Parse(this.reader.GetValue(20).ToString());
                    partner.discount = this.reader.IsDBNull(21) ? 0 : double.Parse(this.reader.GetValue(21).ToString());
                    partner.creditLine = this.reader.IsDBNull(22) ? 0 : double.Parse(this.reader.GetValue(22).ToString());
                    partner.debitLine = this.reader.IsDBNull(23) ? 0 : double.Parse(this.reader.GetValue(23).ToString());
                    partner.dunTerm = this.reader.IsDBNull(24) ? "" : this.reader.GetValue(24).ToString();
                    partner.balance = this.reader.IsDBNull(93) ? 0 : double.Parse(this.reader.GetValue(93).ToString());
                    #endregion

                    #region Accounting tab
                    #region general subtab
                    partner.debPayAcct = this.reader.IsDBNull(25) ? "" : this.reader.GetValue(25).ToString();
                    partner.blockDunn = this.reader.IsDBNull(26) ? false : (this.reader.GetValue(26).ToString() == "Y" ? true : false);
                    #endregion

                    #region tax subtab
                    partner.wtLiable = this.reader.IsDBNull(27) ? false : (this.reader.GetValue(27).ToString() == "Y" ? true : false);
                    #endregion
                    #endregion

                    #region Propierties tab
                    partner.qryGroup1 = this.reader.IsDBNull(28) ? false : (this.reader.GetValue(28).ToString() == "Y" ? true : false);
                    partner.qryGroup2 = this.reader.IsDBNull(29) ? false : (this.reader.GetValue(29).ToString() == "Y" ? true : false);
                    partner.qryGroup3 = this.reader.IsDBNull(30) ? false : (this.reader.GetValue(30).ToString() == "Y" ? true : false);
                    partner.qryGroup4 = this.reader.IsDBNull(31) ? false : (this.reader.GetValue(31).ToString() == "Y" ? true : false);
                    partner.qryGroup5 = this.reader.IsDBNull(32) ? false : (this.reader.GetValue(32).ToString() == "Y" ? true : false);
                    partner.qryGroup6 = this.reader.IsDBNull(33) ? false : (this.reader.GetValue(33).ToString() == "Y" ? true : false);
                    partner.qryGroup7 = this.reader.IsDBNull(34) ? false : (this.reader.GetValue(34).ToString() == "Y" ? true : false);
                    partner.qryGroup8 = this.reader.IsDBNull(35) ? false : (this.reader.GetValue(35).ToString() == "Y" ? true : false);
                    partner.qryGroup9 = this.reader.IsDBNull(36) ? false : (this.reader.GetValue(36).ToString() == "Y" ? true : false);
                    partner.qryGroup10 = this.reader.IsDBNull(37) ? false : (this.reader.GetValue(37).ToString() == "Y" ? true : false);
                    partner.qryGroup11 = this.reader.IsDBNull(38) ? false : (this.reader.GetValue(38).ToString() == "Y" ? true : false);
                    partner.qryGroup12 = this.reader.IsDBNull(39) ? false : (this.reader.GetValue(39).ToString() == "Y" ? true : false);
                    partner.qryGroup13 = this.reader.IsDBNull(40) ? false : (this.reader.GetValue(40).ToString() == "Y" ? true : false);
                    partner.qryGroup14 = this.reader.IsDBNull(41) ? false : (this.reader.GetValue(41).ToString() == "Y" ? true : false);
                    partner.qryGroup15 = this.reader.IsDBNull(42) ? false : (this.reader.GetValue(42).ToString() == "Y" ? true : false);
                    partner.qryGroup16 = this.reader.IsDBNull(43) ? false : (this.reader.GetValue(43).ToString() == "Y" ? true : false);
                    partner.qryGroup17 = this.reader.IsDBNull(44) ? false : (this.reader.GetValue(44).ToString() == "Y" ? true : false);
                    partner.qryGroup18 = this.reader.IsDBNull(45) ? false : (this.reader.GetValue(45).ToString() == "Y" ? true : false);
                    partner.qryGroup19 = this.reader.IsDBNull(46) ? false : (this.reader.GetValue(46).ToString() == "Y" ? true : false);
                    partner.qryGroup20 = this.reader.IsDBNull(47) ? false : (this.reader.GetValue(47).ToString() == "Y" ? true : false);
                    partner.qryGroup21 = this.reader.IsDBNull(48) ? false : (this.reader.GetValue(48).ToString() == "Y" ? true : false);
                    partner.qryGroup22 = this.reader.IsDBNull(49) ? false : (this.reader.GetValue(49).ToString() == "Y" ? true : false);
                    partner.qryGroup23 = this.reader.IsDBNull(50) ? false : (this.reader.GetValue(50).ToString() == "Y" ? true : false);
                    partner.qryGroup24 = this.reader.IsDBNull(51) ? false : (this.reader.GetValue(51).ToString() == "Y" ? true : false);
                    partner.qryGroup25 = this.reader.IsDBNull(52) ? false : (this.reader.GetValue(52).ToString() == "Y" ? true : false);
                    partner.qryGroup26 = this.reader.IsDBNull(53) ? false : (this.reader.GetValue(53).ToString() == "Y" ? true : false);
                    partner.qryGroup27 = this.reader.IsDBNull(54) ? false : (this.reader.GetValue(54).ToString() == "Y" ? true : false);
                    partner.qryGroup28 = this.reader.IsDBNull(55) ? false : (this.reader.GetValue(55).ToString() == "Y" ? true : false);
                    partner.qryGroup29 = this.reader.IsDBNull(56) ? false : (this.reader.GetValue(56).ToString() == "Y" ? true : false);
                    partner.qryGroup30 = this.reader.IsDBNull(57) ? false : (this.reader.GetValue(57).ToString() == "Y" ? true : false);
                    partner.qryGroup31 = this.reader.IsDBNull(58) ? false : (this.reader.GetValue(58).ToString() == "Y" ? true : false);
                    partner.qryGroup32 = this.reader.IsDBNull(59) ? false : (this.reader.GetValue(59).ToString() == "Y" ? true : false);
                    partner.qryGroup33 = this.reader.IsDBNull(60) ? false : (this.reader.GetValue(60).ToString() == "Y" ? true : false);
                    partner.qryGroup34 = this.reader.IsDBNull(61) ? false : (this.reader.GetValue(61).ToString() == "Y" ? true : false);
                    partner.qryGroup35 = this.reader.IsDBNull(62) ? false : (this.reader.GetValue(62).ToString() == "Y" ? true : false);
                    partner.qryGroup36 = this.reader.IsDBNull(63) ? false : (this.reader.GetValue(63).ToString() == "Y" ? true : false);
                    partner.qryGroup37 = this.reader.IsDBNull(64) ? false : (this.reader.GetValue(64).ToString() == "Y" ? true : false);
                    partner.qryGroup38 = this.reader.IsDBNull(65) ? false : (this.reader.GetValue(65).ToString() == "Y" ? true : false);
                    partner.qryGroup39 = this.reader.IsDBNull(66) ? false : (this.reader.GetValue(66).ToString() == "Y" ? true : false);
                    partner.qryGroup40 = this.reader.IsDBNull(67) ? false : (this.reader.GetValue(67).ToString() == "Y" ? true : false);
                    partner.qryGroup41 = this.reader.IsDBNull(68) ? false : (this.reader.GetValue(68).ToString() == "Y" ? true : false);
                    partner.qryGroup42 = this.reader.IsDBNull(69) ? false : (this.reader.GetValue(69).ToString() == "Y" ? true : false);
                    partner.qryGroup43 = this.reader.IsDBNull(70) ? false : (this.reader.GetValue(70).ToString() == "Y" ? true : false);
                    partner.qryGroup44 = this.reader.IsDBNull(71) ? false : (this.reader.GetValue(71).ToString() == "Y" ? true : false);
                    partner.qryGroup45 = this.reader.IsDBNull(72) ? false : (this.reader.GetValue(72).ToString() == "Y" ? true : false);
                    partner.qryGroup46 = this.reader.IsDBNull(73) ? false : (this.reader.GetValue(73).ToString() == "Y" ? true : false);
                    partner.qryGroup47 = this.reader.IsDBNull(74) ? false : (this.reader.GetValue(74).ToString() == "Y" ? true : false);
                    partner.qryGroup48 = this.reader.IsDBNull(75) ? false : (this.reader.GetValue(75).ToString() == "Y" ? true : false);
                    partner.qryGroup49 = this.reader.IsDBNull(76) ? false : (this.reader.GetValue(76).ToString() == "Y" ? true : false);
                    partner.qryGroup50 = this.reader.IsDBNull(77) ? false : (this.reader.GetValue(77).ToString() == "Y" ? true : false);
                    partner.qryGroup51 = this.reader.IsDBNull(78) ? false : (this.reader.GetValue(78).ToString() == "Y" ? true : false);
                    partner.qryGroup52 = this.reader.IsDBNull(79) ? false : (this.reader.GetValue(79).ToString() == "Y" ? true : false);
                    partner.qryGroup53 = this.reader.IsDBNull(80) ? false : (this.reader.GetValue(80).ToString() == "Y" ? true : false);
                    partner.qryGroup54 = this.reader.IsDBNull(81) ? false : (this.reader.GetValue(81).ToString() == "Y" ? true : false);
                    partner.qryGroup55 = this.reader.IsDBNull(82) ? false : (this.reader.GetValue(82).ToString() == "Y" ? true : false);
                    partner.qryGroup56 = this.reader.IsDBNull(83) ? false : (this.reader.GetValue(83).ToString() == "Y" ? true : false);
                    partner.qryGroup57 = this.reader.IsDBNull(84) ? false : (this.reader.GetValue(84).ToString() == "Y" ? true : false);
                    partner.qryGroup58 = this.reader.IsDBNull(85) ? false : (this.reader.GetValue(85).ToString() == "Y" ? true : false);
                    partner.qryGroup59 = this.reader.IsDBNull(86) ? false : (this.reader.GetValue(86).ToString() == "Y" ? true : false);
                    partner.qryGroup60 = this.reader.IsDBNull(87) ? false : (this.reader.GetValue(87).ToString() == "Y" ? true : false);
                    partner.qryGroup61 = this.reader.IsDBNull(88) ? false : (this.reader.GetValue(88).ToString() == "Y" ? true : false);
                    partner.qryGroup62 = this.reader.IsDBNull(89) ? false : (this.reader.GetValue(89).ToString() == "Y" ? true : false);
                    partner.qryGroup63 = this.reader.IsDBNull(90) ? false : (this.reader.GetValue(90).ToString() == "Y" ? true : false);
                    partner.qryGroup64 = this.reader.IsDBNull(91) ? false : (this.reader.GetValue(91).ToString() == "Y" ? true : false);
                    #endregion

                    #region Remarks tab
                    partner.freeText = this.reader.IsDBNull(92) ? "" : this.reader.GetValue(92).ToString();
                    #endregion

                    #region UDF's
                    int currentField = 96;
                    foreach (UserDefinedField item in ocrdUdfs)
                    {
                        item.value = this.reader.IsDBNull(currentField) ? "" : this.reader.GetValue(currentField).ToString();
                        partner.userDefinedFields.Add(item);

                        currentField++;
                    }
                    #endregion
                }
            }
            return partner;
        }

        public bool GetCreditStatus(string cardCode)
        {
            int hits = 0;
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("select CASE When DATEDIFF(DD, a.DocDueDate, GETDATE()) > c.ExtraDays then 1 when DATEDIFF(DD, a.DocDueDate, GETDATE()) <= c.ExtraDays then 0 else 0 end delayed ");
            oSQL.Append("from OINV a inner join OCRD b on a.CardCode = b.CardCode inner join OCTG c on b.GroupNum = c.GroupNum ");
            oSQL.Append(string.Format("where a.cardcode = '{0}' ", cardCode));

            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());


            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    if (double.Parse(this.reader.GetValue(0).ToString()) > 0)
                        hits++;
                }
            }

            return hits > 0 ? true : false;
        }

        public int GetOldestOpenInvoice(string cardCode)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append(string.Format("select MAX(DATEDIFF(DD, DocDate, GETDATE())) From OINV a Where CardCode = '{0}' and DocStatus = 'O' ", cardCode));

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            int invoice = 0;

            if (!int.TryParse(this.dataBase.ExecuteScalar(dbCommand).ToString(), out invoice))
                invoice = -1;

            return invoice;
        }

        public void Add(BusinessPartner partner)
        {
            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            #region Basic Data
            bp.CardCode = partner.cardCode;

            switch (partner.cardType)
            {
                case CardType.Customer:
                    bp.CardType = BoCardTypes.cCustomer;
                    break;
                case CardType.Supplier:
                    bp.CardType = BoCardTypes.cSupplier;
                    break;
                case CardType.Lead:
                    bp.CardType = BoCardTypes.cLid;
                    break;
                default:
                    break;
            }

            bp.CardName = partner.cardName;

            if (!string.IsNullOrEmpty(partner.cardFName))
                bp.CardForeignName = partner.cardFName;

            bp.GroupCode = partner.groupCode;
            bp.FederalTaxID = partner.licTradNum;
            bp.Currency = partner.currency;
            #endregion

            #region General tab
            bp.Phone1 = partner.phone1;

            if (!string.IsNullOrEmpty(partner.phone2))
                bp.Phone2 = partner.phone2;
            if (!string.IsNullOrEmpty(partner.cellular))
                bp.Cellular = partner.cellular;
            if (!string.IsNullOrEmpty(partner.fax))
                bp.Fax = partner.fax;
            if (!string.IsNullOrEmpty(partner.e_Mail))
                bp.EmailAddress = partner.e_Mail;
            if (!string.IsNullOrEmpty(partner.password))
                bp.Password = partner.password;
            if (partner.slpCode != 0)
                bp.SalesPersonCode = partner.slpCode;
            if (partner.territory != null)
                bp.Territory = (int)partner.territory;

            /*
             * DI-Api not expose agent Code field
            if (!string.IsNullOrEmpty(partner.agentCode))
                bp.a = partner.phone2;
             * 
            */

            #endregion

            #region Payment temrs tab
            if (partner.groupNum != null)
                bp.PayTermsGrpCode = (int)partner.groupNum;
            if (partner.intrstRate != null)
                bp.IntrestRatePercent = (double)partner.intrstRate;
            if (partner.listNum != null)
                bp.PriceListNum = (int)partner.listNum;
            if (partner.discount != null)
                bp.DiscountPercent = (double)partner.discount;
            if (partner.creditLine != null)
                bp.CreditLimit = (double)partner.creditLine;
            if (partner.debitLine != null)
                bp.MaxCommitment = (double)partner.debitLine;
            if (!string.IsNullOrEmpty(partner.dunTerm))
                bp.DunningTerm = partner.dunTerm;
            #endregion

            #region Accounting tab
            #region general subtab
            if (!string.IsNullOrEmpty(partner.debPayAcct))
                bp.DebitorAccount = partner.debPayAcct;

            if (partner.blockDunn)
                bp.BlockDunning = BoYesNoEnum.tYES;
            else
                bp.BlockDunning = BoYesNoEnum.tNO;

            #endregion
            #region tax subtab
            if (partner.wtLiable)
            {
                bp.SubjectToWithholdingTax = BoYesNoEnum.tYES;
                //foreach (BusinessPartnerWithholdingTax item in partner.withholdingTaxes)
                //{
                //    bp.BPWithholdingTax.WTCode = item.wtCode;
                //    bp.BPWithholdingTax.Add();
                //}
            }
            #endregion
            #endregion

            #region Propierties tab
            bp.set_Properties(1, partner.qryGroup1 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(2, partner.qryGroup2 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(3, partner.qryGroup3 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(4, partner.qryGroup4 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(5, partner.qryGroup5 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(6, partner.qryGroup6 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(7, partner.qryGroup7 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(8, partner.qryGroup8 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(9, partner.qryGroup9 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(10, partner.qryGroup10 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(11, partner.qryGroup11 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(12, partner.qryGroup12 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(13, partner.qryGroup13 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(14, partner.qryGroup14 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(15, partner.qryGroup15 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(16, partner.qryGroup16 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(17, partner.qryGroup17 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(18, partner.qryGroup18 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(19, partner.qryGroup19 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(20, partner.qryGroup20 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(21, partner.qryGroup21 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(22, partner.qryGroup22 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(23, partner.qryGroup23 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(24, partner.qryGroup24 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(25, partner.qryGroup25 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(26, partner.qryGroup26 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(27, partner.qryGroup27 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(28, partner.qryGroup28 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(29, partner.qryGroup29 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(30, partner.qryGroup30 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(31, partner.qryGroup31 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(32, partner.qryGroup32 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(33, partner.qryGroup33 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(34, partner.qryGroup34 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(35, partner.qryGroup35 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(36, partner.qryGroup36 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(37, partner.qryGroup37 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(38, partner.qryGroup38 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(39, partner.qryGroup39 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(40, partner.qryGroup40 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(41, partner.qryGroup41 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(42, partner.qryGroup42 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(43, partner.qryGroup43 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(44, partner.qryGroup44 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(45, partner.qryGroup45 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(46, partner.qryGroup46 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(47, partner.qryGroup47 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(48, partner.qryGroup48 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(49, partner.qryGroup49 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(50, partner.qryGroup50 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(51, partner.qryGroup51 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(52, partner.qryGroup52 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(53, partner.qryGroup53 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(54, partner.qryGroup54 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(55, partner.qryGroup55 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(56, partner.qryGroup56 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(57, partner.qryGroup57 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(58, partner.qryGroup58 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(59, partner.qryGroup59 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(60, partner.qryGroup60 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(61, partner.qryGroup61 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(62, partner.qryGroup62 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(63, partner.qryGroup63 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(64, partner.qryGroup64 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            #endregion

            #region Remarks tab
            if (!string.IsNullOrEmpty(partner.freeText))
                bp.FreeText = partner.freeText;
            #endregion

            #region UDF's
            if (partner.userDefinedFields != null)
                foreach (UserDefinedField item in partner.userDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                bp.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                bp.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
            #endregion

            if (bp.Add() != 0)
                throw new SAPException(SAPConnection.conn.company.GetLastErrorCode(), SAPConnection.conn.company.GetLastErrorDescription());

            #region Contact persons tab
            //foreach (ContactEmployee item in partner.contactPersons)
            //{
            //    bp.ContactEmployees.Name = item.name;

            //    if (!string.IsNullOrEmpty(item.firstName))
            //        bp.ContactEmployees.FirstName = item.firstName;
            //    if (!string.IsNullOrEmpty(item.middleName))
            //        bp.ContactEmployees.MiddleName = item.middleName;
            //    if (!string.IsNullOrEmpty(item.lastName))
            //        bp.ContactEmployees.LastName = item.lastName;
            //    if (!string.IsNullOrEmpty(item.title))
            //        bp.ContactEmployees.Title = item.title;
            //    if (!string.IsNullOrEmpty(item.position))
            //        bp.ContactEmployees.Position = item.position;
            //    if (!string.IsNullOrEmpty(item.address))
            //        bp.ContactEmployees.Address = item.address;
            //    if (!string.IsNullOrEmpty(item.telephone1))
            //        bp.ContactEmployees.Phone1 = item.telephone1;
            //    if (!string.IsNullOrEmpty(item.telephone2))
            //        bp.ContactEmployees.Phone2 = item.telephone2;
            //    if (!string.IsNullOrEmpty(item.cellolar))
            //        bp.ContactEmployees.MobilePhone = item.cellolar;
            //    if (!string.IsNullOrEmpty(item.e_mail))
            //        bp.ContactEmployees.E_Mail = item.e_mail;

            //    bp.ContactEmployees.Add();

            //    //if (item.defaultContact)
            //    //    bp.ContactPerson = item.name;

            //    //bp.Update();
            //}
            #endregion

            #region Addresses tab
            //foreach (BusinessPartnerAddress item in partner.addresses)
            //{
            //    bp.Addresses.AddressType = item.addressType == AddressType.BillTo ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;
            //    bp.Addresses.AddressName = item.address;
            //    bp.Addresses.Street = item.street;
            //    bp.Addresses.Block = item.block;
            //    bp.Addresses.ZipCode = item.zipCode;
            //    bp.Addresses.City = item.city;
            //    bp.Addresses.Country = item.coutry;
            //    bp.Addresses.TaxCode = item.taxCode;
            //    bp.Addresses.StreetNo = item.streetNo;
            //    bp.Addresses.Add();

            //    if (item.defaultAddress)
            //        bp.Address = item.address;
            //}
            #endregion
        }

        public void Add(BusinessPartner partner, SAPConnection sapConn)
        {
            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)sapConn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            #region Basic Data
            bp.CardCode = partner.cardCode;

            switch (partner.cardType)
            {
                case CardType.Customer:
                    bp.CardType = BoCardTypes.cCustomer;
                    break;
                case CardType.Supplier:
                    bp.CardType = BoCardTypes.cSupplier;
                    break;
                case CardType.Lead:
                    bp.CardType = BoCardTypes.cLid;
                    break;
                default:
                    break;
            }

            bp.CardName = partner.cardName;

            if (!string.IsNullOrEmpty(partner.cardFName))
                bp.CardForeignName = partner.cardFName;

            bp.GroupCode = partner.groupCode;
            bp.FederalTaxID = partner.licTradNum;
            bp.Currency = partner.currency;
            #endregion

            #region General tab
            bp.Phone1 = partner.phone1;

            if (!string.IsNullOrEmpty(partner.phone2))
                bp.Phone2 = partner.phone2;
            if (!string.IsNullOrEmpty(partner.cellular))
                bp.Cellular = partner.cellular;
            if (!string.IsNullOrEmpty(partner.fax))
                bp.Fax = partner.fax;
            if (!string.IsNullOrEmpty(partner.e_Mail))
                bp.EmailAddress = partner.e_Mail;
            if (!string.IsNullOrEmpty(partner.password))
                bp.Password = partner.password;
            if (partner.slpCode != 0)
                bp.SalesPersonCode = partner.slpCode;
            if (partner.territory != null)
                bp.Territory = (int)partner.territory;

            /*
             * DI-Api not expose agent Code field
            if (!string.IsNullOrEmpty(partner.agentCode))
                bp.a = partner.phone2;
             * 
            */

            #endregion

            #region Payment temrs tab
            if (partner.groupNum != null)
                bp.PayTermsGrpCode = (int)partner.groupNum;
            if (partner.intrstRate != null)
                bp.IntrestRatePercent = (double)partner.intrstRate;
            if (partner.listNum != null)
                bp.PriceListNum = (int)partner.listNum;
            if (partner.discount != null)
                bp.DiscountPercent = (double)partner.discount;
            if (partner.creditLine != null)
                bp.CreditLimit = (double)partner.creditLine;
            if (partner.debitLine != null)
                bp.MaxCommitment = (double)partner.debitLine;
            if (!string.IsNullOrEmpty(partner.dunTerm))
                bp.DunningTerm = partner.dunTerm;
            #endregion

            #region Accounting tab
            #region general subtab
            if (!string.IsNullOrEmpty(partner.debPayAcct))
                bp.DebitorAccount = partner.debPayAcct;

            if (partner.blockDunn)
                bp.BlockDunning = BoYesNoEnum.tYES;
            else
                bp.BlockDunning = BoYesNoEnum.tNO;

            #endregion
            #region tax subtab
            if (partner.wtLiable)
            {
                bp.SubjectToWithholdingTax = BoYesNoEnum.tYES;
            }
            #endregion
            #endregion

            #region Propierties tab
            bp.set_Properties(1, partner.qryGroup1 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(2, partner.qryGroup2 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(3, partner.qryGroup3 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(4, partner.qryGroup4 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(5, partner.qryGroup5 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(6, partner.qryGroup6 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(7, partner.qryGroup7 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(8, partner.qryGroup8 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(9, partner.qryGroup9 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(10, partner.qryGroup10 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(11, partner.qryGroup11 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(12, partner.qryGroup12 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(13, partner.qryGroup13 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(14, partner.qryGroup14 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(15, partner.qryGroup15 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(16, partner.qryGroup16 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(17, partner.qryGroup17 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(18, partner.qryGroup18 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(19, partner.qryGroup19 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(20, partner.qryGroup20 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(21, partner.qryGroup21 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(22, partner.qryGroup22 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(23, partner.qryGroup23 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(24, partner.qryGroup24 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(25, partner.qryGroup25 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(26, partner.qryGroup26 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(27, partner.qryGroup27 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(28, partner.qryGroup28 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(29, partner.qryGroup29 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(30, partner.qryGroup30 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(31, partner.qryGroup31 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(32, partner.qryGroup32 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(33, partner.qryGroup33 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(34, partner.qryGroup34 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(35, partner.qryGroup35 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(36, partner.qryGroup36 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(37, partner.qryGroup37 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(38, partner.qryGroup38 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(39, partner.qryGroup39 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(40, partner.qryGroup40 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(41, partner.qryGroup41 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(42, partner.qryGroup42 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(43, partner.qryGroup43 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(44, partner.qryGroup44 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(45, partner.qryGroup45 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(46, partner.qryGroup46 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(47, partner.qryGroup47 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(48, partner.qryGroup48 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(49, partner.qryGroup49 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(50, partner.qryGroup50 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(51, partner.qryGroup51 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(52, partner.qryGroup52 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(53, partner.qryGroup53 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(54, partner.qryGroup54 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(55, partner.qryGroup55 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(56, partner.qryGroup56 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(57, partner.qryGroup57 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(58, partner.qryGroup58 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(59, partner.qryGroup59 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(60, partner.qryGroup60 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(61, partner.qryGroup61 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(62, partner.qryGroup62 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(63, partner.qryGroup63 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(64, partner.qryGroup64 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            #endregion

            #region Remarks tab
            if (!string.IsNullOrEmpty(partner.freeText))
                bp.FreeText = partner.freeText;
            #endregion

            #region UDF's
            if (partner.userDefinedFields != null)
                foreach (UserDefinedField item in partner.userDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                bp.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                bp.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
            #endregion

            if (bp.Add() != 0)
                throw new SAPException(sapConn.company.GetLastErrorCode(), sapConn.company.GetLastErrorDescription());
        }

        public void AddAddress(BusinessPartnerAddress address)
        {
            int addressQty = GetAddressList(address.cardCode, AddressType.BillTo).Count() + GetAddressList(address.cardCode, AddressType.ShipTo).Count();

            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            bp.GetByKey(address.cardCode);

            bp.Addresses.Add();
            bp.Addresses.SetCurrentLine(addressQty);

            bp.Addresses.AddressType = address.addressType == AddressType.BillTo ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;
            bp.Addresses.AddressName = address.address;

            if (!string.IsNullOrEmpty(address.street))
                bp.Addresses.Street = address.street;

            if (!string.IsNullOrEmpty(address.block))
                bp.Addresses.Block = address.block;

            if (!string.IsNullOrEmpty(address.zipCode))
                bp.Addresses.ZipCode = address.zipCode;

            if (!string.IsNullOrEmpty(address.city))
                bp.Addresses.City = address.city;

            if (!string.IsNullOrEmpty(address.country))
                bp.Addresses.Country = address.country;

            if (!string.IsNullOrEmpty(address.county))
                bp.Addresses.County = address.county;

            if (!string.IsNullOrEmpty(address.taxCode))
                bp.Addresses.TaxCode = address.taxCode;

            if (!string.IsNullOrEmpty(address.streetNo))
                bp.Addresses.StreetNo = address.streetNo;

            if (!string.IsNullOrEmpty(address.state))
                bp.Addresses.State = address.state;

            #region UDF's
            if (address.UserDefinedFields != null)
                foreach (UserDefinedField item in address.UserDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                bp.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                bp.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
            #endregion

            bp.Update();
        }

        public void AddAddress(BusinessPartnerAddress address, SAPConnection sapConn)
        {
            int addressQty = GetAddressList(address.cardCode, AddressType.BillTo).Count() + GetAddressList(address.cardCode, AddressType.ShipTo).Count();

            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)sapConn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            bp.GetByKey(address.cardCode);

            bp.Addresses.Add();
            bp.Addresses.SetCurrentLine(addressQty);

            bp.Addresses.AddressType = address.addressType == AddressType.BillTo ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;
            bp.Addresses.AddressName = address.address;

            if (!string.IsNullOrEmpty(address.street))
                bp.Addresses.Street = address.street;

            if (!string.IsNullOrEmpty(address.block))
                bp.Addresses.Block = address.block;

            if (!string.IsNullOrEmpty(address.zipCode))
                bp.Addresses.ZipCode = address.zipCode;

            if (!string.IsNullOrEmpty(address.city))
                bp.Addresses.City = address.city;

            if (!string.IsNullOrEmpty(address.country))
                bp.Addresses.Country = address.country;

            if (!string.IsNullOrEmpty(address.county))
                bp.Addresses.County = address.county;

            if (!string.IsNullOrEmpty(address.taxCode))
                bp.Addresses.TaxCode = address.taxCode;

            if (!string.IsNullOrEmpty(address.streetNo))
                bp.Addresses.StreetNo = address.streetNo;

            if (!string.IsNullOrEmpty(address.state))
                bp.Addresses.State = address.state;

            #region UDF's
            if (address.UserDefinedFields != null)
                foreach (UserDefinedField item in address.UserDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                bp.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                bp.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
            #endregion

            bp.Update();
        }

        public void AddContact(ContactEmployee contact)
        {
            List<ContactEmployee> contacts = GetContactList(contact.cardCode);

            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            bp.GetByKey(contact.cardCode);
            int contactQty = contacts.Count();

            bp.ContactEmployees.Add();
            bp.ContactEmployees.SetCurrentLine(contactQty);
            bp.ContactEmployees.Name = contact.name;

            if (!string.IsNullOrEmpty(contact.firstName))
                bp.ContactEmployees.FirstName = contact.firstName;

            if (!string.IsNullOrEmpty(contact.middleName))
                bp.ContactEmployees.MiddleName = contact.middleName;

            if (!string.IsNullOrEmpty(contact.lastName))
                bp.ContactEmployees.LastName = contact.lastName;

            if (!string.IsNullOrEmpty(contact.title))
                bp.ContactEmployees.Title = contact.title;

            if (!string.IsNullOrEmpty(contact.position))
                bp.ContactEmployees.Position = contact.position;

            if (!string.IsNullOrEmpty(contact.address))
                bp.ContactEmployees.Address = contact.address;

            if (!string.IsNullOrEmpty(contact.telephone1))
                bp.ContactEmployees.Phone1 = contact.telephone1;

            if (!string.IsNullOrEmpty(contact.telephone2))
                bp.ContactEmployees.Phone2 = contact.telephone2;

            if (!string.IsNullOrEmpty(contact.cellolar))
                bp.ContactEmployees.MobilePhone = contact.cellolar;

            if (!string.IsNullOrEmpty(contact.e_mail))
                bp.ContactEmployees.E_Mail = contact.e_mail;

            bp.Update();
        }

        public void AddContact(ContactEmployee contact, SAPConnection sapConn)
        {
            List<ContactEmployee> contacts = GetContactList(contact.cardCode);

            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)sapConn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            bp.GetByKey(contact.cardCode);
            int contactQty = contacts.Count();

            bp.ContactEmployees.Add();
            bp.ContactEmployees.SetCurrentLine(contactQty);
            bp.ContactEmployees.Name = contact.name;

            if (!string.IsNullOrEmpty(contact.firstName))
                bp.ContactEmployees.FirstName = contact.firstName;

            if (!string.IsNullOrEmpty(contact.middleName))
                bp.ContactEmployees.MiddleName = contact.middleName;

            if (!string.IsNullOrEmpty(contact.lastName))
                bp.ContactEmployees.LastName = contact.lastName;

            if (!string.IsNullOrEmpty(contact.title))
                bp.ContactEmployees.Title = contact.title;

            if (!string.IsNullOrEmpty(contact.position))
                bp.ContactEmployees.Position = contact.position;

            if (!string.IsNullOrEmpty(contact.address))
                bp.ContactEmployees.Address = contact.address;

            if (!string.IsNullOrEmpty(contact.telephone1))
                bp.ContactEmployees.Phone1 = contact.telephone1;

            if (!string.IsNullOrEmpty(contact.telephone2))
                bp.ContactEmployees.Phone2 = contact.telephone2;

            if (!string.IsNullOrEmpty(contact.cellolar))
                bp.ContactEmployees.MobilePhone = contact.cellolar;

            if (!string.IsNullOrEmpty(contact.e_mail))
                bp.ContactEmployees.E_Mail = contact.e_mail;

            bp.Update();
        }

        public void AddBusinessPartnerWithholdingTax(BusinessPartnerWithholdingTax withholdingTax)
        {
            List<BusinessPartnerWithholdingTax> bpWithholdingTaxes = GetBusinessPartnerWithholdingTaxList(withholdingTax.cardCode);

            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            bp.GetByKey(withholdingTax.cardCode);
            int wtQty = bpWithholdingTaxes.Count();

            bp.BPWithholdingTax.Add();
            bp.BPWithholdingTax.SetCurrentLine(wtQty);
            bp.BPWithholdingTax.WTCode = withholdingTax.wtCode;

            bp.Update();
        }

        public void AddBusinessPartnerWithholdingTax(BusinessPartnerWithholdingTax withholdingTax, SAPConnection sapConn)
        {
            List<BusinessPartnerWithholdingTax> bpWithholdingTaxes = GetBusinessPartnerWithholdingTaxList(withholdingTax.cardCode);

            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)sapConn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            bp.GetByKey(withholdingTax.cardCode);
            int wtQty = bpWithholdingTaxes.Count();

            bp.BPWithholdingTax.Add();
            bp.BPWithholdingTax.SetCurrentLine(wtQty);
            bp.BPWithholdingTax.WTCode = withholdingTax.wtCode;

            bp.Update();
        }

        public void Update(BusinessPartner partner)
        {
            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);

            bp.GetByKey(partner.cardCode);

            #region Basic Data
            //switch (partner.cardType)
            //{
            //    case CardType.Customer:
            //        bp.CardType = BoCardTypes.cCustomer;
            //        break;
            //    case CardType.Supplier:
            //        bp.CardType = BoCardTypes.cSupplier;
            //        break;
            //    case CardType.Lead:
            //        bp.CardType = BoCardTypes.cLid;
            //        break;
            //    default:
            //        break;
            //}

            bp.CardName = partner.cardName;

            if (!string.IsNullOrEmpty(partner.cardFName))
                bp.CardForeignName = partner.cardFName;

            bp.GroupCode = partner.groupCode;
            bp.FederalTaxID = partner.licTradNum;
            bp.Currency = partner.currency;
            #endregion

            #region General tab
            bp.Phone1 = partner.phone1;

            if (!string.IsNullOrEmpty(partner.phone2))
                bp.Phone2 = partner.phone2;
            if (!string.IsNullOrEmpty(partner.cellular))
                bp.Cellular = partner.cellular;
            if (!string.IsNullOrEmpty(partner.fax))
                bp.Fax = partner.fax;
            if (!string.IsNullOrEmpty(partner.e_Mail))
                bp.EmailAddress = partner.e_Mail;
            if (!string.IsNullOrEmpty(partner.password))
                bp.Password = partner.password;
            if (partner.slpCode != 0)
                bp.SalesPersonCode = partner.slpCode;
            if (partner.territory != null)
                bp.Territory = (int)partner.territory;

            #endregion

            #region Payment temrs tab
            if (partner.groupNum != null)
                bp.PayTermsGrpCode = (int)partner.groupNum;
            if (partner.intrstRate != null)
                bp.IntrestRatePercent = (double)partner.intrstRate;
            if (partner.listNum != null)
                bp.PriceListNum = (int)partner.listNum;
            if (partner.discount != null)
                bp.DiscountPercent = (double)partner.discount;
            if (partner.creditLine != null)
                bp.CreditLimit = (double)partner.creditLine;
            if (partner.debitLine != null)
                bp.MaxCommitment = (double)partner.debitLine;
            if (!string.IsNullOrEmpty(partner.dunTerm))
                bp.DunningTerm = partner.dunTerm;
            #endregion

            #region Accounting tab
            #region general subtab
            if (!string.IsNullOrEmpty(partner.debPayAcct))
                bp.DebitorAccount = partner.debPayAcct;

            if (partner.blockDunn)
                bp.BlockDunning = BoYesNoEnum.tYES;
            else
                bp.BlockDunning = BoYesNoEnum.tNO;

            #endregion
            #region tax subtab
            if (partner.wtLiable)
            {
                bp.SubjectToWithholdingTax = BoYesNoEnum.tYES;
            }
            #endregion
            #endregion

            #region Propierties tab
            bp.set_Properties(1, partner.qryGroup1 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(2, partner.qryGroup2 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(3, partner.qryGroup3 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(4, partner.qryGroup4 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(5, partner.qryGroup5 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(6, partner.qryGroup6 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(7, partner.qryGroup7 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(8, partner.qryGroup8 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(9, partner.qryGroup9 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(10, partner.qryGroup10 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(11, partner.qryGroup11 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(12, partner.qryGroup12 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(13, partner.qryGroup13 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(14, partner.qryGroup14 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(15, partner.qryGroup15 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(16, partner.qryGroup16 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(17, partner.qryGroup17 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(18, partner.qryGroup18 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(19, partner.qryGroup19 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(20, partner.qryGroup20 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(21, partner.qryGroup21 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(22, partner.qryGroup22 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(23, partner.qryGroup23 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(24, partner.qryGroup24 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(25, partner.qryGroup25 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(26, partner.qryGroup26 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(27, partner.qryGroup27 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(28, partner.qryGroup28 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(29, partner.qryGroup29 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(30, partner.qryGroup30 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(31, partner.qryGroup31 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(32, partner.qryGroup32 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(33, partner.qryGroup33 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(34, partner.qryGroup34 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(35, partner.qryGroup35 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(36, partner.qryGroup36 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(37, partner.qryGroup37 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(38, partner.qryGroup38 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(39, partner.qryGroup39 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(40, partner.qryGroup40 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(41, partner.qryGroup41 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(42, partner.qryGroup42 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(43, partner.qryGroup43 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(44, partner.qryGroup44 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(45, partner.qryGroup45 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(46, partner.qryGroup46 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(47, partner.qryGroup47 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(48, partner.qryGroup48 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(49, partner.qryGroup49 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(50, partner.qryGroup50 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(51, partner.qryGroup51 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(52, partner.qryGroup52 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(53, partner.qryGroup53 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(54, partner.qryGroup54 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(55, partner.qryGroup55 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(56, partner.qryGroup56 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(57, partner.qryGroup57 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(58, partner.qryGroup58 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(59, partner.qryGroup59 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(60, partner.qryGroup60 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(61, partner.qryGroup61 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(62, partner.qryGroup62 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(63, partner.qryGroup63 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(64, partner.qryGroup64 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            #endregion

            #region Remarks tab
            if (!string.IsNullOrEmpty(partner.freeText))
                bp.FreeText = partner.freeText;
            #endregion

            #region UDF's
            if (partner.userDefinedFields != null)
                foreach (UserDefinedField item in partner.userDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                bp.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                bp.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
            #endregion

            if (bp.Update() != 0)
                throw new SAPException(SAPConnection.conn.company.GetLastErrorCode(), SAPConnection.conn.company.GetLastErrorDescription());
        }

        public void Update(BusinessPartner partner, SAPConnection sapConn)
        {
            BusinessPartners bp; //= new BusinessPartners();
            bp = (BusinessPartners)sapConn.company.GetBusinessObject(BoObjectTypes.oBusinessPartners);


            if (!bp.GetByKey(partner.cardCode))
                throw new SAPException(-9000, "Socio no encontrado");

            #region Basic Data
            bp.CardName = partner.cardName;

            if (!string.IsNullOrEmpty(partner.cardFName))
                bp.CardForeignName = partner.cardFName;

            bp.GroupCode = partner.groupCode;
            bp.FederalTaxID = partner.licTradNum;
            //bp.Currency = partner.currency;
            #endregion

            #region General tab
            bp.Phone1 = partner.phone1;

            if (!string.IsNullOrEmpty(partner.phone2))
                bp.Phone2 = partner.phone2;

            if (!string.IsNullOrEmpty(partner.cellular))
                bp.Cellular = partner.cellular;

            if (!string.IsNullOrEmpty(partner.fax))
                bp.Fax = partner.fax;

            if (!string.IsNullOrEmpty(partner.e_Mail))
                bp.EmailAddress = partner.e_Mail;

            if (!string.IsNullOrEmpty(partner.password))
                bp.Password = partner.password;

            if (partner.slpCode != 0)
                bp.SalesPersonCode = partner.slpCode;
            if (partner.territory != null)
                bp.Territory = (int)partner.territory;

            #endregion

            #region Payment temrs tab
            if (partner.groupNum != null)
                bp.PayTermsGrpCode = (int)partner.groupNum;
            if (partner.intrstRate != null)
                bp.IntrestRatePercent = (double)partner.intrstRate;
            if (partner.listNum != null)
                bp.PriceListNum = (int)partner.listNum;
            if (partner.discount != null)
                bp.DiscountPercent = (double)partner.discount;
            if (partner.creditLine != null)
                bp.CreditLimit = (double)partner.creditLine;
            if (partner.debitLine != null)
                bp.MaxCommitment = (double)partner.debitLine;
            if (!string.IsNullOrEmpty(partner.dunTerm))
                bp.DunningTerm = partner.dunTerm;
            #endregion

            #region Propierties tab
            bp.set_Properties(1, partner.qryGroup1 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(2, partner.qryGroup2 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(3, partner.qryGroup3 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(4, partner.qryGroup4 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(5, partner.qryGroup5 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(6, partner.qryGroup6 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(7, partner.qryGroup7 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(8, partner.qryGroup8 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(9, partner.qryGroup9 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(10, partner.qryGroup10 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(11, partner.qryGroup11 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(12, partner.qryGroup12 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(13, partner.qryGroup13 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(14, partner.qryGroup14 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(15, partner.qryGroup15 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(16, partner.qryGroup16 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(17, partner.qryGroup17 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(18, partner.qryGroup18 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(19, partner.qryGroup19 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(20, partner.qryGroup20 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(21, partner.qryGroup21 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(22, partner.qryGroup22 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(23, partner.qryGroup23 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(24, partner.qryGroup24 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(25, partner.qryGroup25 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(26, partner.qryGroup26 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(27, partner.qryGroup27 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(28, partner.qryGroup28 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(29, partner.qryGroup29 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(30, partner.qryGroup30 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(31, partner.qryGroup31 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(32, partner.qryGroup32 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(33, partner.qryGroup33 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(34, partner.qryGroup34 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(35, partner.qryGroup35 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(36, partner.qryGroup36 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(37, partner.qryGroup37 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(38, partner.qryGroup38 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(39, partner.qryGroup39 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(40, partner.qryGroup40 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(41, partner.qryGroup41 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(42, partner.qryGroup42 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(43, partner.qryGroup43 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(44, partner.qryGroup44 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(45, partner.qryGroup45 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(46, partner.qryGroup46 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(47, partner.qryGroup47 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(48, partner.qryGroup48 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(49, partner.qryGroup49 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(50, partner.qryGroup50 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(51, partner.qryGroup51 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(52, partner.qryGroup52 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(53, partner.qryGroup53 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(54, partner.qryGroup54 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(55, partner.qryGroup55 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(56, partner.qryGroup56 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(57, partner.qryGroup57 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(58, partner.qryGroup58 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(59, partner.qryGroup59 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(60, partner.qryGroup60 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(61, partner.qryGroup61 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(62, partner.qryGroup62 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(63, partner.qryGroup63 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            bp.set_Properties(64, partner.qryGroup64 ? BoYesNoEnum.tYES : BoYesNoEnum.tNO);
            #endregion

            #region Remarks tab
            if (!string.IsNullOrEmpty(partner.freeText))
                bp.FreeText = partner.freeText;
            #endregion

            #region UDF's
            if (partner.userDefinedFields != null)
                foreach (UserDefinedField item in partner.userDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                bp.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                bp.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                bp.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                bp.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
            #endregion

            if (bp.Update() != 0)
                throw new SAPException(sapConn.company.GetLastErrorCode(), sapConn.company.GetLastErrorDescription());
        }
        #endregion
    }
}
