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
            this.SqlTableFormat = Properties.Settings.Default.SqlTableFormat; 

            CommandLine.Parser.Default.ParseArguments(args, this);

            //        if (CommandLine.Parser.Default.ParseArguments(args, _options))
            // parse command line
            // override app settings when setting was passed on the command line
        }

        public override string ToString()
        {
            String tsout = "Verbose: " + Verbose.ToString() + "\n";
            tsout += "U2Host: " + U2Host.ToString() + "\n";
            tsout += "U2Login: " + U2Login.ToString() + "\n";
            tsout += "U2Password: " + U2Password.ToString() + "\n";
            tsout += "U2Account: " + U2Account.ToString() + "\n";
            tsout += "U2File: " + U2File.ToString() + "\n";

            tsout += "SqlHost: " + SqlHost.ToString() + "\n";
            tsout += "SqlPort: " + SqlPort.ToString() + "\n";
            tsout += "SqlLogin: " + SqlLogin.ToString() + "\n";
            tsout += "SqlPassword: " + SqlPassword.ToString() + "\n";
            tsout += "SqlDatabase: " + SqlDatabase.ToString() + "\n";
            tsout += "SqlTable: " + SqlTable.ToString() + "\n";
            tsout += "SqlTableFormat: " + SqlTableFormat.ToString() + "\n";

            return tsout;
        }

        [Option('v', "verbose", DefaultValue = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

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

        [Option('f', "SqlTableFormat")]
        public String SqlTableFormat { get; set; }
    }
}


/*
class Options {
  [Option('r', "read", Required = true,
    HelpText = "Input file to be processed.")]
  public string InputFile { get; set; }
    
  [Option('v', "verbose", DefaultValue = true,
    HelpText = "Prints all messages to standard output.")]
  public bool Verbose { get; set; }

  [ParserState]
  public IParserState LastParserState { get; set; }

  [HelpOption]
  public string GetUsage() {
    return HelpText.AutoBuild(this,
      (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
  }
}
 * 
 *             <setting name="U2Host" serializeAs="String">
                <value>pmsteel.kwyk.net</value>
            </setting>
            <setting name="U2Port" serializeAs="String">
                <value>31438</value>
            </setting>
            <setting name="U2Login" serializeAs="String">
                <value>administrator</value>
            </setting>
            <setting name="U2Password" serializeAs="String">
                <value>H!carb0n</value>
            </setting>
            <setting name="U2Account" serializeAs="String">
                <value>C:\BAI.PROD\PM</value>
            </setting>
            <setting name="SqlHost" serializeAs="String">
                <value>pmsteel.kwyk.net</value>
            </setting>
            <setting name="SqlPort" serializeAs="String">
                <value>1433</value>
            </setting>
            <setting name="SqlLogin" serializeAs="String">
                <value>pmsteel</value>
            </setting>
            <setting name="SqlPassword" serializeAs="String">
                <value>H!carb0n</value>
            </setting>
            <setting name="SqlDatabase" serializeAs="String">
                <value>pmsteel</value>
            </setting>
            <setting name="Verbose" serializeAs="String">
                <value>True</value>
            </setting>
            <setting name="U2File" serializeAs="String">
                <value>CUST</value>
            </setting>
            <setting name="SqlTable" serializeAs="String">
                <value>CUST</value>
            </setting>

*/