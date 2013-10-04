using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace escapeU2
{
    public interface IEU2DbConn
    {
        string Host
        {
            get;
            set;
        }

        string Port
        {
            get;
            set;
        }

        string Login
        {
            get;
            set;
        }

        string Password
        {
            get;
            set;
        }

        string Catalog
        {
            get;
            set;
        }

        bool isConnected
        {
            get;
            set;
        }

        void Connect();

        void Disconnect();
    }
}
