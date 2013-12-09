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

        public U2Conn()
        {
            Port = "31438";
            isConnected = false;
        }

        public IEnumerable<string> GetRows(string fileName)
        {
            yield return "the end";
        }

        public string Host { get; set; }

        public string Port { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Catalog { get; set; }

        public bool isConnected { get; set; }

        private UniSession _uSession;

        public void Connect()
        {
            // check for required props throw exception if not set
            // do i need try/catch here or just let exception bubble up/out?
            try
            {
                _uSession = UniObjects.OpenSession(Host, Login, Password, Catalog);
                isConnected = true;
            }
            catch (UniSessionException e)
            {
                isConnected = false;
                throw;
            }

        }
        public void Connect(string host, string login, string password, string catalog)
        {
            this.Host = host;
            this.Login = login;
            this.Password = password;
            this.Catalog = catalog;

            this.Connect();
        }

        public void Disconnect()
        {
            if (_uSession == null || !_uSession.IsActive) return;
            isConnected = false;
            UniObjects.CloseSession(_uSession);
        }
        
        public U2DataReader GetReader(string fileName)
        {
            return new U2DataReader(_uSession, fileName);
        }

        ~U2Conn()
        {
            if (isConnected) this.Disconnect();
        }
    }
}
