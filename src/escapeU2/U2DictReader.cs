using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Xml.Linq;
using IBMU2.UODOTNET;

namespace escapeU2
{
    public class U2DictReader : IDataReader
    {
        private UniDictionary uFile;
        private UniSelectList usl;
        private UniDataSet uds;
        private string[] _keys;
        private int _rowIdx = 0;

        private List<string> _row = new List<string>();
        private bool _firstRow;
        /*
        private void ReadDataset()
        {
            _keys = new string[1000];

            for (int i = 0; i < 1000; i++)
            {
                string key = "";

                while (!usl.LastRecordRead && "" == key)
                    key = usl.Next();

                //if ("" != key)
                keys[i] = key;

                Console.WriteLine(keys[i]);

                if (usl.LastRecordRead)
                {
                    Array.Resize(ref keys, i + 1);
                    break;
                }
            }


            uds = uFile.ReadRecords(_keys);

        }
        */
        // constructor
        public U2DictReader(UniSession uSession, string fileName)
        {
            uFile = uSession.CreateUniDictionary(fileName);
            //usl = uSession.CreateUniSelectList(8);
            //usl.Select(uFile);

            /*
            string cmdTxt = string.Format("SELECT {0} BY @ID SAMPLED 100", fileName);
            UniCommand uCmd = uSession.CreateUniCommand(); //By default the results of this will become select list 0.
            uCmd.Command = cmdTxt;
            uCmd.Execute();
            */
            usl = uSession.CreateUniSelectList(0);
            usl.Select(uFile);
            string[] keys = usl.ReadListAsStringArray();

            /*
            foreach (string str in keys)
                Console.WriteLine(str);
            */

            RecordsAffected = 0;
            // have to read the first row to get the field count. We use _firstRow to tell Read that first row already read
            //_firstRow = this.Read();

            uds = uFile.ReadRecords(keys);
            if (uds.RowCount > 0)
                _rowIdx = 0;
            
        }
        ~U2DictReader()
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
            get;
            set;
        }

        // Translate retrieved data to requested format
        public string Format
        {
            get;
            set;
        }


        public bool Read()
        {
            if (_rowIdx <= uds.RowCount)
            {
                UniRecord urRow = uds.GetRecord(_rowIdx);

                _row.Clear();

                _row.Add(urRow.RecordID);

                for (int colIdx = 1; colIdx < 8; colIdx++)
                    _row.Add(urRow.Record.Extract(colIdx).ToString());
                
                RecordsAffected++;
                _rowIdx++;
            }

            return (_rowIdx <= uds.RowCount);
        }

        public int RecordsAffected
        {
            get;
            set;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            get
            {
                return 8;
            }
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
