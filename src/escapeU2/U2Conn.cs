using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IBMU2.UODOTNET;

namespace escapeU2
{
    class U2Conn : IEU2DbConn
    {
        // todo: create an IEnumerable method that takes a "file" name and returns row data
        // or i may need an OpenFile method then getrows with no parameter?

        public IEnumerable<string> GetRows(string fileName)
        {
            yield return "the end";
        }

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
        private string port = "31438";
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
        public bool isConnected
        {
            get
            {
                return isconnected;
            }
            set
            {
            }
        }

        private UniSession uSession;
        public void Connect()
        {
            // check for required props throw exception if not set
            // do i need try/catch here or just let exception bubble up/out?
            try
            {
                uSession = UniObjects.OpenSession(host, login, password, catalog);
                isconnected = true;
            }
            catch (UniSessionException e)
            {
                isconnected = false;
                throw;
            }

        }

        public void Disconnect()
        {
            if (uSession != null && uSession.IsActive)
            {
                isconnected = false;
                UniObjects.CloseSession(uSession);
            }
        }

        ~U2Conn()
        {
            if (isconnected) this.Disconnect();
        }
    }
}
