using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private UniSession _uSession;
        private UniFile uFile;
        private UniSelectList usl;
        private UniDataSet _uds;

        private string[] _keySample;
        private string[] _keyBlock;
        private List<string> _row = new List<string>();
        private int _blockIdx = 0;
        private int _rowIdx = 0;

        // constructor
        public U2DataReader(UniSession uSession, string fileName)
        {
            _uSession = uSession;

            try
            {
                uFile = _uSession.CreateUniFile(fileName);

                UniCommand uCmd = _uSession.CreateUniCommand();
                uCmd.Command = string.Format("SELECT {0} BY @ID SAMPLED 100", uFile.FileName);
                uCmd.Execute();
                usl = _uSession.CreateUniSelectList(0);
                _keySample = usl.ReadListAsStringArray();

                RecordsAffected = 0;
            }
            catch(UniSessionException e)   // unisession file not exists
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
        ~U2DataReader()
        {
            //this.Close();
        }

        public void Close()
        {
            if (uFile!=null)
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
            if (_keySample == null)
                return false;

            if ((_blockIdx > _keySample.Length) || (Limit > 0 && RecordsAffected >= Limit))
                return false;

            if (0 == _rowIdx)
            {
                UniCommand uCmd = _uSession.CreateUniCommand();

                _keyBlock = null;
                while (_keyBlock == null)
                {
                    uCmd.Command = string.Format("SELECT {0} BY @ID", uFile.FileName);

                    if (_blockIdx > 0)
                        uCmd.Command += string.Format(" WITH @ID >= \"{0}\"",
                            _keySample[_blockIdx - 1].Replace("\"", "\"\""));


                    if (_blockIdx < _keySample.Length)
                    {
                        if (_blockIdx > 0)
                            uCmd.Command += " AND ";
                        else
                            uCmd.Command += " WITH ";

                        uCmd.Command += string.Format("@ID < \"{0}\"", _keySample[_blockIdx].Replace("\"", "\"\""));
                    }

                    uCmd.Execute();
                    usl = _uSession.CreateUniSelectList(0);
                    _keyBlock = usl.ReadListAsStringArray();
                    if (_keyBlock == null) _blockIdx++;
                }
                // Console.Write(_keyBlock.ToString());
                _uds = uFile.ReadRecords(_keyBlock);
            }

            if (_rowIdx < _keyBlock.Length)
            {
                _row.Clear();
  
                _row.Add(_keyBlock[_rowIdx]);
                _row.Add(_uds.GetRecord(_rowIdx).Record.ToString());

                _rowIdx++;
                if (_rowIdx == _keyBlock.Length)
                {
                    _rowIdx = 0;
                    _blockIdx++;
                }
            }

            RecordsAffected++;
    
            return true;




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
            get { return 2; }
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
