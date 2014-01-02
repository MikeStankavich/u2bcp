using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandLine;
using CommandLine.Text;
using CommandLine.Extensions;
using CommandLine.Infrastructure;
using CommandLine.Parsing;

namespace escapeU2
{
    class Options
    {

        public Options(string[] args)
        {
            // load all propertiers from settings.app file
            this.Verbose = Properties.Settings.Default.Verbose;
            this.Limit = Properties.Settings.Default.Limit;

            this.U2Host = Properties.Settings.Default.U2Host;
            this.U2Port = Properties.Settings.Default.U2Port;
            this.U2Login = Properties.Settings.Default.U2Login;
            this.U2Password = Properties.Settings.Default.U2Password;
            this.U2Account = Properties.Settings.Default.U2Account;
            this.U2File = Properties.Settings.Default.U2File;

            this.SqlHost = Properties.Settings.Default.SqlHost;
            this.SqlPort = Properties.Settings.Default.SqlPort;
            this.SqlLogin = Properties.Settings.Default.SqlLogin;
            this.SqlPassword = Properties.Settings.Default.SqlPassword;
            this.SqlDatabase = Properties.Settings.Default.SqlDatabase;
            this.SqlTable = Properties.Settings.Default.SqlTable;
            this.SqlDictTable = Properties.Settings.Default.SqlDictTable;
            this.SqlTableAction = Properties.Settings.Default.SqlTableAction;
            this.SqlTableFormat = Properties.Settings.Default.SqlTableFormat;

            // parse command line, override app settings when setting was passed on the command line
            CommandLine.Parser.Default.ParseArguments(args, this);

            //default SQLTable to U2File if it isn't specified
            //if ("" == this.SqlTable)
            //    this.SqlTable = this.U2File;
        }

        public override string ToString()
        {
            string tsout = "";

            tsout += "Verbose: " + Verbose.ToString() + "\n";
            tsout += "Format: " + SqlTableFormat + "\n";
            tsout += "Limit: " + SqlTableFormat + "\n";
            tsout += "Dump: " + SqlTableFormat + "\n";

            tsout += "U2Host: " + U2Host + "\n";
            tsout += "U2Login: " + U2Login + "\n";
            tsout += "U2Password: " + U2Password + "\n";
            tsout += "U2Account: " + U2Account + "\n";
            tsout += "U2File: " + U2File + "\n";

            tsout += "SqlHost: " + SqlHost + "\n";
            tsout += "SqlPort: " + SqlPort + "\n";
            tsout += "SqlLogin: " + SqlLogin + "\n";
            tsout += "SqlPassword: " + SqlPassword + "\n";
            tsout += "SqlDatabase: " + SqlDatabase + "\n";
            tsout += "SqlTable: " + SqlTable + "\n";
            tsout += "SqlDictTable: " + SqlDictTable + "\n";
            tsout += "SqlTableAction: " + SqlTableAction + "\n";



            return tsout;
        }

        [Option('v', "verbose", DefaultValue = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('l', "limit", HelpText = "Return no more than N rows.")]
        public int Limit { get; set; }

        [Option("U2Host")]
        public String U2Host { get; set; }

        [Option("U2Port")]
        public String U2Port { get; set; }

        [Option("U2Login")]
        public String U2Login { get; set; }

        [Option("U2Password")]
        public String U2Password { get; set; }

        [Option("U2Account")]
        public String U2Account { get; set; }

        [Option("U2File")]
        public String U2File { get; set; }


        [Option("SqlHost")]
        public String SqlHost { get; set; }

        [Option("SqlPort")]
        public String SqlPort { get; set; }

        [Option("SqlLogin")]
        public String SqlLogin { get; set; }

        [Option("SqlPassword")]
        public String SqlPassword { get; set; }

        [Option("SqlDatabase")]
        public String SqlDatabase { get; set; }

        [Option("SqlTable")]
        public String SqlTable { get; set; }

        [Option("SqlDictTable")]
        public String SqlDictTable { get; set; }

        [Option("SqlTableAction")]
        public String SqlTableAction { get; set; }

        [Option('f', "SqlTableFormat")]
        public String SqlTableFormat { get; set; }
    }
}

