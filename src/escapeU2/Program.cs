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
                var srcReader = srcConn.GetReader(_options.U2File);


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


       
    }
}
