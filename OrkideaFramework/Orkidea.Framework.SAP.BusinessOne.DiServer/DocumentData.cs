using Orkidea.Framework.SAP.BusinessOne.Entities.Global;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.Administration;
using Orkidea.Framework.SAP.BusinessOne.Entities.MarketingDocuments;
using SBODI_Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Orkidea.Framework.SAP.BusinessOne.DiServer
{
    public class DocumentData
    {
        public string _dataBase { get; set; }
        public string _licenceServer { get; set; }
        public string _databaseServer { get; set; }
        public string _user { get; set; }
        public string _password { get; set; }
        public string _userBd { get; set; }
        public string _passwordBD { get; set; }
        public string _serverType { get; set; }

        private Node node = null;
        private string _sessionId = string.Empty;

        public DocumentData(string dataBase, string licenceServer, string databaseServer, string user, string password, string userBd, string passwordBD, string serverType)
        {
            _dataBase = dataBase;
            _licenceServer = licenceServer;
            _databaseServer = databaseServer;
            _user = user;
            _password = password;
            _userBd = userBd;
            _passwordBD = passwordBD;
            //_serverType = serverType;

            switch (serverType)
            {
                case "MSSQL2005":
                    _serverType = "dst_MSSQL2005";
                    break;
                case "MSSQL2008":
                    _serverType = "dst_MSSQL2008";
                    break;
                default:
                    _serverType = "dst_MSSQL2012";
                    break;
            }
        }

        public void CreateSession()
        {
            node = new Node();
            StringBuilder cmd = new StringBuilder();

            cmd.Append(@"<?xml version=""1.0"" encoding=""UTF-16""?>");
            cmd.Append(@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">");
            cmd.Append(@"<env:Body><dis:Login xmlns:dis=""http://www.sap.com/SBO/DIS"">");
            cmd.Append(string.Format("<DatabaseServer>{0}</DatabaseServer>", _databaseServer));
            cmd.Append(string.Format("<DatabaseName>{0}</DatabaseName>", _dataBase));
            cmd.Append(string.Format("<DatabaseType>{0}</DatabaseType>", _serverType));
            cmd.Append(string.Format("<DatabaseUsername>{0}</DatabaseUsername>", _userBd));
            cmd.Append(string.Format("<DatabasePassword>{0}</DatabasePassword>", _passwordBD));
            cmd.Append(string.Format("<CompanyUsername>{0}</CompanyUsername>", _user));
            cmd.Append(string.Format("<CompanyPassword>{0}</CompanyPassword>", _password));
            cmd.Append("<Language>ln_English</Language>");
            cmd.Append(string.Format("<LicenseServer>{0}</LicenseServer>", _licenceServer));
            cmd.Append("</dis:Login></env:Body></env:Envelope>");

            string login = node.Interact(cmd.ToString());

            XmlDocument response = new XmlDocument();
            response.LoadXml(login);

            var sessionId = response.DocumentElement.InnerText;
            Guid session = Guid.Empty;

            if (Guid.TryParse(sessionId, out session))
                _sessionId = sessionId;
            else
                throw new Exception(sessionId);
        }

        public void ReleaseSession()
        {
            node = new Node();
            StringBuilder cmd = new StringBuilder();

            cmd.Append(@"<?xml version=""1.0"" encoding=""UTF-16""?>");
            cmd.Append(@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">");
            cmd.Append(string.Format("<env:Header><SessionID>{0}</SessionID></env:Header>", _sessionId));
            cmd.Append(@"<env:Body><dis:Logout xmlns:dis=""http://www.sap.com/SBO/DIS""></dis:Logout></env:Body>");
            cmd.Append("</env:Envelope>");

            string logout = node.Interact(cmd.ToString());

            node = null;
            _sessionId = null;
        }

        public MarketingDocument Add(SapDocumentType mktgDocType, MarketingDocument document)
        {
            StringBuilder cmd = new StringBuilder();
            string doc = string.Empty;

            #region Document definition
            switch (mktgDocType)
            {
                case SapDocumentType.SalesInvoice:
                    doc = "InvoicesService";
                    break;
                case SapDocumentType.SalesCreditNote:
                    doc = "CreditNotesService";
                    break;
                case SapDocumentType.SalesDelivery:
                    doc = "DeliveryNotesService";
                    break;
                case SapDocumentType.SalesReturn:
                    doc = "ReturnsService";
                    break;
                case SapDocumentType.SalesOrder:
                    doc = "OrdersService";
                    break;
                case SapDocumentType.PurchaseInvoice:
                    doc = "PurchaseInvoicesService";
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    doc = "PurchaseCreditNotesService";
                    break;
                case SapDocumentType.PurchaseDelivery:
                    doc = "PurchaseDeliveryNotesService";
                    break;
                case SapDocumentType.PurchaseReturn:
                    doc = "PurchaseReturnsService";
                    break;
                case SapDocumentType.PurchaseOrder:
                    doc = "PurchaseOrdersService";
                    break;
                case SapDocumentType.Quotation:
                    doc = "QuotationsService";
                    break;
                case SapDocumentType.Draft:
                    doc = "DraftsService";
                    break;
                default:
                    break;
            }
            #endregion
            try
            {
                CreateSession();
                cmd.Append(@"<?xml version=""1.0"" encoding=""UTF-16""?>");
                cmd.Append(@"<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">");
                cmd.Append(string.Format("<env:Header><SessionID>{0}</SessionID></env:Header>", _sessionId));
                cmd.Append("<env:Body>");
                cmd.Append(@"<dis:Add xmlns:dis=""http://www.sap.com/SBO/DIS"">");
                cmd.Append(string.Format("<Service>{0}</Service>", doc));

                cmd.Append("<Document>");

                #region Document header                
                cmd.Append(string.Format("<Series>{0}</Series>", document.serie));
                cmd.Append(string.Format("<CardCode>{0}</CardCode>", document.cardCode));
                cmd.Append(string.Format("<DocDate>{0}</DocDate>", document.docDate.ToString("yyyy-MM-dd")));
                cmd.Append(string.Format("<DocDueDate>{0}</DocDueDate>", document.docDueDate.ToString("yyyy-MM-dd")));
                cmd.Append(string.Format("<TaxDate>{0}</TaxDate>", document.taxDate.ToString("yyyy-MM-dd")));

                if (!string.IsNullOrEmpty(document.numAtCard))
                    cmd.Append(string.Format("<NumAtCard>{0}</NumAtCard>", document.numAtCard));

                if (!string.IsNullOrEmpty(document.shipToCode))
                    cmd.Append(string.Format("<ShipToCode>{0}</ShipToCode>", document.shipToCode));

                if (!string.IsNullOrEmpty(document.payToCode))
                    cmd.Append(string.Format("<PayToCode>{0}</PayToCode>", document.payToCode));

                if (document.groupNum != null)
                    cmd.Append(string.Format("<GroupNumber>{0}</GroupNumber>", document.groupNum.ToString()));

                if (!string.IsNullOrEmpty(document.comments))
                    cmd.Append(string.Format("<Comments>{0}</Comments>", document.comments));

                if (document.slpCode != null)
                    cmd.Append(string.Format("<SalesPersonCode>{0}</SalesPersonCode>", document.slpCode));

                #endregion

                #region Document lines
                cmd.Append("<DocumentLines>");
                foreach (MarketingDocumentLine line in document.lines)
                {
                    cmd.Append("<DocumentLine>");

                    if (line.baseType != 202)
                        cmd.Append(string.Format("<ItemCode>{0}</ItemCode>", line.itemCode));

                    cmd.Append(string.Format("<Quantity>{0}</Quantity>", line.quantity.ToString()));
                    cmd.Append(string.Format("<WarehouseCode>{0}</WarehouseCode>", line.whsCode));

                    if (line.price != 0)
                        cmd.Append(string.Format("<UnitPrice>{0}</UnitPrice>", line.price.ToString()));

                    if (!string.IsNullOrEmpty(line.unitMsr))
                        cmd.Append(string.Format("<MeasureUnit>{0}</MeasureUnit>", line.unitMsr));

                    if (!string.IsNullOrEmpty(line.taxCode))
                        cmd.Append(string.Format("<TaxCode>{0}</TaxCode>", line.taxCode));

                    if (!string.IsNullOrEmpty(line.ocrCode))
                        cmd.Append(string.Format("<CostingCode>{0}</CostingCode>", line.ocrCode));

                    if (line.numPerMsr != 0)
                        cmd.Append(string.Format("<UnitsOfMeasurment>{0}</UnitsOfMeasurment>", line.numPerMsr));

                    if (line.baseEntry != 0)
                    {
                        cmd.Append(string.Format("<BaseEntry>{0}</BaseEntry>", line.baseEntry));
                        cmd.Append(string.Format("<BaseLine>{0}</BaseLine>", line.baseLine));
                        cmd.Append(string.Format("<BaseType>{0}</BaseType>", line.baseType));
                    }

                    cmd.Append("</DocumentLine>");
                }

                cmd.Append("<DocumentLines>");
                #endregion

                cmd.Append("</Document>");
                cmd.Append("</dis:Add></env:Body></env:Envelope>");

                string AddDocument = node.BatchInteract(cmd.ToString());
                XmlDocument response = new XmlDocument();
                response.Load(AddDocument);

                int docEntry = -1;
                if (int.TryParse(response.DocumentElement.InnerText, out docEntry))                
                    document.docEntry = docEntry;

                ReleaseSession();
            }
            catch (Exception ex)
            {
                ReleaseSession();
                throw ex;
            }            

            return document;
        }
    }
}
