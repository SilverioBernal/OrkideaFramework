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
        public List<MarketingDocument> GetList(SapDocumentType mktgDocType, DateTime startDate, DateTime endDate)
        {
            StringBuilder oSQL = new StringBuilder();

            switch (mktgDocType)
            {
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
                case SapDocumentType.SalesInvoice:
                    break;
                case SapDocumentType.SalesDelivery:
                    break;
                case SapDocumentType.SalesCreditNote:
                    break;
                case SapDocumentType.PurchaseCreditNote:
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

        /// <summary>
        /// Consulta un socio de negocios en SAP Business One
        /// </summary>
        /// <param name="cardCode">Codigo de socio de negocio</param>
        /// <returns>Socio con la información</returns>
        public MarketingDocument GetSingle(SapDocumentType mktgDocType, string docNum)
        {
            StringBuilder oSQL = new StringBuilder();
            MarketingDocument document = new MarketingDocument();

            switch (mktgDocType)
            {
                case SapDocumentType.PurchaseOrder:
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
                            if (document.docEntry == 0)
                            {
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
                            }
                            MarketingDocumentLine line = new MarketingDocumentLine();
                            line.itemCode = this.reader.IsDBNull(8) ? "" : this.reader.GetValue(8).ToString();
                            line.quantity = this.reader.IsDBNull(9) ? 0 : Convert.ToDouble(this.reader.GetValue(9).ToString());
                            line.whsCode = this.reader.IsDBNull(10) ? "" : this.reader.GetValue(10).ToString();
                            line.whsName = this.reader.IsDBNull(11) ? "" : this.reader.GetValue(11).ToString();
                            line.dscription = this.reader.IsDBNull(12) ? "" : this.reader.GetValue(12).ToString();
                            line.lineStatus = this.reader.IsDBNull(13) ? "" : this.reader.GetValue(13).ToString();
                            line.openCreQty = this.reader.IsDBNull(16) ? 0 : Convert.ToDouble(this.reader.GetValue(16).ToString());
                            line.openQty = this.reader.IsDBNull(17) ? 0 : Convert.ToDouble(this.reader.GetValue(17).ToString());
                            line.unitMsr = this.reader.IsDBNull(18) ? "" : this.reader.GetValue(18).ToString();
                            line.numPerMsr = this.reader.IsDBNull(19) ? 0 : Convert.ToDouble(this.reader.GetValue(19).ToString());
                            line.targetType = this.reader.IsDBNull(20) ? 0 : Convert.ToInt32(this.reader.GetValue(20).ToString());
                            line.trgetEntry = this.reader.IsDBNull(21) ? 0 : Convert.ToInt32(this.reader.GetValue(21).ToString());
                            line.lineNum = this.reader.IsDBNull(22) ? 0 : Convert.ToInt32(this.reader.GetValue(22).ToString());

                            document.lines.Add(line);
                        }
                    }
                    break;
                case SapDocumentType.SalesInvoice:
                    break;
                case SapDocumentType.SalesDelivery:
                    break;
                case SapDocumentType.SalesCreditNote:
                    break;
                case SapDocumentType.PurchaseCreditNote:
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
        public MarketingDocument Add(SapDocumentType mktgDocType, MarketingDocument document)
        {
            Documents doc = null;

            #region Document definition
            switch (mktgDocType)
            {
                case SapDocumentType.PurchaseOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                    break;
                case SapDocumentType.SalesOrder:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oOrders);
                    break;
                case SapDocumentType.SalesInvoice:
                    doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oInvoices);
                    break;
                case SapDocumentType.SalesDelivery:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    break;
                case SapDocumentType.SalesCreditNote:
                    doc = doc = (Documents)SAPConnection.conn.company.GetBusinessObject(BoObjectTypes.oCreditNotes);
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

            #region UDF's
            foreach (UserDefinedField item in document.userDefinedFields)
            {
                if (!string.IsNullOrEmpty(item.value))
                    switch (item.type)
                    {
                        case UdfType.Alphanumeric:
                            doc.UserFields.Fields.Item(item.name).Value = item.value;
                            break;
                        case UdfType.Numeric:
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
                    doc.Lines.Price = line.price;
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
                            case UdfType.Numeric:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
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
                case SapDocumentType.PurchaseOrder:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                    break;
                case SapDocumentType.SalesOrder:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oOrders);
                    break;
                case SapDocumentType.SalesInvoice:
                    doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oInvoices);
                    break;
                case SapDocumentType.SalesDelivery:
                    doc = doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oDeliveryNotes);
                    break;
                case SapDocumentType.SalesCreditNote:
                    doc = doc = (Documents)sapConn.company.GetBusinessObject(BoObjectTypes.oCreditNotes);
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
                doc.PayToCode= document.payToCode;

            if (document.groupNum!= null)
                doc.GroupNumber = (int)document.groupNum;

            if (!string.IsNullOrEmpty(document.comments))
                doc.Comments = document.comments;

            if (document.slpCode!=null)
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
                        case UdfType.Numeric:
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
                    doc.Lines.Price = line.price;
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
                            case UdfType.Numeric:
                                doc.Lines.UserFields.Fields.Item(item.name).Value = double.Parse(item.value);
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
        #endregion
    }
}
