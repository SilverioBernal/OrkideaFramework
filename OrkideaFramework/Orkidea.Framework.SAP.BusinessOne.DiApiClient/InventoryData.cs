using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.UserDefinedFileds;
using Orkidea.Framework.SAP.BusinessOne.Entities.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient
{
    public class InventoryData
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
        public InventoryData ()	
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
        }

        public InventoryData(string connStringName)
        {
            this.dataBase = DatabaseFactory.CreateDatabase(connStringName);
        }
        #endregion

        #region Métodos
        public List<GenericItem> GetItemAll()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT ItemCode, ItemName FROM OITM T0 ");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<GenericItem> items = new List<GenericItem>();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    GenericItem item = new GenericItem();
                    item.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.ItemName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    items.Add(item);
                }
            }
            return items;
        }

        public List<GenericItem> GetItemList(Warehouse warehouse)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT T0.ItemCode, T0.ItemName ");            
            oSQL.Append("FROM OITM T0 ");
            oSQL.Append("INNER JOIN OITW T1 ");
            oSQL.Append("ON T0.ItemCode = T1.ItemCode ");
            oSQL.Append("AND T1.WhsCode = @warehouse");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommand, "warehouse", DbType.String, warehouse.WhsCode);

            List<GenericItem> items = new List<GenericItem>();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    GenericItem item = new GenericItem();
                    item.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.ItemName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    items.Add(item);
                }
            }
            return items;
        }

        public Item GetSingle(string itemCode)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT T0.ItemCode, T0.ItemName, T0.TaxCodeAR, T0.DfltWH ");
            oSQL.Append("FROM OITM T0 ");            
            oSQL.Append(string.Format("where T0.ItemCode = '{0}'", itemCode));

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            Item item = new Item();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {                    
                    item.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.ItemName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    item.TaxCodeAR = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    item.DefaultWarehouse = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();   
                }
            }
            return item;
        }

        public Item GetSingle(string itemCode, List<UserDefinedField> userDefinedFields)
        {
            StringBuilder udf = new StringBuilder();
            for (int i = 0; i < userDefinedFields.Count(); i++)
            {
                if (i.Equals(userDefinedFields.Count() - 1))
                    udf.Append(string.Format(", {0} ", userDefinedFields[i].name));
                else
                    udf.Append(string.Format(", {0}, ", userDefinedFields[i].name));
                
            }

            StringBuilder oSQL = new StringBuilder();
            oSQL.Append(string.Format("SELECT T0.ItemCode, T0.ItemName, T0.TaxCodeAR, T0.DfltWH {0} ", udf.ToString()));
            oSQL.Append("FROM OITM T0 ");
            oSQL.Append(string.Format("where T0.ItemCode = '{0}'", itemCode));

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            Item item = new Item();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    item.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.ItemName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    item.TaxCodeAR = this.reader.IsDBNull(2) ? "" : this.reader.GetValue(2).ToString();
                    item.DefaultWarehouse = this.reader.IsDBNull(3) ? "" : this.reader.GetValue(3).ToString();

                    int udfItem = 4;

                    for (int i = 0; i < userDefinedFields.Count(); i++)
                    {
                        item.userDefinedFields.Add(new UserDefinedField() { name = userDefinedFields[i].name, value = this.reader.IsDBNull(udfItem) ? "" : this.reader.GetValue(udfItem).ToString() });
                        udfItem++;
                    }

                }
            }
            return item;
        }

        public double GetItemPrice(string itemCode, int priceList)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT price ");
            oSQL.Append("FROM ITM1 ");
            oSQL.Append(string.Format("where ItemCode = '{0}' and PriceList = {1}", itemCode, priceList));

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            double price = 0;

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    price = this.reader.IsDBNull(0) ? 0 : double.Parse( this.reader.GetValue(0).ToString());                    
                }
            }
            return price;
        }

        public List<StockLevel> GetItemStockLevel(string itemCode)
        {

            {
                StringBuilder oSQL = new StringBuilder();
                oSQL.Append("select T0.WhsCode, T1.WhsName, OnHand, IsCommited, OnOrder, OnHand + OnOrder - IsCommited IsAvailable ");
                oSQL.Append("From OITW T0 inner join OWHS T1 on T0.WhsCode = T1.WhsCode ");
                oSQL.Append("where ItemCode = @itemCode and (OnHand > 0 or OnOrder > 0)");                

                DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
                this.dataBase.AddInParameter(dbCommand, "itemCode", DbType.String, itemCode);

                List<StockLevel> itemStockLevel = new List<StockLevel>();

                using (this.reader = this.dataBase.ExecuteReader(dbCommand))
                {
                    while (this.reader.Read())
                    {
                        StockLevel stockWhs = new StockLevel();
                        stockWhs.WhsCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                        stockWhs.WhsName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                        stockWhs.OnHand = this.reader.IsDBNull(2) ? 0 : double.Parse(this.reader.GetValue(2).ToString());
                        stockWhs.IsCommited = this.reader.IsDBNull(3) ? 0 : double.Parse(this.reader.GetValue(3).ToString());
                        stockWhs.OnOrder = this.reader.IsDBNull(4) ? 0 : double.Parse(this.reader.GetValue(4).ToString());
                        stockWhs.IsAvailable = this.reader.IsDBNull(5) ? 0 : double.Parse(this.reader.GetValue(5).ToString());

                        itemStockLevel.Add(stockWhs);
                    }
                }
                return itemStockLevel;
            }
        }
        #endregion
    }
}
