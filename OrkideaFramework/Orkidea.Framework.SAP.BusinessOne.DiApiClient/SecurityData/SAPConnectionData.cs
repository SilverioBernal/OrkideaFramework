using Orkidea.Framework.SAP.BusinessOne.Entities.Global.ExceptionManagement;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.SAP.BusinessOne.DiApiClient.SecurityData
{
    public class SAPConnectionData
    {
        #region Attributes
        static readonly object padlock = new object();
        static readonly object padlock2 = new object();
        /// <summary>
        /// Contiene la conexion a SAP Business One
        /// </summary>
        public SAPConnection Conn; 
        #endregion

        #region Constructor
        public SAPConnectionData()
        {
            Conn = SAPConnection.conn;
        }

        public SAPConnectionData(string dataBase, string licenceServer, string DatabaseServer, string user, string password, string userBd, string passwordBD, string serverType)
        {
            Conn = new SAPConnection(dataBase, licenceServer, DatabaseServer, user, password, userBd, passwordBD, serverType);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Inicia una transacción en SAP Business One
        /// </summary>
        /// <returns>Estado de la operación</returns>
        public bool BeginTran()
        {
            lock (padlock)
            {
                while (Conn.company.InTransaction) { }
                Conn.company.StartTransaction();
            }
            return true;
        }

        /// <summary>
        /// Liberar el objeto COM de acuerdo a buenas prácticas
        /// </summary>
        /// <returns>Bool, Indica el exito de la tarea de liberar la compañia</returns>
        public bool ReleaseCompany()
        {
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(conexion.compania);
            Conn = null;
            SAPConnection.conn = null;
            return true;
        }


        public bool DisconnectCompany()
        {
            Conn.company.Disconnect();
            return true;
        }

        /// <summary>
        /// Permite conectar con SAP Business One
        /// </summary>
        /// <returns>Estado de la operación</returns>
        public bool ConnectCompany()
        {
            long nResult = 0;
            lock (padlock2)
            {
                if (!Conn.company.Connected)
                    nResult = Conn.company.Connect();
            }
            if (nResult != 0)
            {
                throw new SAPException(Conn.company.GetLastErrorCode(), Conn.company.GetLastErrorDescription());
            }
            return true;
        }

        /// <summary>
        /// Conecta a SAP Business One segun las credenciales enviadas
        /// </summary>
        /// <param name="dataBase">Compañia de SAP a la que se va conectar</param>
        /// <param name="sapUser">Usuario de autenticación con SAP</param>
        /// <param name="sapUserPassword">Contraseña de autenticación con SAP</param>
        /// <returns>True|Conexión exitosa
        ///         False|Fallo en la conexión</returns>
        public bool ConnectCompany(string dataBase, string sapUser, string sapUserPassword)
        {
            long nResult = 0;
            Conn.company.CompanyDB = dataBase;
            Conn.company.UserName = sapUser;
            Conn.company.Password = sapUserPassword;

            if (!Conn.company.Connected)
                nResult = Conn.company.Connect();
            if (nResult != 0)
            {
                throw new SAPException(Conn.company.GetLastErrorCode(), Conn.company.GetLastErrorDescription());
            }
            return true;
        }

        /// <summary>
        /// Termina una transacción en SAP, exitosa o fallida
        /// </summary>
        /// <param name="TransOpt">Opción de la transacción</param>
        /// <returns>Estado de la operación</returns>
        public bool EndTranAndRelease(BoWfTransOpt TransOpt)
        {
            if (Conn.company.Connected)
            {
                if (Conn.company.InTransaction)
                    Conn.company.EndTransaction(TransOpt);

                DisconnectCompany();
                ReleaseCompany();
            }
            return true;
        }

        /// <summary>
        /// Termina una transacción en SAP, exitosa o fallida
        /// </summary>
        /// <param name="TransOpt">Opción de la transacción</param>
        /// <returns>Estado de la operación</returns>
        public bool EndTran(BoWfTransOpt TransOpt)
        {
            if (Conn.company.Connected)
            {
                if (Conn.company.InTransaction)
                    Conn.company.EndTransaction(TransOpt);
            }
            return true;
        }
        #endregion
    }
}
