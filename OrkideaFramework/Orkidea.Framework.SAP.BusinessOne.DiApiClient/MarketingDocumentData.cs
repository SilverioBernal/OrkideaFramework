using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.DiApiClient.SecurityData;
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
        public MarketingDocumentData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
        }
        #endregion

        #region Métodos
        public List<MarketingDocument> GetList(MktgDocType mktgDocType, DateTime startDate, DateTime endDate)
        {
            StringBuilder oSQL = new StringBuilder();

            switch (mktgDocType)
            {
                case MktgDocType.PurchaseOrder:
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
                case MktgDocType.Invoice:
                    break;
                case MktgDocType.Delivery:
                    break;
                case MktgDocType.CreditNote:
                    break;
                case MktgDocType.DebitNote:
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
                    if (document.DocEntry == 0 || Convert.ToInt32(this.reader.GetValue(0).ToString()) != DocEntryTemporal)
                    {
                        if (document.DocEntry != 0)
                            documentList.Add(document);

                        document = new MarketingDocument();
                        document.DocEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                        document.DocNum = this.reader.IsDBNull(1) ? 0 : Convert.ToInt32(this.reader.GetValue(1).ToString());
                        document.CardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                        document.CardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                        document.NumAtCard = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                        document.DocDate = this.reader.IsDBNull(5) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(5).ToString());
                        document.DocStatus = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                        document.Doctype = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                        document.invntsttus = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                        document.Objtype = this.reader.IsDBNull(23) ? "" : this.reader.GetValue(23).ToString();
                        document.UpdateDate = this.reader.IsDBNull(24) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(24).ToString());

                        if (this.reader.GetValue(7).ToString().Equals("Y"))
                            document.Canceled = true;
                        else
                            document.Canceled = false;
                        DocEntryTemporal = document.DocEntry;
                    }
                    MarketingDocumentLine linea = new MarketingDocumentLine();
                    linea.ItemCode = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                    linea.Quantity = this.reader.IsDBNull(9) ? 0 : Convert.ToDouble(this.reader.GetValue(9).ToString());
                    linea.WhsCode = this.reader.IsDBNull(10) ? "" : this.reader.GetValue(10).ToString();
                    linea.WhsName = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                    linea.Dscription = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                    linea.LineStatus = this.reader.IsDBNull(13) ? "" : this.reader.GetValue(13).ToString();
                    linea.OpenCreQty = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                    linea.OpenQty = this.reader.IsDBNull(17) ? 0 : Convert.ToDouble(this.reader.GetValue(17).ToString());
                    linea.unitMsr = this.reader.IsDBNull(18) ? "" : this.reader.GetValue(18).ToString();
                    linea.NumPerMsr = this.reader.IsDBNull(19) ? 0 : Convert.ToDouble(this.reader.GetValue(19).ToString());
                    linea.TargetType = this.reader.IsDBNull(20) ? 0 : Convert.ToInt32(this.reader.GetValue(20).ToString());
                    linea.TrgetEntry = this.reader.IsDBNull(21) ? 0 : Convert.ToInt32(this.reader.GetValue(21).ToString());
                    linea.LineNum = this.reader.IsDBNull(22) ? 0 : Convert.ToInt32(this.reader.GetValue(22).ToString());

                    document.lines.Add(linea);
                    contadorLineas++;
                }
                if (document.DocEntry != 0)
                    documentList.Add(document);
            }
            return documentList;
        }

        /// <summary>
        /// Consulta un socio de negocios en SAP Business One
        /// </summary>
        /// <param name="cardCode">Codigo de socio de negocio</param>
        /// <returns>Socio con la información</returns>
        public MarketingDocument GetSingle(MktgDocType mktgDocType, string docNum)
        {
            StringBuilder oSQL = new StringBuilder();
            MarketingDocument document = new MarketingDocument();

            switch (mktgDocType)
            {
                case MktgDocType.PurchaseOrder:
                    oSQL.Append("SELECT  T0.DocEntry, DocNum, CardCode, CardName, NumAtCard, T0.DocDate, DocStatus, Canceled, ");
                    oSQL.Append("ItemCode, Quantity, T1.WhsCode, WhsName, Dscription, LineStatus, T0.Doctype, T0.invntsttus, ");
                    oSQL.Append("T1.OpenCreQty, T1.OpenQty, T1.unitMsr, T1.NumPerMsr, T1.TargetType, T1.TrgetEntry, T1.LineNum, T0.ObjType, T0.UpdateDate ");
                    oSQL.Append("FROM OPOR T0 ");
                    oSQL.Append("INNER JOIN POR1 T1 ");
                    oSQL.Append("ON T0.DocEntry = T1.DocEntry ");
                    oSQL.Append("AND T0.DocEntry = @DocEntry ");
                    oSQL.Append("LEFT JOIN OWHS T2 ");
                    oSQL.Append("ON T1.WhsCode = T2.WhsCode ");

                    DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
                    this.dataBase.AddInParameter(dbCommand, "DocEntry", DbType.Int32, docNum);

                    using (this.reader = this.dataBase.ExecuteReader(dbCommand))
                    {
                        while (this.reader.Read())
                        {
                            if (document.DocEntry == 0)
                            {
                                document.DocEntry = this.reader.IsDBNull(0) ? 0 : Convert.ToInt32(this.reader.GetValue(0).ToString());
                                document.DocNum = this.reader.IsDBNull(1) ? 0 : Convert.ToInt32(this.reader.GetValue(1).ToString());
                                document.CardCode = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                                document.CardName = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();
                                document.NumAtCard = this.reader.IsDBNull(4) ? "" : this.reader.GetValue(4).ToString();
                                document.DocDate = this.reader.IsDBNull(5) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(5).ToString());
                                document.DocStatus = this.reader.IsDBNull(6) ? "" : this.reader.GetValue(6).ToString();
                                document.Doctype = this.reader.IsDBNull(14) ? "" : this.reader.GetValue(14).ToString();
                                document.invntsttus = this.reader.IsDBNull(15) ? "" : this.reader.GetValue(15).ToString();
                                document.Objtype = this.reader.IsDBNull(23) ? "" : this.reader.GetValue(23).ToString();
                                document.UpdateDate = this.reader.IsDBNull(24) ? DateTime.Now : Convert.ToDateTime(this.reader.GetValue(24).ToString());

                                if (this.reader.GetValue(7).ToString().Equals("Y"))
                                    document.Canceled = true;
                                else
                                    document.Canceled = false;
                            }
                            MarketingDocumentLine line = new MarketingDocumentLine();
                            line.ItemCode = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                            line.Quantity = this.reader.IsDBNull(9) ? 0 : Convert.ToDouble(this.reader.GetValue(9).ToString());
                            line.WhsCode = this.reader.IsDBNull(10) ? "" : this.reader.GetValue(10).ToString();
                            line.WhsName = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                            line.Dscription = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                            line.LineStatus = this.reader.IsDBNull(13) ? "" : this.reader.GetValue(13).ToString();
                            line.OpenCreQty = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                            line.OpenQty = this.reader.IsDBNull(17) ? 0 : Convert.ToDouble(this.reader.GetValue(17).ToString());
                            line.unitMsr = this.reader.IsDBNull(18) ? "" : this.reader.GetValue(18).ToString();
                            line.NumPerMsr = this.reader.IsDBNull(19) ? 0 : Convert.ToDouble(this.reader.GetValue(19).ToString());
                            line.TargetType = this.reader.IsDBNull(20) ? 0 : Convert.ToInt32(this.reader.GetValue(20).ToString());
                            line.TrgetEntry = this.reader.IsDBNull(21) ? 0 : Convert.ToInt32(this.reader.GetValue(21).ToString());
                            line.LineNum = this.reader.IsDBNull(22) ? 0 : Convert.ToInt32(this.reader.GetValue(22).ToString());

                            document.lines.Add(line);
                        }
                    }
                    break;
                case MktgDocType.Invoice:
                    break;
                case MktgDocType.Delivery:
                    break;
                case MktgDocType.CreditNote:
                    break;
                case MktgDocType.DebitNote:
                    break;
                default:
                    break;
            }

            return document;
        }

        /// <summary>
        /// Método para la creacion de socios de negocio en SAP
        /// </summary>
        /// <param name="document"></param>
        public void Add(MktgDocType mktgDocType, MarketingDocument document)
        {
            Documents doc = null;

            #region Document definition
            switch (mktgDocType)
            {
                case MktgDocType.PurchaseOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                    break;
                case MktgDocType.Invoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oInvoices);
                    break;
                case MktgDocType.Delivery:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    break;
                case MktgDocType.CreditNote:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oCreditNotes);
                    break;
            }
            #endregion

            #region Document header
            doc.DocDate = document.DocDate;
            doc.DocDueDate = document.DocDate;
            doc.TaxDate = document.DocDate;
            doc.CardCode = document.CardCode;

            if (!string.IsNullOrEmpty(document.NumAtCard))
                doc.NumAtCard = document.NumAtCard;

            #region Header UDF
            foreach (UserDefinedField udf in document.UserDefinedFields)
            {
                if (udf.type == UdfType.Alphanumeric)
                    doc.UserFields.Fields.Item(udf.name).Value = udf.value;

                if (udf.type == UdfType.Numeric)
                    doc.UserFields.Fields.Item(udf.name).Value = decimal.Parse(udf.value);

                if (udf.type == UdfType.Datetime)
                    doc.UserFields.Fields.Item(udf.name).Value = DateTime.Parse(udf.value);
            }
            #endregion

            #endregion

            #region Document lines
            foreach (MarketingDocumentLine line in document.lines)
            {
                if (line.BaseType != 202)
                    doc.Lines.ItemCode = line.ItemCode;

                doc.Lines.Quantity = line.Quantity;
                doc.Lines.WarehouseCode = line.WhsCode;

                if (!string.IsNullOrEmpty(line.unitMsr))
                    doc.Lines.MeasureUnit = line.unitMsr;

                if (line.NumPerMsr != 0)
                    doc.Lines.UnitsOfMeasurment = line.NumPerMsr;

                if (line.BaseEntry != 0)
                {
                    doc.Lines.BaseEntry = line.BaseEntry;
                    doc.Lines.BaseLine = line.BaseLine;
                    doc.Lines.BaseType = line.BaseType;
                }

                #region Line UDF
                foreach (UserDefinedField udf in line.UserDefinedFields)
                {
                    if (udf.type == UdfType.Alphanumeric)
                        doc.Lines.UserFields.Fields.Item(udf.name).Value = udf.value;

                    if (udf.type == UdfType.Numeric)
                        doc.Lines.UserFields.Fields.Item(udf.name).Value = decimal.Parse(udf.value);

                    if (udf.type == UdfType.Datetime)
                        doc.Lines.UserFields.Fields.Item(udf.name).Value = DateTime.Parse(udf.value);
                }
                #endregion

                #region Batchs
                if (line.BatchNumbers != null)
                    foreach (BatchNumber batchNumber in line.BatchNumbers)
                    {
                        doc.Lines.BatchNumbers.BatchNumber = batchNumber.DistNumber;
                        doc.Lines.BatchNumbers.Quantity = batchNumber.Quantity;
                        doc.Lines.BatchNumbers.ExpiryDate = batchNumber.ExpDate;
                        doc.Lines.BatchNumbers.Add();
                    }
                #endregion

                #region Series
                if (line.SerialNumbers != null)
                    foreach (SerialNumber serie in line.SerialNumbers)
                    {

                        doc.Lines.SerialNumbers.InternalSerialNumber = serie.DisNumber;
                        doc.Lines.SerialNumbers.Add();
                    }
                doc.Lines.Add();
                #endregion
            }
            #endregion
        }
        #endregion
    }
}
