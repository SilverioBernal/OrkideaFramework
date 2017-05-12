using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.Entities.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient
{
    public class FinanceData
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
        public FinanceData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
        }

        public FinanceData(string connStringName)
        {
            this.dataBase = DatabaseFactory.CreateDatabase(connStringName);
        }
        #endregion

        #region Métodos
        public List<SalesTaxCode> GetSalesTaxCodeList()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT code, name FROM OSTC T0 where lock = 'N'");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<SalesTaxCode> taxCodes = new List<SalesTaxCode>();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    SalesTaxCode item = new SalesTaxCode();
                    item.code = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.name = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    taxCodes.Add(item);
                }
            }
            return taxCodes;
        }

        public SalesTaxCode GetSingleTaxCode(string taxCode)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append(string.Format("SELECT code, name, rate FROM OSTC T0 where code = '{0}'", taxCode));

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            SalesTaxCode tax = new SalesTaxCode();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    tax.code = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    tax.name = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    tax.rate = this.reader.IsDBNull(2) ? 0 : double.Parse(this.reader.GetValue(2).ToString());                    
                }
            }
            return tax;
        }

        public List<WithholdingTax> GetWithholdingTax()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT wtCode, wtName FROM OWHT T0 where inactive = 'N'");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<WithholdingTax> withholdingTaxes = new List<WithholdingTax>();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    WithholdingTax item = new WithholdingTax();
                    item.wtCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.wtName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    withholdingTaxes.Add(item);
                }
            }
            return withholdingTaxes;
        }
        #endregion
    }
}
