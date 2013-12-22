using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace escapeU2
{
    class SqlConn : IEU2DbConn
    {
        private string host;
        public string Host
        {
            get
            {
                return host;
            }
            set
            {
                host = value;
            }
        }
        private string port = "1433";
        public string Port
        {
            get
            {
                return port;
            }
            set
            {
                port = value;
            }
        }

        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        private string catalog;
        public string Catalog
        {
            get
            {
                return catalog;
            }
            set
            {
                catalog = value;
            }
        }

        bool isconnected = false;
        public bool IsConnected
        {
            get
            {
                return isconnected;
            }
            set
            {
            }
        }

        //private UniSession uSession;
        SqlConnection conn = new SqlConnection();
        public void Connect()
        {
            // check for required props throw exception if not set
            // do i need try/catch here or just let exception bubble up/out?
            try
            {
                //uSession = UniObjects.OpenSession(host, login, password, catalog);
                var connectString = "server=" + host;
                connectString += ";user id=" + login;
                connectString += ";password=" + password;
                connectString += ";database=" + catalog;

                conn.ConnectionString = connectString;
                conn.Open();
                isconnected = true;
            }
            catch (Exception e)
            {
                isconnected = false;
                throw e;
            }

        }

        public void Disconnect()
        {
            //if (uSession != null && uSession.IsActive)
            {
                isconnected = false;
                //UniObjects.CloseSession(uSession);
            }
        }

        ~SqlConn()
        {
            if (isconnected) this.Disconnect();
        }
    }
}
