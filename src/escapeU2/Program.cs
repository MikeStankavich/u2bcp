using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;


namespace escapeU2
{
    class Program
    {

        static Options _options;
            
        static void Main(string[] args)
        {
            _options = new Options(args);

            if (_options.Verbose) Console.WriteLine(_options.ToString());
            
            try
            {
                var srcConn = new U2Conn();
                srcConn.Connect(_options.U2Host, _options.U2Login, _options.U2Password, _options.U2Account);
                
                var connectString = string.Format("Server={0};User Id={1};Password={2};;Database={3};"
                                             , _options.SqlHost // "pmsteel.kwyk.net"
                                             , _options.SqlLogin  // "pmsteel"
                                             , _options.SqlPassword // "H!carb0n"
                                             , _options.SqlDatabase // "pmsteel"
                                             );
                var destConn = new SqlConnection(connectString);
                destConn.Open();

                

                if ("" != _options.SqlDictTable)
                {
                    var srcDictReader = srcConn.GetDictReader(_options.U2File);

                    SqlTableAction(destConn, _options.SqlTableAction, _options.SqlDictTable, "dictionary");

                    using (var bulkCopy = new SqlBulkCopy(destConn))
                    {
                        bulkCopy.DestinationTableName = _options.SqlDictTable;
                        bulkCopy.BatchSize = 100;
                        bulkCopy.NotifyAfter = 100;
                        bulkCopy.SqlRowsCopied += OnSqlRowsCopied;
                        bulkCopy.WriteToServer(srcDictReader);
                    }
                    Console.WriteLine("Copied {0} rows to {1}", srcDictReader.RecordsAffected, _options.SqlDictTable);
                }

                var srcReader = srcConn.GetReader(_options.U2File);
                srcReader.Limit = _options.Limit;
                
                /*
                while (srcReader.Read())
                {
                    for (int i = 0; i < srcReader.FieldCount; i++)
                    {
                        Console.Write(srcReader.GetValue(i).ToString() + '^');
                    }
                    Console.Write('\n');
                }
                */

                SqlTableAction(destConn, _options.SqlTableAction, _options.SqlTable, _options.SqlTableFormat);

                using (var bulkCopy = new SqlBulkCopy(destConn))
                {
                    bulkCopy.DestinationTableName = _options.SqlTable;
                    bulkCopy.BatchSize = 100;
                    bulkCopy.NotifyAfter = 100;
                    bulkCopy.SqlRowsCopied += OnSqlRowsCopied;
                    bulkCopy.WriteToServer(srcReader);
                }
                Console.WriteLine("Copied {0} rows to {1}", srcReader.RecordsAffected, _options.SqlTable);

                if (_options.Verbose)
                {
                    Console.Write("\nPress any key to continue");
                    Console.ReadKey();
                }
                
                /*
                                else
                                {

                                    var connectString =  string.Format("Server={0};User Id={1};Password={2};;Database={3};"
                                                                 , _options.SqlHost // "pmsteel.kwyk.net"
                                                                 , _options.SqlLogin  // "pmsteel"
                                                                 , _options.SqlPassword // "H!carb0n"
                                                                 , _options.SqlDatabase // "pmsteel"
                                                                 );
                                    var destConn = new SqlConnection(connectString);
                                    destConn.Open();

                                    using (var cmd = destConn.CreateCommand())
                                    {
                                        cmd.CommandText = "exec prc_prep_table '" + _options.SqlTable + "', " + srcReader.FieldCount;
                                        cmd.ExecuteNonQuery();
                                    }

                                    using (var bulkCopy = new SqlBulkCopy(destConn))
                                    {
                                        bulkCopy.DestinationTableName = _options.SqlTable;
                                        bulkCopy.BatchSize = 100;
                                        bulkCopy.NotifyAfter = 100;
                                        // Set up the event handler to notify after 50 rows.
                                        //bulkCopy.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);
                                        bulkCopy.SqlRowsCopied += OnSqlRowsCopied;
                                        bulkCopy.WriteToServer(srcReader);
                                    }
                                }
                                */

                srcReader.Close();

                srcConn.Disconnect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                // todo: remove this after debugging
                Console.ReadKey();
            }
        }
        private static void OnSqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            if (_options.Verbose)
                Console.WriteLine("Copied {0} so far...", e.RowsCopied);
        }

        private static bool SqlTableAction(SqlConnection sqlConn, string tableAction, string tableName, string tableFormat)
        {

            using(SqlCommand sqlCmd = sqlConn.CreateCommand())
            {
                switch (tableAction.ToLower())
                {
                    case "truncate":
                        sqlCmd.CommandText = string.Format("TRUNCATE TABLE {0}", tableName);
                        if (_options.Verbose)
                            Console.WriteLine(sqlCmd.CommandText);
                        sqlCmd.ExecuteNonQuery();
                        break;

                    case "overwrite":
                        sqlCmd.CommandText = string.Format("IF OBJECT_ID('{0}') IS NOT NULL DROP TABLE {0}", tableName);
                        if (_options.Verbose)
                            Console.WriteLine(sqlCmd.CommandText);
                        sqlCmd.ExecuteNonQuery();
                        goto case "create";

                    case "create":
                        switch (tableFormat.ToLower())
                        {
                            case "raw":
                                sqlCmd.CommandText =
                                    string.Format(
                                        "CREATE TABLE {0} (id VARCHAR(900) NOT NULL PRIMARY KEY, data VARCHAR(MAX) NULL)"
                                        , tableName);
                                break;

                            case "dictionary":
                                sqlCmd.CommandText = string.Format("CREATE TABLE {0} (" +
                                                                   "   id varchar(20) NOT NULL, /* primary key, */" +
                                                                   "   typ varchar(20) NOT NULL," +
                                                                   "   loc varchar(255) NOT NULL," +
                                                                   "   conv varchar(50) NOT NULL," +
                                                                   "   mname varchar(50) NOT NULL," +
                                                                   "   fmt varchar(50) NOT NULL," +
                                                                   "   sm varchar(50) NOT NULL," +
                                                                   "	assoc varchar(50) NOT NULL)", tableName);
                                break;
                        }
                        if (_options.Verbose)
                            Console.WriteLine(sqlCmd.CommandText);
                        sqlCmd.ExecuteNonQuery();
                        break;
                }
            }
            return true;
        }

       
    }
}
