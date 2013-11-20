using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using IBMU2.UODOTNET;

namespace escapeU2
{
    class Program
    {
        static void debugPrint(bool debug, String str)
        {
            if (debug)
                Console.Write(str);
        }
        
        static void Main(string[] args)
        {
            bool debug = Properties.Settings.Default.Verbose;
            
            U2Conn srcConn = new U2Conn();
            SqlConn destConn = new SqlConn();
            
            srcConn.Host = Properties.Settings.Default.U2Host; // "pmsteel.kwyk.net";
            srcConn.Login = Properties.Settings.Default.U2Login; // "administrator";
            srcConn.Password = Properties.Settings.Default.U2Password; // "H!carb0n";
            srcConn.Catalog = Properties.Settings.Default.U2Account; // "C:\\BAI.PROD\\PM";

            destConn.Host = Properties.Settings.Default.SqlHost; // "pmsteel.kwyk.net";
            destConn.Login = Properties.Settings.Default.SqlLogin; // "pmsteel";
            destConn.Password = Properties.Settings.Default.SqlPassword; // "H!carb0n";
            destConn.Catalog = Properties.Settings.Default.SqlDatabase; // "pmsteel"

            /*
            try
            {
                srcConn.Connect();
                destConn.Connect();
                srcConn.Disconnect();
                destConn.Disconnect();
            }
            catch (Exception e)
            {
                debugPrint(true, "Error: " + e);
            }
            */

            String sqlStmt = "";
            
            try
            {
                // every thing runs out of the UniSession instance.
                UniSession uSession = null;

                //Set up variables.
                uSession = UniObjects.OpenSession(Properties.Settings.Default.U2Host, Properties.Settings.Default.U2Login, 
                                                  Properties.Settings.Default.U2Password, Properties.Settings.Default.U2Account);
                
                debugPrint(debug, "U2 openSession succeeded\n");

                debugPrint(debug, "Status = " + uSession.Status + "\n");

                //Class.forName("net.sourceforge.jtds.jdbc.Driver");
                //Connection conn = DriverManager.getConnection(sqlJdbcUrl, sqlUser, sqlPass);
                //debugPrint(debug, "SQL Server connection succeeded");
                //Statement stmt = conn.createStatement();

                UniFile uf = uSession.CreateUniFile(Properties.Settings.Default.U2File);
                debugPrint(debug, Properties.Settings.Default.U2File + " file opened\n");
                //debugPrint(debug, uf.ToString());

                UniSelectList usl = uSession.CreateUniSelectList(8);

                StreamWriter sw = new StreamWriter(@"c:\temp\u2rl.xml");
                        
                usl.Select(uf);
                while (!usl.LastRecordRead)
                {
                    String key = usl.Next();
                    debugPrint(debug, "key: " + key + "\n");
                    if (key != "")
                    {
                        XElement xd = new XElement("data");
                        UniDynArray udaRow = uf.Read(key);
                        for (int f = 1; f <= udaRow.Dcount(); f++)
                        {
                            XElement xf = new XElement("fld", new XAttribute("loc", f));
                            string fld = udaRow.Extract(f).ToString();
                            if ("" != fld)
                            {
                                for (int v = 1; v <= udaRow.Dcount(f); v++)
                                {
                                    XElement xv = new XElement("val", new XAttribute("loc", v));
                                    string val = udaRow.Extract(f, v).ToString();
                                    if ("" != val)
                                    {
                                        for (int s = 1; s <= udaRow.Dcount(f, v); s++)
                                        {
                                            xv.Add(new XElement("sub", new XAttribute("loc", s), udaRow.Extract(f, v, s).ToString()));
                                        }
                                    }
                                    xf.Add(xv);
                                }
                            }
                            xd.Add(xf);
                        }
                        XElement xr = new XElement("row", new XElement("id", key), xd);
                        sqlStmt = "INSERT INVF(u2_id, u2_data) VALUES ('" + key.Replace("'", "''") + "', '" + xd.ToString().Replace("'", "''") + "')";
                        sw.WriteLine(xr);
                    }
                }
                sw.Close();

                //UniDynArray udaRow = uSession.CreateUniDynArray(usl.Next());
                //debugPrint(debug, usl.readList().ToString());
                //while (!usl.IsLastRecordRead)
                //while (null != udaRow)
                //{
                    //String usKey = udaRow.Extract(1).ToString();
                    //Properties.Settings.Default.SqlTable = usKey;
                    //debugPrint(debug, "key: " + usKey + "\n");
                    //debugPrint(debug, usKey.ToString() + ": " + Properties.Settings.Default.SqlTable);
                    //sqlStmt = "IF EXISTS (SELECT * FROM sys.tables WHERE OBJECT_ID('dbo.[" + Properties.Settings.Default.SqlTable + "]') IS NOT NULL) DROP TABLE dbo.[" + Properties.Settings.Default.SqlTable + "]";
                    //debugPrint(debug, sqlStmt);
                    //stmt.executeUpdate(sqlStmt);
                    //sqlStmt = "CREATE TABLE dbo.[" + Properties.Settings.Default.SqlTable + "] (ID varchar(255) NOT NULL,LOC smallint NOT NULL,SEQ smallint NOT NULL,VAL varchar(8000) NOT NULL)";
                    //debugPrint(debug, sqlStmt);
                    //stmt.executeUpdate(sqlStmt);

                    //{{ int valIdx = 1; int fldIdx = 1;
                    //for (int fldIdx = 1; fldIdx <= udaRow.Dcount(); fldIdx++)
                    //{
                    //    debugPrint(debug, "field: " + udaRow.Extract(fldIdx).ToString());
                    //    for (int valIdx = 1; valIdx <= udaRow.dcount(fldIdx); valIdx++)
                    //    {
                    //        UniString usValue = udaRow.extract(fldIdx, valIdx);
                    //        debugPrint(debug, usValue.ToString());
                    //    }
                    //}

                    //UniFile ufTblFile = uSession.open(usKey);
                    //debugPrint(debug, "file opened");
                    ////debugPrint(debug, usKey.ToString());

                    //UniSelectList uslTblList = uSession.selectList(2);

                    //uslTblList.select(ufTblFile);
                    //UniString usTblKey = uslTblList.next();
                    //int cnt = 0;

                    //while (!uslTblList.isLastRecordRead())
                    //{

                    //    if (cnt++ % 1000 == 0) debugPrint(debug, "rows: " + cnt);
                    //    //debugPrint(debug, usTblKey.ToString());
                    //    UniDynArray udaTblRow = new UniDynArray(ufTblFile.read(usTblKey));

                    //    for (int fldTblIdx = 1; fldTblIdx <= udaTblRow.dcount(); fldTblIdx++)
                    //    {
                    //        for (int valTblIdx = 1; valTblIdx <= udaTblRow.dcount(fldTblIdx); valTblIdx++)
                    //        {
                    //            UniString usTblValue = udaTblRow.extract(fldTblIdx, valTblIdx);
                    //            //debugPrint(debug, usTblValue.ToString());
                    //            sqlStmt = "INSERT [" + Properties.Settings.Default.SqlTable + "] VALUES ('" + usTblKey.ToString().replace("'", "''") + "', " + fldTblIdx + ", " + valTblIdx + ", '" + usTblValue.ToString().replace("'", "''") + "')";

                    //            //debugPrint(debug, sqlStmt);
                    //            //stmt.executeUpdate(sqlStmt);

                    //        }
                    //    }
                    //    usTblKey = uslTblList.next();
                    //}

                    //udaRow = uSession.CreateUniDynArray(usl.Next());
                //}
                /*
                udaKeys = usl.readList();
                //UniDynArray udaValues;
 
                sqlStmt = "TRUNCATE TABLE [" + Properties.Settings.Default.SqlTable + ']';
                debugPrint(debug, sqlStmt);
                stmt.executeUpdate(sqlStmt);
    	 

                for (int keyIdx = 1;keyIdx < udaKeys.count();keyIdx++) {
                    usKey = udaKeys.extract(keyIdx);
                    debugPrint(debug, usKey.ToString());
                    if (usKey.length() > 0) {
                        debugPrint(debug, udaKeys.count() + " " + usKey + usKey.length() + " " + keyIdx);
                        sqlStmt = "INSERT [" + Properties.Settings.Default.SqlTable + "] VALUES ('" + usKey + "', 0, 1, '" +  usKey + "')";
                        debugPrint(debug, sqlStmt);
                        stmt.executeUpdate(sqlStmt);
                        udaRow = new UniDynArray(uf.read(usKey));
                        for (int fldIdx = 1; fldIdx <= udaRow.dcount(); fldIdx++) {
                            //usField = udaRow.extract(fldIdx) ;
                            //udaValues = new UniDynArray(usField);
                            for (int valIdx = 1; valIdx <= udaRow.dcount(fldIdx); valIdx++) {
                                usValue = udaRow.extract(fldIdx, valIdx) ;
                                if (usValue.length() > 0) {
                                    //debugPrint(debug, fldIdx + " " + valIdx + " -->" + usValue + "<--");
		        		 
                                    sqlStmt = "INSERT [" + Properties.Settings.Default.SqlTable+ "] VALUES ('" + usKey + "', " + fldIdx + ", " + valIdx + ", '" +  usValue.ToString().replace("'", "''") + "')";
                                    debugPrint(debug, sqlStmt);
                                    stmt.executeUpdate(sqlStmt);
                                }
                            }
                        }
                    }
                    //debugPrint(debug, udaKeys.extract(keyIdx));
                }
                */
                uf.Close();
                debugPrint(debug, Properties.Settings.Default.U2File + " file closed\n");
                
                // did we connect?

                debugPrint(debug, "U2 Disconnected\n");
                //conn.close();
                //debugPrint(debug, "SQL Disconnected.");

            }
            catch (UniSessionException e)
            {
                debugPrint(debug, "Error: " + e);
            }
            catch (UniFileException e)
            {
                debugPrint(debug, "File Error: " + e);
            }
            catch (UniSelectListException e)
            {
                // TODO Auto-generated catch block
                debugPrint(debug, "File Error: " + e);
            }
            Console.ReadKey();
        }
    }
}
