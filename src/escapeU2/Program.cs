using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            /*
            String sqlTable = "KAS_FILE";
            String u2RefFile = "KAS.FILE";

            String sqlStmt = "";
            bool debug = true;

            try
            {
                // every thing runs out of the UniSession instance.
                UniSession uSession = null;

                //Set up variables.

                
                debugPrint(debug, "U2 openSession succeeded");

                debugPrint(debug, "Status = " + uSession.Status);

                //Class.forName("net.sourceforge.jtds.jdbc.Driver");
                //Connection conn = DriverManager.getConnection(sqlJdbcUrl, sqlUser, sqlPass);
                //debugPrint(debug, "SQL Server connection succeeded");
                //Statement stmt = conn.createStatement();

                UniFile ufKasFile = uSession.CreateUniFile(u2RefFile);
                debugPrint(debug, "file opened");
                debugPrint(debug, ufKasFile.ToString());

                UniSelectList uslKasList = uSession.CreateUniSelectList(2);

                uslKasList.Select(ufKasFile);
                UniDynArray udaKasRow = uSession.CreateUniDynArray(uslKasList.Next());
                //debugPrint(debug, uslKasList.readList().ToString());
                //while (!uslKasList.IsLastRecordRead)
                while (null != udaKasRow)
                {
                    String usKasKey = udaKasRow.Extract(1).ToString();
                    sqlTable = usKasKey;
                    debugPrint(debug, usKasKey);
                    //debugPrint(debug, usKasKey.ToString() + ": " + sqlTable);
                    sqlStmt = "IF EXISTS (SELECT * FROM sys.tables WHERE OBJECT_ID('dbo.[" + sqlTable + "]') IS NOT NULL) DROP TABLE dbo.[" + sqlTable + "]";
                    //debugPrint(debug, sqlStmt);
                    //stmt.executeUpdate(sqlStmt);
                    sqlStmt = "CREATE TABLE dbo.[" + sqlTable + "] (ID varchar(255) NOT NULL,LOC smallint NOT NULL,SEQ smallint NOT NULL,VAL varchar(8000) NOT NULL)";
                    //debugPrint(debug, sqlStmt);
                    //stmt.executeUpdate(sqlStmt);

                    /*
                    //{{ int valIdx = 1; int fldIdx = 1;
                    for (int fldIdx = 1; fldIdx <= udaKasRow.dcount(); fldIdx++) {
                        for (int valIdx = 1; valIdx <= udaKasRow.dcount(fldIdx); valIdx++) {
                            UniString usValue = udaKasRow.extract(fldIdx, valIdx) ;
                            debugPrint(debug, usValue.ToString());
                        }
                    }
                    * /

                    //UniFile ufTblFile = uSession.open(usKasKey);
                    //debugPrint(debug, "file opened");
                    ////debugPrint(debug, usKasKey.ToString());

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
                    //            sqlStmt = "INSERT [" + sqlTable + "] VALUES ('" + usTblKey.ToString().replace("'", "''") + "', " + fldTblIdx + ", " + valTblIdx + ", '" + usTblValue.ToString().replace("'", "''") + "')";

                    //            //debugPrint(debug, sqlStmt);
                    //            //stmt.executeUpdate(sqlStmt);

                    //        }
                    //    }
                    //    usTblKey = uslTblList.next();
                    //}

                    udaKasRow = uSession.CreateUniDynArray(uslKasList.Next());
                }
                /*
                udaKeys = uslKasList.readList();
                //UniDynArray udaValues;
 
                sqlStmt = "TRUNCATE TABLE [" + sqlTable + ']';
                debugPrint(debug, sqlStmt);
                stmt.executeUpdate(sqlStmt);
    	 

                for (int keyIdx = 1;keyIdx < udaKeys.count();keyIdx++) {
                    usKasKey = udaKeys.extract(keyIdx);
                    debugPrint(debug, usKasKey.ToString());
                    if (usKasKey.length() > 0) {
                        debugPrint(debug, udaKeys.count() + " " + usKasKey + usKasKey.length() + " " + keyIdx);
                        sqlStmt = "INSERT [" + sqlTable + "] VALUES ('" + usKasKey + "', 0, 1, '" +  usKasKey + "')";
                        debugPrint(debug, sqlStmt);
                        stmt.executeUpdate(sqlStmt);
                        udaKasRow = new UniDynArray(ufKasFile.read(usKasKey));
                        for (int fldIdx = 1; fldIdx <= udaKasRow.dcount(); fldIdx++) {
                            //usField = udaKasRow.extract(fldIdx) ;
                            //udaValues = new UniDynArray(usField);
                            for (int valIdx = 1; valIdx <= udaKasRow.dcount(fldIdx); valIdx++) {
                                usValue = udaKasRow.extract(fldIdx, valIdx) ;
                                if (usValue.length() > 0) {
                                    //debugPrint(debug, fldIdx + " " + valIdx + " -->" + usValue + "<--");
		        		 
                                    sqlStmt = "INSERT [" + sqlTable+ "] VALUES ('" + usKasKey + "', " + fldIdx + ", " + valIdx + ", '" +  usValue.ToString().replace("'", "''") + "')";
                                    debugPrint(debug, sqlStmt);
                                    stmt.executeUpdate(sqlStmt);
                                }
                            }
                        }
                    }
                    //debugPrint(debug, udaKeys.extract(keyIdx));
                }
                * /
                ufKasFile.Close();
                debugPrint(debug, "file closed");

                // did we connect?

                debugPrint(debug, "U2 Disconnected.");
                //conn.close();
                debugPrint(debug, "SQL Disconnected.");

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
            */
        }
    }
}
