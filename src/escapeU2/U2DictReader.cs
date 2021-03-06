﻿using System;
using System.Collections.Generic;
using System.Data;
using IBMU2.UODOTNET;

namespace escapeU2
{
    public class U2DictReader : IDataReader
    {
        private UniSession _uSession;
        private UniDictionary uFile;
        private UniSelectList usl;
        private UniDataSet _uds;
        private int _rowIdx = 0;

        private List<string> _row = new List<string>();

        // constructor
        public U2DictReader(UniSession uSession, string fileName)
        {
            _uSession = uSession;

            try
            {
                uFile = _uSession.CreateUniDictionary(fileName);

                usl = _uSession.CreateUniSelectList(0);
                usl.Select(uFile);
                string[] keys = usl.ReadListAsStringArray();

                _uds = uFile.ReadRecords(keys);
            }
            catch (UniSessionException e)   // unisession file not exists
            {
                if (e.ErrorCode == 14002)
                {
                    Console.WriteLine("U2 file not found");
                }
                else
                {
                    // dont know, so rethrow
                    throw;
                }
            }
        }
        ~U2DictReader()
        {
            //this.Close();
        }

        public void Close()
        {
            if (uFile != null)
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
            if (_uds == null) 
                return false;

            if (_rowIdx < _uds.RowCount)
            {
                UniRecord urRow = _uds.GetRecord(_rowIdx);

                _row.Clear();

                _row.Add(urRow.RecordID);

                for (int colIdx = 1; colIdx < 8; colIdx++)
                    _row.Add(urRow.Record.Extract(colIdx).ToString());
                
                _rowIdx++;

                return true;
            }
            else
            {
                return false;
            }
        }

        public int RecordsAffected
        {
            get { return _rowIdx; }
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
