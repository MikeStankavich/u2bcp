﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        //private String key;
        //UniDynArray udaRow;

        private List<string> _row = new List<string>();
        private bool _firstRow;

        // constructor
        public U2DataReader(UniSession uSession, string fileName)
        {
            uFile = uSession.CreateUniFile(fileName);
            usl = uSession.CreateUniSelectList(8);
            usl.Select(uFile);
            

            // refactor - set a module bool to indicate first row then in Read method say if first row then first row = false else read
            // read a row to set field count, etc
            _firstRow = false;
            RecordsAffected = 0;

            if (this.Read())
            {
                // reset back to the beginning of the list to align to .NET behavior
                //usl.ClearList();
                //usl.Select(uFile);
                _firstRow = true;
                // parse/transform the row
            }   
        }
        ~U2DataReader()
        {
            //this.Close();
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

        // stop retrieving rows after reaching this limit. if zero retrieve all rows
        public int Limit
        {
            get;set;
        }

        // Translate retrieved data to requested format
        public string Format
        {
            get; set;
        }


        public bool Read()
        {

            if (_firstRow)
            {
                _firstRow = false;
            }
            else
            {                
                string key = "";

                while (!usl.LastRecordRead && "" == key && !_firstRow)
                    key = usl.Next();

                if ("" != key)
                {
                    UniDynArray udaRow = uFile.Read(key);

                    _row.Clear();

                    _row.Add(key);

                    // if format is rawcol
                    _row.Add(udaRow.ToString());

                    /*
                // string data = "";
                    for (int i = 0; i < udaRow.Count(); i++)
                    {
                        //_row.Add(udaRow.Extract(i).ToString());
                        data += udaRow.Extract(i).ToString();
                    }
                    */

                    /*
try
{

    if (0 == i)
        value = key;
    else
    {
        var fld = udaRow.Extract(i).ToString();
        if ("" != fld)
        {
            var xf = new XElement("fld" / *, new XAttribute("loc", i) * /);
            for (var v = 1; v <= udaRow.Dcount(i); v++)
            {
                / *
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
                * /

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
 *     */
                    RecordsAffected++;
                }
            }

            return !usl.LastRecordRead && (Limit == 0 || (RecordsAffected) <= Limit);
        }

        public int RecordsAffected
        {
            get;set;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            // get { return udaRow.Dcount() + 1; }
            get { return _row.Count; }
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
            return _row[i];
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
