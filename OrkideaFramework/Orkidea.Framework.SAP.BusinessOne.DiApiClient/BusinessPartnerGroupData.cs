using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.Entities.BusinessPartners;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient
{
    public class BusinessPartnerGroupData
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
        public BusinessPartnerGroupData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
        }

        public BusinessPartnerGroupData(string connStringName)
        {
            this.dataBase = DatabaseFactory.CreateDatabase(connStringName);
        }
        #endregion

        #region Methods
        public List<BusinessPartnerGroup> GetList(CardType cardType)
        {
            StringBuilder oSQL = new StringBuilder();
            oSQL.Append("SELECT  GroupCode,GroupName FROM OCRG T0 ");
            switch (cardType)
            {
                case CardType.Customer:
                    oSQL.Append(string.Format("where GroupType = '{0}' ", "C"));
                    break;
                case CardType.Supplier:
                    oSQL.Append(string.Format("where GroupType = '{0}' ", "S"));
                    break;
                case CardType.Lead:
                    oSQL.Append(string.Format("where GroupType = '{0}' ", "C"));
                    break;
                default:
                    break;
            }
            DbCommand myCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<BusinessPartnerGroup> partnerGroups = new List<BusinessPartnerGroup>();

            using (this.reader = this.dataBase.ExecuteReader(myCommand))
            {
                while (this.reader.Read())
                {
                    BusinessPartnerGroup partnerGroup = new BusinessPartnerGroup();
                    partnerGroup.groupCode = int.Parse(this.reader.GetValue(0).ToString());
                    partnerGroup.groupName = this.reader.IsDBNull(1) ? "" : this.reader.GetValue(1).ToString();
                    partnerGroups.Add(partnerGroup);
                }
            }
            return partnerGroups;
        }
        #endregion
    }
}
