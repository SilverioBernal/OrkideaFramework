using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient.SecurityData
{
    /// <summary>
    /// clase que se usa para manejar la información de la conexión
    /// </summary>
    public class SAPConnection
    {
        #region Atributos
        /// <summary>
        /// Permite implementar el patron de diseño Singleton con el fin de tener solo una instancia.
        /// </summary>
        private static SAPConnection Conn;
        /// <summary>
        /// Atributo de conexión a la base de datos
        /// </summary>
        public Company company;

        static readonly object padlock = new object();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor Privado para implementar el patron Singleton
        /// </summary>
        public SAPConnection(string dataBase, string licenceServer, string DatabaseServer, string user, string password, string userBd, string passwordBD, string serverType)
        {
            company = new Company();
            company.CompanyDB = dataBase;
            company.UserName = user;
            company.Server = DatabaseServer;
            company.LicenseServer = licenceServer;
            company.Password = password;
            company.DbPassword = passwordBD;
            company.DbUserName = userBd;
            company.language = BoSuppLangs.ln_Spanish_La;
            switch (serverType)
            {
                case "MSSQL2005":
                    company.DbServerType = BoDataServerTypes.dst_MSSQL2005;
                    break;
                case "MSSQL2008":
                    company.DbServerType = BoDataServerTypes.dst_MSSQL2008;
                    break;
                default:
                    company.DbServerType = BoDataServerTypes.dst_MSSQL2012;
                    break;
            }

        } 
        #endregion

        #region Properties
        /// <summary>
        /// Devuelve solo una instanacia del objeto Company
        /// </summary>
        public static SAPConnection conn
        {
            set
            {
                Conn = value;
            }
            get
            {
                lock (padlock)
                {
                    if (Conn == null)
                    {
                        Conn = new SAPConnection(
                            ConfigurationManager.AppSettings["DataBase"],
                            ConfigurationManager.AppSettings["LicenceServer"], 
                            ConfigurationManager.AppSettings["DataBaseServer"],
                            ConfigurationManager.AppSettings["UserSAP"], 
                            ConfigurationManager.AppSettings["PasswordSAP"],
                            ConfigurationManager.AppSettings["UserBD"], 
                            ConfigurationManager.AppSettings["PasswordBD"],
                            ConfigurationManager.AppSettings["ServerType"]
                            );
                    }
                    return Conn;
                }
            }
        } 
        #endregion        
    }
}
