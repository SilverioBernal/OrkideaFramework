using Microsoft.Practices.EnterpriseLibrary.Data;
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
        #endregion

        #region Métodos
        public List<Item> GetItemAll()
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT ItemCode, ItemName FROM OITM T0 ");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<Item> items = new List<Item>();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    Item item = new Item();
                    item.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.ItemName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    items.Add(item);
                }
            }
            return items;
        }

        public List<Item> GetItemList(Warehouse warehouse)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT T0.ItemCode, T0.ItemName ");            
            oSQL.Append("FROM OITM T0 ");
            oSQL.Append("INNER JOIN OITW T1 ");
            oSQL.Append("ON T0.ItemCode = T1.ItemCode ");
            oSQL.Append("AND T1.WhsCode = @warehouse");

            DbCommand dbCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());
            this.dataBase.AddInParameter(dbCommand, "warehouse", DbType.String, warehouse.WhsCode);

            List<Item> items = new List<Item>();

            using (this.reader = this.dataBase.ExecuteReader(dbCommand))
            {
                while (this.reader.Read())
                {
                    Item item = new Item();
                    item.ItemCode = this.reader.IsDBNull(0) ? "" : this.reader.GetValue(0).ToString();
                    item.ItemName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();

                    items.Add(item);
                }
            }
            return items;
        }

        public List<StockLevel> GetItemStockLevel(string itemCode)
        {

            {
                StringBuilder oSQL = new StringBuilder();
                oSQL.Append("select T0.WhsCode, T1.WhsName, OnHand, IsCommited, OnOrder, OnHand + OnOrder - IsCommited IsAvailable ");
                oSQL.Append("From OITW T0 inner join OWHS T1 on T0.WhsCode = T1.WhsCode ");                
                oSQL.Append("where ItemCode = @itemCode");                

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
