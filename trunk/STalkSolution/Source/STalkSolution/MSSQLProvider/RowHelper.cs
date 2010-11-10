using System;
using System.Data.SqlClient;

namespace STalk.MSSQLProvider
{

    public class RowHelper
    {
        private DateTime defaultDate;
        private SqlDataReader reader;

        public RowHelper(SqlDataReader reader)
		{
			this.defaultDate = DateTime.MinValue;
			this.reader = reader;
		}


		public int GetInt32(String column)
		{
			int data = (reader.IsDBNull(reader.GetOrdinal(column))) ? 0 : Convert.ToInt32(reader[column]);
			return data;
		}

        public short GetInt16(String column)
		{
            short data = (reader.IsDBNull(reader.GetOrdinal(column))) ? (short)0 : Convert.ToInt16(reader[column]);
			return data;
		}

        public ushort GetUInt16(String column)
        {
            ushort data = (reader.IsDBNull(reader.GetOrdinal(column))) ? (ushort)0 : Convert.ToUInt16(reader[column]);
            return data;
        }

        public uint GetUInt32(String column)
        {
            uint data = (reader.IsDBNull(reader.GetOrdinal(column))) ? (uint)0 : Convert.ToUInt32(reader[column]);
            return data;
        }

		public float GetFloat(String column)
		{
			float data = (reader.IsDBNull(reader.GetOrdinal(column))) ? 0 : float.Parse(reader[column].ToString());
			return data;
		}

		public bool GetBoolean(String column)
		{
			bool data = (reader.IsDBNull(reader.GetOrdinal(column))) ? false : (bool)reader[column];
			return data;
		}

		public String GetString(String column)
		{
			String data = (reader.IsDBNull(reader.GetOrdinal(column))) ? null : reader[column].ToString();
			return data;
		}

		public DateTime GetDateTime(String column)
		{
			DateTime data = (reader.IsDBNull(reader.GetOrdinal(column))) ? defaultDate : (DateTime)reader[column];
			return data;
		}

        public decimal GetDecimal(string column)
        {
            decimal data = (reader.IsDBNull(reader.GetOrdinal(column))) ? 0 : (decimal)reader[column];
            return data;
        }

		public bool Read()
		{
            try
            {
                return this.reader.Read();
            }
            catch {
                return false;
            }
		}
    }
}
