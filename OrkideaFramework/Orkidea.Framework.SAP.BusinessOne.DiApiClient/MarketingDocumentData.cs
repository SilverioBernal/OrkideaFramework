using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.DiApiClient.SecurityData;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.Administration;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;
using Orkidea.Framework.SAP.BusinessOne.Entities.Inventory;
using Orkidea.Framework.SAP.BusinessOne.Entities.MarketingDocuments;
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
    public class MarketingDocumentData
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
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public MarketingDocumentData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
        }

        public MarketingDocumentData(string connStringName)
        {
            this.dataBase = DatabaseFactory.CreateDatabase(connStringName);
        }
        #endregion

        #region Methods
        public List<MarketingDocument> GetWideList(SapDocumentType mktgDocType, DateTime startDate, DateTime endDate)
        {
            StringBuilder oSQL = new StringBuilder();

            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    break;
                case SapDocumentType.SalesCreditNote:
                    break;
                case SapDocumentType.SalesDelivery:
                    break;
                case SapDocumentType.SalesReturn:
                    break;
                case SapDocumentType.SalesOrder:
                    break;
                case SapDocumentType.PurchaseInvoice:
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    break;
                case SapDocumentType.PurchaseDelivery:
                    break;
                case SapDocumentType.PurchaseReturn:
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("SELECT  T0.DocEntry, DocNum, CardCode, CardName, NumAtCard, T0.DocDate, DocStatus, Canceled, ");
                    oSQL.Append("ItemCode, Quantity, T1.WhsCode, WhsName, Dscription, LineStatus, T0.Doctype, T0.invntsttus, ");
                    oSQL.Append("T1.OpenCreQty, T1.OpenQty, T1.unitMsr, T1.NumPerMsr, T1.TargetType, T1.TrgetEntry, T1.LineNum, ");
                    oSQL.Append("T0.ObjType, T0.UpdateDate ");
                    oSQL.Append("FROM OPOR T0 ");
                    oSQL.Append("INNER JOIN POR1 T1 ");
                    oSQL.Append("ON T0.DocEntry = T1.DocEntry ");
                    oSQL.Append("LEFT JOIN OWHS T2 ");
                    oSQL.Append("ON T1.WhsCode = T2.WhsCode ");
                    oSQL.Append("Where T0.DocDate between @starDate and @endDate");
                    break;
                default:
                    break;
            }

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommand, "starDate", DbType.Date, startDate);
            this.dataBase.AddInParameter(dbCommand, "endDate", DbType.Date, endDate);

            List<MarketingDocument> documentList = new List<MarketingDocument>();
            MarketingDocument document = new MarketingDocument();
            int DocEntryTemporal = 0;
            int contadorLineas = 0;

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    if (document.docEntry == 0 || Convert.ToInt32(this.reader.GetValue(0).ToString()) != DocEntryTemporal)
                    {
                        if (document.docEntry != 0)
                            documentList.Add(document);

                        document = new MarketingDocument();
                        document.docEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                        document.docNum = this.reader.IsDBNull(1) ? 0 : Convert.ToInt32(this.reader.GetValue(1).ToString());
                        document.cardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                        document.cardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                        document.numAtCard = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                        document.docDate = this.reader.IsDBNull(5) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(5).ToString());
                        document.docStatus = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                        document.doctype = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                        document.invntsttus = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                        document.objtype = this.reader.IsDBNull(23) ? "" : this.reader.GetValue(23).ToString();
                        document.updateDate = this.reader.IsDBNull(24) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(24).ToString());

                        if (this.reader.GetValue(7).ToString().Equals("Y"))
                            document.canceled = true;
                        else
                            document.canceled = false;
                        DocEntryTemporal = document.docEntry;
                    }
                    MarketingDocumentLine linea = new MarketingDocumentLine();
                    linea.itemCode = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                    linea.quantity = this.reader.IsDBNull(9) ? 0 : Convert.ToDouble(this.reader.GetValue(9).ToString());
                    linea.whsCode = this.reader.IsDBNull(10) ? "" : this.reader.GetValue(10).ToString();
                    linea.whsName = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    linea.dscription = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    linea.lineStatus = this.reader.IsDBNull(13) ? "" : this.reader.GetValue(13).ToString();
                    linea.openCreQty = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                    linea.openQty = this.reader.IsDBNull(17) ? 0 : Convert.ToDouble(this.reader.GetValue(17).ToString());
                    linea.unitMsr = this.reader.IsDBNull(18) ? "" : this.reader.GetValue(18).ToString();
                    linea.numPerMsr = this.reader.IsDBNull(19) ? 0 : Convert.ToDouble(this.reader.GetValue(19).ToString());
                    linea.targetType = this.reader.IsDBNull(20) ? 0 : Convert.ToInt32(this.reader.GetValue(20).ToString());
                    linea.trgetEntry = this.reader.IsDBNull(21) ? 0 : Convert.ToInt32(this.reader.GetValue(21).ToString());
                    linea.lineNum = this.reader.IsDBNull(22) ? 0 : Convert.ToInt32(this.reader.GetValue(22).ToString());

                    document.lines.Add(linea);
                    contadorLineas++;
                }
                if (document.docEntry != 0)
                    documentList.Add(document);
            }
            return documentList;
        }

        public List<LightMarketingDocument> GetList(SapDocumentType mktgDocType, DateTime startDate, DateTime endDate)
        {
            StringBuilder oSQL = new StringBuilder();
            List<LightMarketingDocument> documentList = new List<LightMarketingDocument>();
            List<LightMarketingDocumentLine> documentsLinesList = new List<LightMarketingDocumentLine>();

            #region Header query
            oSQL.Append("Select a.DocEntry, a.DocNum, a.CardCode, a.CardName, a.DocDate, a.DocDueDate, a.DocStatus ");

            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    oSQL.Append("FROM OINV a ");
                    break;
                case SapDocumentType.SalesCreditNote:
                    oSQL.Append("FROM ORIN a ");
                    break;
                case SapDocumentType.SalesDelivery:
                    oSQL.Append("FROM ODLN a ");
                    break;
                case SapDocumentType.SalesReturn:
                    oSQL.Append("FROM ORDN a ");
                    break;
                case SapDocumentType.SalesOrder:
                    oSQL.Append("FROM ORDR a ");
                    break;
                case SapDocumentType.PurchaseInvoice:
                    oSQL.Append("FROM OPCH a ");
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    oSQL.Append("FROM ORPC a ");
                    break;
                case SapDocumentType.PurchaseDelivery:
                    oSQL.Append("FROM OPDN a ");
                    break;
                case SapDocumentType.PurchaseReturn:
                    oSQL.Append("FROM ORPD a ");
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("FROM OPOR a ");
                    break;
                case SapDocumentType.Quotation:
                    oSQL.Append("FROM OQUT a ");
                    break;
                default:
                    break;
            }

            oSQL.Append("Where a.DocDate between @starDate and @endDate");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommand, "starDate", DbType.Date, startDate);
            this.dataBase.AddInParameter(dbCommand, "endDate", DbType.Date, endDate);
            #endregion

            #region Get Header Data
            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    LightMarketingDocument document = new LightMarketingDocument();
                    document.docEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                    document.docNum = this.reader.IsDBNull(1) ? 0 : Convert.ToInt32(this.reader.GetValue(1).ToString());
                    document.cardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    document.cardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    document.docDate = this.reader.IsDBNull(4) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(4).ToString());
                    document.docDueDate = this.reader.IsDBNull(5) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(5).ToString());
                    document.docStatus = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();

                    documentList.Add(document);
                }
            }
            #endregion

            #region Lines
            //#region Lines Query
            //oSQL.Clear();

            //oSQL.Append("Select a.DocEntry, b.ItemCode, c.ItemName, b.Quantity, b.Price, b.Quantity*b.Price Total");

            //switch (mktgDocType)
            //{
            //    case SapDocumentType.SalesInvoice:
            //        oSQL.Append("From OINV a inner join INV1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesCreditNote:
            //        oSQL.Append("From ORIN a inner join RIN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesDelivery:
            //        oSQL.Append("From ODLN a inner join DLN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesReturn:
            //        oSQL.Append("From ORDN a inner join RDN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesOrder:
            //        oSQL.Append("From ORDR a inner join RDR1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseInvoice:
            //        oSQL.Append("From OPCH a inner join PCH1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseCreditNote:
            //        oSQL.Append("From ORPC a inner join RPC1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseDelivery:
            //        oSQL.Append("From OPDN a inner join PDN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseReturn:
            //        oSQL.Append("From ORPD a inner join RPD1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseOrder:
            //        oSQL.Append("From OPOR a inner join POR1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    default:
            //        break;
            //}

            //oSQL.Append("Where a.DocDate between @starDate and @endDate");

            //DbCommand dbCommandLines  = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            //this.dataBase.AddInParameter(dbCommandLines, "starDate", DbType.Date, startDate);
            //this.dataBase.AddInParameter(dbCommandLines, "endDate", DbType.Date, endDate);

            //#endregion

            //#region Get Lines Data
            //using (this.reader = this.dataBase.ExecuteReader(dbCommandLines))
            //{
            //    while (this.reader.Read())
            //    {
            //        LightMarketingDocumentLine documentLine = new LightMarketingDocumentLine();
            //        documentLine.docEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
            //        documentLine.itemCode = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
            //        documentLine.itemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
            //        documentLine.quantity = this.reader.IsDBNull(3) ? 0 : Convert.ToDouble(this.reader.GetValue(3).ToString());
            //        documentLine.price = this.reader.IsDBNull(4) ? 0 : Convert.ToDouble(this.reader.GetValue(4).ToString());
            //        documentLine.total = this.reader.IsDBNull(5) ? 0 : Convert.ToDouble(this.reader.GetValue(5).ToString());

            //        documentsLinesList.Add(documentLine);
            //    }
            //}
            //#endregion

            //#region Put each line into respective order
            //foreach (LightMarketingDocument order in documentList)            
            //    order.lines.AddRange(documentsLinesList.Where(x => x.docEntry.Equals(order.docEntry)).ToList());            
            //#endregion
            #endregion

            return documentList;
        }

        public List<LightMarketingDocument> GetList(SapDocumentType mktgDocType, DateTime startDate, DateTime endDate, string cardCode)
        {
            StringBuilder oSQL = new StringBuilder();
            List<LightMarketingDocument> documentList = new List<LightMarketingDocument>();
            List<LightMarketingDocumentLine> documentsLinesList = new List<LightMarketingDocumentLine>();

            #region Header query
            oSQL.Append("Select a.DocEntry, a.DocNum, a.CardCode, a.CardName, a.DocDate, a.DocDueDate, a.DocStatus ");

            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    oSQL.Append("FROM OINV a ");
                    break;
                case SapDocumentType.SalesCreditNote:
                    oSQL.Append("FROM ORIN a ");
                    break;
                case SapDocumentType.SalesDelivery:
                    oSQL.Append("FROM ODLN a ");
                    break;
                case SapDocumentType.SalesReturn:
                    oSQL.Append("FROM ORDN a ");
                    break;
                case SapDocumentType.SalesOrder:
                    oSQL.Append("FROM ORDR a ");
                    break;
                case SapDocumentType.PurchaseInvoice:
                    oSQL.Append("FROM OPCH a ");
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    oSQL.Append("FROM ORPC a ");
                    break;
                case SapDocumentType.PurchaseDelivery:
                    oSQL.Append("FROM OPDN a ");
                    break;
                case SapDocumentType.PurchaseReturn:
                    oSQL.Append("FROM ORPD a ");
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("FROM OPOR a ");
                    break;
                case SapDocumentType.Quotation:
                    oSQL.Append("FROM OQUT a ");
                    break;
                default:
                    break;
            }

            oSQL.Append("Where a.cardCode = @cardCode ");
            oSQL.Append("and a.DocDate between @starDate and @endDate");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommand, "cardCode", DbType.String, cardCode);
            this.dataBase.AddInParameter(dbCommand, "starDate", DbType.Date, startDate);
            this.dataBase.AddInParameter(dbCommand, "endDate", DbType.Date, endDate);
            #endregion

            #region Get Header Data
            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    LightMarketingDocument document = new LightMarketingDocument();
                    document.docEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                    document.docNum = this.reader.IsDBNull(1) ? 0 : Convert.ToInt32(this.reader.GetValue(1).ToString());
                    document.cardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    document.cardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    document.docDate = this.reader.IsDBNull(4) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(4).ToString());
                    document.docDueDate = this.reader.IsDBNull(5) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(5).ToString());
                    document.docStatus = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();

                    documentList.Add(document);
                }
            }
            #endregion

            #region Lines
            //#region Lines Query
            //oSQL.Clear();

            //oSQL.Append("Select a.DocEntry, b.ItemCode, c.ItemName, b.Quantity, b.Price, b.Quantity*b.Price Total");

            //switch (mktgDocType)
            //{
            //    case SapDocumentType.SalesInvoice:
            //        oSQL.Append("From OINV a inner join INV1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesCreditNote:
            //        oSQL.Append("From ORIN a inner join RIN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesDelivery:
            //        oSQL.Append("From ODLN a inner join DLN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesReturn:
            //        oSQL.Append("From ORDN a inner join RDN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.SalesOrder:
            //        oSQL.Append("From ORDR a inner join RDR1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseInvoice:
            //        oSQL.Append("From OPCH a inner join PCH1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseCreditNote:
            //        oSQL.Append("From ORPC a inner join RPC1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseDelivery:
            //        oSQL.Append("From OPDN a inner join PDN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseReturn:
            //        oSQL.Append("From ORPD a inner join RPD1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    case SapDocumentType.PurchaseOrder:
            //        oSQL.Append("From OPOR a inner join POR1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
            //        break;
            //    default:
            //        break;
            //}

            //oSQL.Append("Where a.cardCode = @cardCode ");
            //oSQL.Append("and a.DocDate between @starDate and @endDate");

            //DbCommand dbCommandLines = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            //this.dataBase.AddInParameter(dbCommandLines, "cardCode", DbType.String, cardCode);
            //this.dataBase.AddInParameter(dbCommandLines, "starDate", DbType.Date, startDate);
            //this.dataBase.AddInParameter(dbCommandLines, "endDate", DbType.Date, endDate);

            //#endregion

            //#region Get Lines Data
            //using (this.reader = this.dataBase.ExecuteReader(dbCommandLines))
            //{
            //    while (this.reader.Read())
            //    {
            //        LightMarketingDocumentLine documentLine = new LightMarketingDocumentLine();
            //        documentLine.docEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
            //        documentLine.itemCode = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
            //        documentLine.itemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
            //        documentLine.quantity = this.reader.IsDBNull(3) ? 0 : Convert.ToDouble(this.reader.GetValue(3).ToString());
            //        documentLine.price = this.reader.IsDBNull(4) ? 0 : Convert.ToDouble(this.reader.GetValue(4).ToString());
            //        documentLine.total = this.reader.IsDBNull(5) ? 0 : Convert.ToDouble(this.reader.GetValue(5).ToString());

            //        documentsLinesList.Add(documentLine);
            //    }
            //}
            //#endregion

            //#region Put each line into respective order
            //foreach (LightMarketingDocument order in documentList)
            //    order.lines.AddRange(documentsLinesList.Where(x => x.docEntry.Equals(order.docEntry)).ToList());
            //#endregion
            #endregion

            return documentList;
        }

        /// <summary>
        /// Consulta un socio de negocios en SAP Business One
        /// </summary>
        /// <param name="cardCode">Codigo de socio de negocio</param>
        /// <returns>Socio con la información</returns>
        public LightMarketingDocument GetSingle(SapDocumentType mktgDocType, string docNum)
        {
            StringBuilder oSQL = new StringBuilder();
            LightMarketingDocument document = new LightMarketingDocument();

            #region Header query
            oSQL.Append("Select a.DocEntry, a.DocNum, a.CardCode, a.CardName, a.DocDate, a.DocDueDate, a.DocStatus ");

            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    oSQL.Append("FROM OINV a ");
                    break;
                case SapDocumentType.SalesCreditNote:
                    oSQL.Append("FROM ORIN a ");
                    break;
                case SapDocumentType.SalesDelivery:
                    oSQL.Append("FROM ODLN a ");
                    break;
                case SapDocumentType.SalesReturn:
                    oSQL.Append("FROM ORDN a ");
                    break;
                case SapDocumentType.SalesOrder:
                    oSQL.Append("FROM ORDR a ");
                    break;
                case SapDocumentType.PurchaseInvoice:
                    oSQL.Append("FROM OPCH a ");
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    oSQL.Append("FROM ORPC a ");
                    break;
                case SapDocumentType.PurchaseDelivery:
                    oSQL.Append("FROM OPDN a ");
                    break;
                case SapDocumentType.PurchaseReturn:
                    oSQL.Append("FROM ORPD a ");
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("FROM OPOR a ");
                    break;
                case SapDocumentType.Quotation:
                    oSQL.Append("FROM OQUT a ");
                    break;
                default:
                    break;
            }

            oSQL.Append("Where a.DocNum = @docNum");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommand, "docNum", DbType.Int32, docNum);
            #endregion

            #region Get Header Data
            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {

                    document.docEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                    document.docNum = this.reader.IsDBNull(1) ? 0 : Convert.ToInt32(this.reader.GetValue(1).ToString());
                    document.cardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    document.cardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                    document.docDate = this.reader.IsDBNull(4) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(4).ToString());
                    document.docDueDate = this.reader.IsDBNull(5) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(5).ToString());
                    document.docStatus = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                }
            }
            #endregion

            #region Lines

            #region Lines Query
            oSQL.Clear();

            oSQL.Append("Select a.DocEntry, b.ItemCode, c.ItemName, b.Quantity, b.Price, b.Quantity*b.Price Total ");

            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    oSQL.Append("From OINV a inner join INV1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.SalesCreditNote:
                    oSQL.Append("From ORIN a inner join RIN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.SalesDelivery:
                    oSQL.Append("From ODLN a inner join DLN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.SalesReturn:
                    oSQL.Append("From ORDN a inner join RDN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.SalesOrder:
                    oSQL.Append("From ORDR a inner join RDR1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.PurchaseInvoice:
                    oSQL.Append("From OPCH a inner join PCH1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    oSQL.Append("From ORPC a inner join RPC1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.PurchaseDelivery:
                    oSQL.Append("From OPDN a inner join PDN1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.PurchaseReturn:
                    oSQL.Append("From ORPD a inner join RPD1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("From OPOR a inner join POR1 b on a.DocEntry = b.DocEntry inner join OITM c on b.ItemCode = c.ItemCode ");
                    break;
                default:
                    break;
            }

            oSQL.Append("Where a.DocNum = @docNum");

            DbCommand dbCommandLines = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommandLines, "docNum", DbType.Int32, docNum);

            #endregion

            #region Get Lines Data
            using (this.reader = this.dataBase.ExecuteReader(dbCommandLines))
            {
                while (this.reader.Read())
                {
                    LightMarketingDocumentLine documentLine = new LightMarketingDocumentLine();
                    documentLine.docEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                    documentLine.itemCode = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    documentLine.itemName = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    documentLine.quantity = this.reader.IsDBNull(3) ? 0 : Convert.ToDouble(this.reader.GetValue(3).ToString());
                    documentLine.price = this.reader.IsDBNull(4) ? 0 : Convert.ToDouble(this.reader.GetValue(4).ToString());
                    documentLine.total = this.reader.IsDBNull(5) ? 0 : Convert.ToDouble(this.reader.GetValue(5).ToString());

                    document.lines.Add(documentLine);
                }
            }
            #endregion

            #endregion
            return document;
        }

        /// <summary>
        /// Método para la creacion de socios de negocio en SAP
        /// </summary>
        /// <param name="document"></param>
        public MarketingDocument Add(SapDocumentType mktgDocType, MarketingDocument document)
        {
            Documents doc = null;

            #region Document definition
            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oInvoices);
                    break;
                case SapDocumentType.SalesCreditNote:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oCreditNotes);
                    break;
                case SapDocumentType.SalesDelivery:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    break;
                case SapDocumentType.SalesReturn:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oReturns);
                    break;
                case SapDocumentType.SalesOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oOrders);
                    break;
                case SapDocumentType.PurchaseInvoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseInvoices);
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseCreditNotes);
                    break;
                case SapDocumentType.PurchaseDelivery:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes);
                    break;
                case SapDocumentType.PurchaseReturn:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseReturns);
                    break;
                case SapDocumentType.PurchaseOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                    break;
                case SapDocumentType.Quotation:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oQuotations);
                    break;
                default:
                    break;
            }
            #endregion

            #region Document header
            doc.Series = document.serie;
            doc.CardCode = document.cardCode;
            doc.DocDate = document.docDate;
            doc.DocDueDate = document.docDate;
            doc.TaxDate = document.docDate;

            if (!string.IsNullOrEmpty(document.numAtCard))
                doc.NumAtCard = document.numAtCard;

            if (!string.IsNullOrEmpty(document.shipToCode))
                doc.ShipToCode = document.shipToCode;

            if (!string.IsNullOrEmpty(document.payToCode))
                doc.PayToCode = document.payToCode;

            if (document.groupNum != null)
                doc.GroupNumber = (int)document.groupNum;

            if (!string.IsNullOrEmpty(document.comments))
                doc.Comments = document.comments;

            if (document.slpCode != null)
                doc.SalesPersonCode = (int)document.slpCode;

            #region UDF's
            foreach (UserDefinedField item in document.userDefinedFields)
            {
                if (!string.IsNullOrEmpty(item.value))
                    switch (item.type)
                    {
                        case UdfType.Alphanumeric:
                            doc.UserFields.Fields.Item(item.name).Value = item.value;
                            break;
                        case UdfType.Integer:
                            doc.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                            break;
                        case UdfType.Double:
                            doc.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                            break;
                        case UdfType.Datetime:
                            doc.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                            break;
                        case UdfType.Price:
                            doc.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                            break;
                        case UdfType.Text:
                            doc.UserFields.Fields.Item(item.name).Value = item.value;
                            break;
                        default:
                            break;
                    }
            }
            #endregion

            #endregion

            #region Document lines
            foreach (MarketingDocumentLine line in document.lines)
            {
                if (line.baseType != 202)
                    doc.Lines.ItemCode = line.itemCode;

                doc.Lines.Quantity = line.quantity;
                doc.Lines.WarehouseCode = line.whsCode;

                if (line.price != 0)//unit price
                    doc.Lines.UnitPrice = line.price;

                if (!string.IsNullOrEmpty(line.unitMsr))
                    doc.Lines.MeasureUnit = line.unitMsr;

                if (!string.IsNullOrEmpty(line.taxCode))
                    doc.Lines.TaxCode = line.taxCode;

                if (!string.IsNullOrEmpty(line.ocrCode))
                    doc.Lines.CostingCode = line.ocrCode;

                if (line.numPerMsr != 0)
                    doc.Lines.UnitsOfMeasurment = line.numPerMsr;

                if (line.baseEntry != 0)
                {
                    doc.Lines.BaseEntry = line.baseEntry;
                    doc.Lines.BaseLine = line.baseLine;
                    doc.Lines.BaseType = line.baseType;
                }

                #region UDF's
                foreach (UserDefinedField item in line.userDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                doc.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                doc.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
                #endregion

                #region Batchs
                if (line.batchNumbers != null)
                    foreach (BatchNumber batchNumber in line.batchNumbers)
                    {
                        doc.Lines.BatchNumbers.BatchNumber = batchNumber.DistNumber;
                        doc.Lines.BatchNumbers.Quantity = batchNumber.Quantity;
                        doc.Lines.BatchNumbers.ExpiryDate = batchNumber.ExpDate;
                        doc.Lines.BatchNumbers.Add();
                    }
                #endregion

                #region Series
                if (line.serialNumbers != null)
                    foreach (SerialNumber serie in line.serialNumbers)
                    {

                        doc.Lines.SerialNumbers.InternalSerialNumber = serie.DisNumber;
                        doc.Lines.SerialNumbers.Add();
                    }
                doc.Lines.Add();
                #endregion
            }
            #endregion

            if (doc.Add() != 0)
            {
                throw new SAPException(SAPConnection.conn.company.GetLastErrorCode(), SAPConnection.conn.company.GetLastErrorDescription());
            }
            document.docEntry = Convert.ToInt32(SAPConnection.conn.company.GetNewObjectKey());
            return document;
        }

        public MarketingDocument Add(SapDocumentType mktgDocType, MarketingDocument document, SAPConnection sapConn)
        {
            Documents doc = null;

            #region Document definition
            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oInvoices);
                    break;
                case SapDocumentType.SalesCreditNote:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oCreditNotes);
                    break;
                case SapDocumentType.SalesDelivery:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    break;
                case SapDocumentType.SalesReturn:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oReturns);
                    break;
                case SapDocumentType.SalesOrder:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oOrders);
                    break;
                case SapDocumentType.PurchaseInvoice:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oPurchaseInvoices);
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oPurchaseCreditNotes);
                    break;
                case SapDocumentType.PurchaseDelivery:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes);
                    break;
                case SapDocumentType.PurchaseReturn:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oPurchaseReturns);
                    break;
                case SapDocumentType.PurchaseOrder:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                    break;
                case SapDocumentType.Quotation:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oQuotations);
                    break;
                default:
                    break;
            }
            #endregion

            #region Document header
            doc.Series = document.serie;
            doc.CardCode = document.cardCode;
            doc.DocDate = document.docDate;
            doc.DocDueDate = document.docDueDate;
            doc.TaxDate = document.taxDate;

            if (!string.IsNullOrEmpty(document.numAtCard))
                doc.NumAtCard = document.numAtCard;

            if (!string.IsNullOrEmpty(document.shipToCode))
                doc.ShipToCode = document.shipToCode;

            if (!string.IsNullOrEmpty(document.payToCode))
                doc.PayToCode = document.payToCode;

            if (document.groupNum != null)
                doc.GroupNumber = (int)document.groupNum;

            if (!string.IsNullOrEmpty(document.comments))
                doc.Comments = document.comments;

            if (document.slpCode != null)
                doc.SalesPersonCode = (int)document.slpCode;

            #region UDF's
            foreach (UserDefinedField item in document.userDefinedFields)
            {
                if (!string.IsNullOrEmpty(item.value))
                    switch (item.type)
                    {
                        case UdfType.Alphanumeric:
                            doc.UserFields.Fields.Item(item.name).Value = item.value;
                            break;
                        case UdfType.Integer:
                            doc.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                            break;
                        case UdfType.Double:
                            doc.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                            break;
                        case UdfType.Datetime:
                            doc.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                            break;
                        case UdfType.Price:
                            doc.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                            break;
                        case UdfType.Text:
                            doc.UserFields.Fields.Item(item.name).Value = item.value;
                            break;
                        default:
                            break;
                    }
            }
            #endregion

            #endregion

            #region Document lines
            foreach (MarketingDocumentLine line in document.lines)
            {
                if (line.baseType != 202)
                    doc.Lines.ItemCode = line.itemCode;

                doc.Lines.Quantity = line.quantity;
                doc.Lines.WarehouseCode = line.whsCode;
                if (line.price != 0)
                    doc.Lines.UnitPrice = line.price;

                if (!string.IsNullOrEmpty(line.unitMsr))
                    doc.Lines.MeasureUnit = line.unitMsr;

                if (!string.IsNullOrEmpty(line.taxCode))
                    doc.Lines.TaxCode = line.taxCode;

                if (!string.IsNullOrEmpty(line.ocrCode))
                    doc.Lines.CostingCode = line.ocrCode;

                if (line.numPerMsr != 0)
                    doc.Lines.UnitsOfMeasurment = line.numPerMsr;

                if (line.baseEntry != 0)
                {
                    doc.Lines.BaseEntry = line.baseEntry;
                    doc.Lines.BaseLine = line.baseLine;
                    doc.Lines.BaseType = line.baseType;
                }

                #region UDF's
                foreach (UserDefinedField item in line.userDefinedFields)
                {
                    if (!string.IsNullOrEmpty(item.value))
                        switch (item.type)
                        {
                            case UdfType.Alphanumeric:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            case UdfType.Integer:
                                doc.UserFields.Fields.Item(item.name).Value = int.Parse(item.value);
                                break;
                            case UdfType.Double:
                                doc.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Datetime:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = DateTime.Parse(item.value);
                                break;
                            case UdfType.Price:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
                                break;
                            case UdfType.Text:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = item.value;
                                break;
                            default:
                                break;
                        }
                }
                #endregion

                #region Batchs
                if (line.batchNumbers != null)
                    foreach (BatchNumber batchNumber in line.batchNumbers)
                    {
                        doc.Lines.BatchNumbers.BatchNumber = batchNumber.DistNumber;
                        doc.Lines.BatchNumbers.Quantity = batchNumber.Quantity;
                        doc.Lines.BatchNumbers.ExpiryDate = batchNumber.ExpDate;
                        doc.Lines.BatchNumbers.Add();
                    }
                #endregion

                #region Series
                if (line.serialNumbers != null)
                    foreach (SerialNumber serie in line.serialNumbers)
                    {

                        doc.Lines.SerialNumbers.InternalSerialNumber = serie.DisNumber;
                        doc.Lines.SerialNumbers.Add();
                    }
                doc.Lines.Add();
                #endregion
            }
            #endregion

            if (doc.Add() != 0)
            {
                throw new SAPException(sapConn.company.GetLastErrorCode(), sapConn.company.GetLastErrorDescription());
            }
            document.docEntry = Convert.ToInt32(sapConn.company.GetNewObjectKey());
            return document;
        }

        public void Cancel(SapDocumentType mktgDocType, int docEntry)
        {
            Documents doc = null;

            #region Document definition
            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oInvoices);
                    break;
                case SapDocumentType.SalesCreditNote:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oCreditNotes);
                    break;
                case SapDocumentType.SalesDelivery:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    break;
                case SapDocumentType.SalesReturn:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oReturns);
                    break;
                case SapDocumentType.SalesOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oOrders);
                    break;
                case SapDocumentType.PurchaseInvoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseInvoices);
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseCreditNotes);
                    break;
                case SapDocumentType.PurchaseDelivery:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes);
                    break;
                case SapDocumentType.PurchaseReturn:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseReturns);
                    break;
                case SapDocumentType.PurchaseOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                    break;
                case SapDocumentType.Quotation:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oQuotations);
                    break;
                default:
                    break;
            }
            #endregion


            doc.GetByKey(docEntry);

            if (doc.DocumentStatus == BoStatus.bost_Close)
                throw new SAPException(-8900, "El pedido está cerrado");

            if (doc.Cancel() != 0)
                throw new SAPException(SAPConnection.conn.company.GetLastErrorCode(), SAPConnection.conn.company.GetLastErrorDescription());
        }

        public void Cancel(SapDocumentType mktgDocType, int docEntry, SAPConnection sapConn)
        {
            Documents doc = null;

            #region Document definition
            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oInvoices);
                    break;
                case SapDocumentType.SalesCreditNote:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oCreditNotes);
                    break;
                case SapDocumentType.SalesDelivery:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    break;
                case SapDocumentType.SalesReturn:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oReturns);
                    break;
                case SapDocumentType.SalesOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oOrders);
                    break;
                case SapDocumentType.PurchaseInvoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseInvoices);
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseCreditNotes);
                    break;
                case SapDocumentType.PurchaseDelivery:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseDeliveryNotes);
                    break;
                case SapDocumentType.PurchaseReturn:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseReturns);
                    break;
                case SapDocumentType.PurchaseOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                    break;
                case SapDocumentType.Quotation:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oQuotations);
                    break;
                default:
                    break;
            }
            #endregion


            doc.GetByKey(docEntry);

            if (doc.DocumentStatus == BoStatus.bost_Close)
                throw new SAPException(-8900, "El pedido está cerrado");

            if (doc.Cancel() != 0)
                throw new SAPException(sapConn.company.GetLastErrorCode(), SAPConnection.conn.company.GetLastErrorDescription());
        }

        public int GetDocNum(SapDocumentType mktgDocType, int docEntry)
        {
            StringBuilder oSQL = new StringBuilder();
            int docNum = -1;
            #region Header query
            oSQL.Append("Select DocNum ");
            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    oSQL.Append("FROM OINV a ");
                    break;
                case SapDocumentType.SalesCreditNote:
                    oSQL.Append("FROM ORIN a ");
                    break;
                case SapDocumentType.SalesDelivery:
                    oSQL.Append("FROM ODLN a ");
                    break;
                case SapDocumentType.SalesReturn:
                    oSQL.Append("FROM ORDN a ");
                    break;
                case SapDocumentType.SalesOrder:
                    oSQL.Append("FROM ORDR a ");
                    break;
                case SapDocumentType.PurchaseInvoice:
                    oSQL.Append("FROM OPCH a ");
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    oSQL.Append("FROM ORPC a ");
                    break;
                case SapDocumentType.PurchaseDelivery:
                    oSQL.Append("FROM OPDN a ");
                    break;
                case SapDocumentType.PurchaseReturn:
                    oSQL.Append("FROM ORPD a ");
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("FROM OPOR a ");
                    break;
                case SapDocumentType.Quotation:
                    oSQL.Append("FROM OQUT a ");
                    break;
                case SapDocumentType.Draft:
                    oSQL.Append("FROM ODRF a ");
                    break;
                default:
                    break;
            }

            oSQL.Append("Where docEntry = @docEntry");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommand, "docEntry", DbType.Int32, docEntry);
            #endregion

            #region Get Data
            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    docNum = this.reader.IsDBNull(0) ? -1 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                }
            }
            #endregion

            if (docNum < 0)
                throw new SAPException(9999, "No se encuentra el documento especificado");

            return docNum;
        }

        public Dictionary<int, int> GetDocNum(SapDocumentType mktgDocType, List<int> docEntry)
        {
            StringBuilder oSQL = new StringBuilder();
            
            #region Header query
            oSQL.Append("Select DocEntry, DocNum ");
            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    oSQL.Append("FROM OINV a ");
                    break;
                case SapDocumentType.SalesCreditNote:
                    oSQL.Append("FROM ORIN a ");
                    break;
                case SapDocumentType.SalesDelivery:
                    oSQL.Append("FROM ODLN a ");
                    break;
                case SapDocumentType.SalesReturn:
                    oSQL.Append("FROM ORDN a ");
                    break;
                case SapDocumentType.SalesOrder:
                    oSQL.Append("FROM ORDR a ");
                    break;
                case SapDocumentType.PurchaseInvoice:
                    oSQL.Append("FROM OPCH a ");
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    oSQL.Append("FROM ORPC a ");
                    break;
                case SapDocumentType.PurchaseDelivery:
                    oSQL.Append("FROM OPDN a ");
                    break;
                case SapDocumentType.PurchaseReturn:
                    oSQL.Append("FROM ORPD a ");
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("FROM OPOR a ");
                    break;
                case SapDocumentType.Quotation:
                    oSQL.Append("FROM OQUT a ");
                    break;
                case SapDocumentType.Draft:
                    oSQL.Append("FROM ODRF a ");
                    break;
                default:
                    break;
            }

            oSQL.Append("Where docEntry in (");

            for (int i = 0; i < docEntry.Count; i++)
            {
                if (i.Equals((docEntry.Count - 1)))
                    oSQL.Append(string.Format("{0}) ", docEntry[i].ToString()));
                else
                    oSQL.Append(string.Format("{0}, ", docEntry[i].ToString()));
            }

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            #endregion

            #region Get Data
            Dictionary<int, int> docs = new Dictionary<int, int>();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    docs.Add(this.reader.IsDBNull(0) ? -1 : Convert.ToInt32(this.reader.GetValue(0).ToString()), this.reader.IsDBNull(0) ? -1 : Convert.ToInt32(this.reader.GetValue(1).ToString()));                    
                }
            }
            #endregion

            if (docs.Count == 0)
                throw new SAPException(9999, "No se encuentra el documento especificado");

            return docs;
        }
        #endregion
    }
}
