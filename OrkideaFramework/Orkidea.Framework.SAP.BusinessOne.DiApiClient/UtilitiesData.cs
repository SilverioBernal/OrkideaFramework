using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.Administration;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient
{
    public class UtilitiesData
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
        public UtilitiesData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
        }

        public UtilitiesData(string connStringName)
        {
            this.dataBase = DatabaseFactory.CreateDatabase(connStringName);
        }
        #endregion

        #region Métodos
        #region UDF's
        public List<UserDefinedField> GetUserDefinedFieldList(string tableId)
        {
            List<UserDefinedField> udfs = new List<UserDefinedField>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT AliasID, TypeID  FROM CUFD WHERE TableID = @tableId");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            this.dataBase.AddInParameter(sqlCommand, "tableId", DbType.String, tableId);

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    udfs.Add(new UserDefinedField()
                    {
                        name = this.reader.GetValue(0).ToString(),
                        type = this.reader.GetValue(1).ToString() == "A" ?UdfType.Alphanumeric : 
                        (this.reader.GetValue(1).ToString() == "N" ? UdfType.Numeric :
                        (this.reader.GetValue(1).ToString() == "D" ? UdfType.Datetime :
                        (this.reader.GetValue(1).ToString() == "B" ? UdfType.Price : UdfType.Text)))

                    });
                }
            }

            return udfs;
        }

        public List<UserDefinedFieldValue> GetUserDefinedFieldValuesList(string tableId, string fieldId)
        {
            List<UserDefinedFieldValue> udfs = new List<UserDefinedFieldValue>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append(string.Format("select FldValue, Descr from UFD1 where TableID = '{0}' and FieldID = {1}", tableId, fieldId));

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());            

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    udfs.Add(new UserDefinedFieldValue()
                    {
                        fldValue = this.reader.GetValue(0).ToString(),
                        descr = this.reader.GetValue(1).ToString() 
                    });
                }
            }

            return udfs;
        }


        #endregion 

        #region UDO's
        public List<UserDefinedFieldValue> GetUdoGenericKeyValueList(string tableId)
        {
            List<UserDefinedFieldValue> udfs = new List<UserDefinedFieldValue>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append(string.Format("select Code, Name from [{0}]", tableId));

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());            

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    udfs.Add(new UserDefinedFieldValue()
                    {
                        fldValue = this.reader.GetValue(0).ToString(),
                        descr = this.reader.GetValue(1).ToString() 
                    });
                }
            }

            return udfs;
        }
        #endregion 

        #region SalesPerson
        public List<SalesPerson> GetSalesPersonList()
        {
            List<SalesPerson> salesPersons = new List<SalesPerson>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT slpCode, slpName FROM OSLP WHERE Locked = 'N' and Active = 'Y'");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());            

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    salesPersons.Add(new SalesPerson()
                    {
                        slpCode = int.Parse(this.reader.GetValue(0).ToString()),
                        slpName = this.reader.GetValue(1).ToString()                        
                    });
                }
            }

            return salesPersons;
        }
        #endregion

        #region Currency
        public List<Currency> GetCurrencyList()
        {
            List<Currency> currencies = new List<Currency>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT currCode, currName FROM OCRN WHERE Locked = 'N'");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    currencies.Add(new Currency()
                    {
                        currCode = this.reader.GetValue(0).ToString(),
                        currName = this.reader.GetValue(1).ToString()
                    });
                }
            }

            return currencies;
        }
        #endregion

        #region Document serial
        public List<DocumentSeries> GetDocumentSeriesList(SapDocumentType docType)
        {
            List<DocumentSeries> documentSeries = new List<DocumentSeries>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT series, seriesName ");

            switch (docType)
            {
                
                case SapDocumentType.SalesInvoice:
                    oSQL.Append("FROM NNM1 where ObjectCode = '13' and locked = 'N'");
                    break;
                case SapDocumentType.SalesCreditNote:
                    oSQL.Append("FROM NNM1 where ObjectCode = '14' and locked = 'N'");
                    break;
                case SapDocumentType.SalesDelivery:
                    oSQL.Append("FROM NNM1 where ObjectCode = '15' and locked = 'N'");
                    break;
                case SapDocumentType.SalesReturn:
                    oSQL.Append("FROM NNM1 where ObjectCode = '16' and locked = 'N'");
                    break;
                case SapDocumentType.SalesOrder:
                    oSQL.Append("FROM NNM1 where ObjectCode = '17' and locked = 'N'");
                    break;
                case SapDocumentType.PurchaseInvoice:
                    oSQL.Append("FROM NNM1 where ObjectCode = '18' and locked = 'N'");
                    break;
                case SapDocumentType.PurchaseCreditNote:
                    oSQL.Append("FROM NNM1 where ObjectCode = '19' and locked = 'N'");
                    break;
                case SapDocumentType.PurchaseDelivery:
                    oSQL.Append("FROM NNM1 where ObjectCode = '20' and locked = 'N'");
                    break;
                case SapDocumentType.PurchaseReturn:
                    oSQL.Append("FROM NNM1 where ObjectCode = '21' and locked = 'N'");
                    break;
                case SapDocumentType.PurchaseOrder:
                    oSQL.Append("FROM NNM1 where ObjectCode = '22' and locked = 'N'");
                    break;                
                default:
                    break;
            }

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    documentSeries.Add(new DocumentSeries()
                    {
                        series = int.Parse(this.reader.GetValue(0).ToString()),
                        seriesName = this.reader.GetValue(1).ToString()
                    });
                }
            }

            return documentSeries;
        }

        public DocumentSeries GetDocumentSeriesSingle(int series)
        {
            DocumentSeries documentSeries = null;

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT series, seriesName ");
            oSQL.Append(string.Format("FROM NNM1 where series = '{0}'",series));
            
            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    documentSeries= new DocumentSeries()
                    {
                        series = int.Parse(this.reader.GetValue(0).ToString()),
                        seriesName = this.reader.GetValue(1).ToString()
                    };
                }
            }

            return documentSeries;
        }
        #endregion

        #region PaymentTerms
        public List<PaymentTerm> GetPaymentTermList()
        {
            List<PaymentTerm> PaymentTerms = new List<PaymentTerm>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT GroupNum, PymntGroup FROM OCTG ");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    PaymentTerms.Add(new PaymentTerm()
                    {
                        groupNum = int.Parse(this.reader.GetValue(0).ToString()),
                        pymntGroup = this.reader.GetValue(1).ToString()
                    });
                }
            }

            return PaymentTerms;
        }
        #endregion

        #region Country/State
        public List<Country> GetCountryList()
        {
            List<Country> countries = new List<Country>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("SELECT code, name FROM OCRY ");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    countries.Add(new Country()
                    {
                        code = this.reader.GetValue(0).ToString(),
                        name = this.reader.GetValue(1).ToString()
                    });
                }
            }

            return countries;
        }

        public List<State> GetStateList(string countryCode)
        {
            List<State> states = new List<State>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append(string.Format("SELECT code, name FROM OCST where country = '{0}'", countryCode));

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    states.Add(new State()
                    {
                        code = this.reader.GetValue(0).ToString(),
                        name = this.reader.GetValue(1).ToString()
                    });
                }
            }

            return states;
        }
        #endregion

        #region Distribution rules
        public List<SapDistributionRule> GetDistributionRulesList()
        {
            List<SapDistributionRule> sapDistributionRules = new List<SapDistributionRule>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("select prcCode, prcName from OPRC where Locked = 'N' ");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    sapDistributionRules.Add(new SapDistributionRule()
                    {
                        prcCode = this.reader.GetValue(0).ToString(),
                        prcName= this.reader.GetValue(1).ToString()
                    });
                }
            }

            return sapDistributionRules;
        }
        #endregion


        #endregion 
    }
}
