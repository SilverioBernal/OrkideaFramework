using Microsoft.Practices.EnterpriseLibrary.Data;
using Orkidea.Framework.SAP.BusinessOne.Entities.Global.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient
{
    public class ManagementData
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

        private string connectionStringStringName;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ManagementData()
        {
            this.dataBase = DatabaseFactory.CreateDatabase("SAP");
            this.connectionStringStringName = "SAP";
        }

        public ManagementData(string connStringName)
        {
            this.dataBase = DatabaseFactory.CreateDatabase(connStringName);
            this.connectionStringStringName = connStringName;
        }
        #endregion

        #region Métodos
        #region Authorization model
        public List<AuthorizationStatus> GetAuthorizationStatusReport(string producerFrom, string producerTo, string approverFrom, string approverTo, string templateFrom, string templateTo, DateTime dateFrom, DateTime dateTo, string cardFrom, string cardTo, string totalFrom, string totalTo, string statusWait, string statusApproved, string statusRejected, string statusProduced, string statusCanceled, string statusApprProduced, string objectsList, int numObjects, string docTable, int absEntry)
        {
            List<AuthorizationStatus> authorizationsStatus = new List<AuthorizationStatus>();
            List<WorkflowConfirmationLevel> confirmationLevels = GetWorkflowConfirmationLevelList();
            List<User> users = GetUserList();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append(string.Format("Exec TmSp_GetWDD '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', {19}, '{20}', {21}",
                producerFrom, producerTo, approverFrom, approverTo, templateFrom, templateTo, dateFrom.ToString("yyyyMMdd"), dateTo.ToString("yyyyMMdd"), cardFrom, cardTo, totalFrom, totalTo, statusWait, statusApproved, statusRejected, statusProduced, statusCanceled, statusApprProduced, objectsList, numObjects.ToString(), docTable, absEntry.ToString()));

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    authorizationsStatus.Add(new AuthorizationStatus()
                    {
                        wddCode = int.Parse(this.reader.GetValue(0).ToString()),
                        wtmCode = int.Parse(this.reader.GetValue(1).ToString()),
                        ownerId = int.Parse(this.reader.GetValue(2).ToString()),
                        ownerName = users.Where(x => x.userId.Equals(int.Parse(this.reader.GetValue(2).ToString()))).Select(x => x.uName).FirstOrDefault(),
                        docEntry = int.Parse(this.reader.GetValue(3).ToString()),
                        objType = int.Parse(this.reader.GetValue(4).ToString()),
                        docDate = DateTime.Parse(this.reader.GetValue(5).ToString()),
                        currStep = int.Parse(this.reader.GetValue(6).ToString()),
                        currStepName = confirmationLevels.Where(x => x.wstCode.Equals(int.Parse(this.reader.GetValue(6).ToString()))).Select(x => x.name).FirstOrDefault(),
                        status = this.reader.GetValue(7).ToString(),
                        remarks = this.reader.GetValue(8).ToString(),
                        userSing = int.Parse(this.reader.GetValue(9).ToString()),
                        createDate = DateTime.Parse(this.reader.GetValue(10).ToString()),
                        createTime = int.Parse(this.reader.GetValue(11).ToString()),
                        isDraft = this.reader.GetValue(12).ToString(),
                        maxReqr = int.Parse(this.reader.GetValue(13).ToString()),
                        maxRejReqr = int.Parse(this.reader.GetValue(14).ToString())
                    });
                }
            }

            List<int> docEntries = new List<int>();

            foreach (AuthorizationStatus item in authorizationsStatus)
            {
                docEntries.Add(item.docEntry);
            }

            MarketingDocumentData mkgtData = new MarketingDocumentData(this.connectionStringStringName);
            Dictionary<int, int> docNums = mkgtData.GetDocNum(SapDocumentType.Draft, docEntries);

            if (docNums.Count > 0)
                foreach (AuthorizationStatus item in authorizationsStatus)
                    foreach (KeyValuePair<int, int> docInfo in docNums)
                        if (item.docEntry.Equals(docInfo.Key))
                            item.docNum = docInfo.Value;

            return authorizationsStatus;
        }

        public List<WorkflowConfirmationLevel> GetWorkflowConfirmationLevelList()
        {
            List<WorkflowConfirmationLevel> confirmationLevels = new List<WorkflowConfirmationLevel>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("select WstCode, Name from OWST ");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            List<string> steps = new List<string>();

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    confirmationLevels.Add(new WorkflowConfirmationLevel()
                    {
                        wstCode = int.Parse(this.reader.GetValue(0).ToString()),
                        name = this.reader.GetValue(1).ToString()
                    });


                }
            }

            return confirmationLevels;
        }
        #endregion

        #region Users
        public List<User> GetUserList()
        {
            List<User> confirmationLevels = new List<User>();

            StringBuilder oSQL = new StringBuilder();

            oSQL.Append("select userId, u_Name from OUSR ");

            DbCommand sqlCommand = this.dataBase.GetSqlStringCommand(oSQL.ToString());

            using (this.reader = this.dataBase.ExecuteReader(sqlCommand))
            {
                while (this.reader.Read())
                {
                    confirmationLevels.Add(new User()
                    {
                        userId = int.Parse(this.reader.GetValue(0).ToString()),
                        uName = this.reader.GetValue(1).ToString()
                    });


                }
            }

            return confirmationLevels;
        }
        #endregion

        #endregion
    }
}
