using System;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Reflection;

namespace escapeU2
{
    class Program
    {

        static Options _options;
            
        static void Main(string[] args)
        {
            _options = new Options(args);
            SqlBulkCopy bulkCopy = null;

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

                bulkCopy = new SqlBulkCopy(destConn);

                if ("" != _options.SqlDictTable)
                {
                    var srcDictReader = srcConn.GetDictReader(_options.U2File);

                    SqlTableAction(destConn, _options.SqlTableAction, _options.SqlDictTable, "dictionary");

                    //using (var bulkCopy = new SqlBulkCopy(destConn))
                    bulkCopy = new SqlBulkCopy(destConn);
                    {
                        bulkCopy.DestinationTableName = _options.SqlDictTable;
                        bulkCopy.BatchSize = 100;
                        bulkCopy.NotifyAfter = 100;
                        bulkCopy.SqlRowsCopied += OnSqlRowsCopied;
                        bulkCopy.WriteToServer(srcDictReader);
                    }
                    Console.WriteLine("Copied {0} dictionary records to {1}", srcDictReader.RecordsAffected, _options.SqlDictTable);
                }
                
                if ("" != _options.SqlTable)
                {
                    var srcReader = srcConn.GetReader(_options.U2File);
                    srcReader.Limit = _options.Limit;
                    
                    SqlTableAction(destConn, _options.SqlTableAction, _options.SqlTable, _options.SqlTableFormat);

                  //  using (bulkCopy = new SqlBulkCopy(destConn))
                        bulkCopy = new SqlBulkCopy(destConn);
                    {
                        bulkCopy.DestinationTableName = _options.SqlTable;
                        bulkCopy.BatchSize = 100;
                        bulkCopy.NotifyAfter = 100;
                        bulkCopy.SqlRowsCopied += OnSqlRowsCopied;
                        bulkCopy.WriteToServer(srcReader);
                    }
                    Console.WriteLine("Copied {0} data records to {1}", srcReader.RecordsAffected, _options.SqlTable);
                    srcReader.Close();
                }

                srcConn.Disconnect();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Received an invalid column length from the bcp client for colid") && bulkCopy != null)
                {
                    string pattern = @"\d+";
                    Match match = Regex.Match(ex.Message.ToString(), pattern);
                    var index = Convert.ToInt32(match.Value) - 1;

                    FieldInfo fi = typeof(SqlBulkCopy).GetField("_sortedColumnMappings", BindingFlags.NonPublic | BindingFlags.Instance);
                    var sortedColumns = fi.GetValue(bulkCopy);
                    var items = (Object[])sortedColumns.GetType().GetField("_items", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(sortedColumns);

                    FieldInfo itemdata = items[index].GetType().GetField("_metadata", BindingFlags.NonPublic | BindingFlags.Instance);
                    var metadata = itemdata.GetValue(items[index]);

                    var column = metadata.GetType().GetField("column", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                    var length = metadata.GetType().GetField("length", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).GetValue(metadata);
                    //throw new DataFormatException(String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
                    Console.WriteLine(String.Format("Column: {0} contains data with a length greater than: {1}", column, length));
                }

                Console.WriteLine("Error: " + ex);

                // todo: remove this after debugging?
                if (_options.Verbose)
                {
                    Console.Write("\nPress any key to continue");
                    Console.ReadKey();
                }
            }
        }
        private static void OnSqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            if (_options.Verbose)
                Console.WriteLine("{0} records copied...", e.RowsCopied);
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
                                        "CREATE TABLE {0} (id VARCHAR(900) NOT NULL /* PRIMARY KEY */, data VARCHAR(MAX) NULL)"
                                        , tableName);
                                break;

                            case "dictionary":
                                sqlCmd.CommandText = string.Format("CREATE TABLE {0} (" +
                                                                   "   id varchar(900) NOT NULL, /* primary key, */" +
                                                                   "   typ varchar(4000) NOT NULL," +
                                                                   "   loc varchar(4000) NOT NULL," +
                                                                   "   conv varchar(4000) NOT NULL," +
                                                                   "   mname varchar(4000) NOT NULL," +
                                                                   "   fmt varchar(4000) NOT NULL," +
                                                                   "   sm varchar(4000) NOT NULL," +
                                                                   "   assoc varchar(4000) NOT NULL)", tableName);
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
