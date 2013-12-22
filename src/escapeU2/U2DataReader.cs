using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml.Linq;
using IBMU2.UODOTNET;

namespace escapeU2
{
    public class U2DataReader : IDataReader
    {
        private UniFile uFile;
        private UniSelectList usl;
        private String key;
        UniDynArray udaRow;

        private String[] _row;

        // constructor
        public U2DataReader(UniSession uSession, string fileName)
        {
            uFile = uSession.CreateUniFile(fileName);
            usl = uSession.CreateUniSelectList(8);
            usl.Select(uFile);

            // refactor - set a module bool to indicate first row then in Read method say if first row then first row = false else read
            // read a row to set field count, etc
            if (this.Read())
            {
                // reset back to the beginning of the list to align to .NET behavior
                usl.ClearList();
                usl.Select(uFile);
            }   
        }
        ~U2DataReader()
        {
            //this.Close();
        }

        private void ParseRow()
        {
            
        }
        public void Close()
        {
            if (uFile.IsFileOpen)
                uFile.Close();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            key = "";
            while (!usl.LastRecordRead && "" == key)
                key = usl.Next();

            if ("" != key)
                udaRow = uFile.Read(key);

            return !usl.LastRecordRead;
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            get { return udaRow.Dcount() + 1; }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            return System.Type.GetType("String");
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            if (0 == i)
                return "id";
            else
                return string.Format("loc{0}", i);
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public object GetValue(int i)
        {
            String value = null;

            try
            {

                if (0 == i)
                    value = key;
                else
                {
                    var fld = udaRow.Extract(i).ToString();
                    if ("" != fld)
                    {
                        var xf = new XElement("fld" /*, new XAttribute("loc", i) */);
                        for (var v = 1; v <= udaRow.Dcount(i); v++)
                        {
                            /*
                            XElement xv = new XElement("val", new XAttribute("loc", v));
                            string val = udaRow.Extract(i, v).ToString();
                            if ("" != val)
                            {
                                for (int s = 1; s <= udaRow.Dcount(i, v); s++)
                                {
                                    xv.Add(new XElement("sub", new XAttribute("loc", s), udaRow.Extract(i, v, s).ToString()));
                                }
                            }
                            xf.Add(xv);
                            */

                            var val = udaRow.Extract(i, v).ToString();


                            //replace control characters that are invalid in xml with empty string
                            var re = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
                            val = Regex.Replace(val, re, "");

                            // replace text and subtext remarks with carriage return
                            re = @"[\xFB\xFC]";
                            val = Regex.Replace(val, re, "\n");
                            //const string TM_CHAR = "\xFB";
                            //const string SM_CHAR = "\xFC";
                            //val.Replace(SM_CHAR, "\n");
                            //val.Replace(TM_CHAR, "\n");
                            
                            xf.Add(new XElement("val", new XAttribute("loc", v), val));
                        }
                        value = xf.ToString();
                    }
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.ToString());
            }
            if (null == value)
                return DBNull.Value;
            else
                return value;
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }
    }
}
