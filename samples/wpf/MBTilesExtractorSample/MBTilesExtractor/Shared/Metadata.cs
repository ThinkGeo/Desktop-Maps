using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ThinkGeo.MapSuite.MbTiles
{
    public class Metadata : Table
    {
        public static readonly string MetadataValueColumnName = "value";
        public static readonly string MetadataNameColumnName = "name";

        public Metadata(SQLiteConnection connection)
            : base(connection)
        {
            TableName = "metadata";
        }

        public override bool Delete(IEnumerable<Entry> entries)
        {
            bool result = true;
            try
            {
                string updateSqlStatement = $"DELETE FROM {TableName} WHERE name = @name";
                SQLiteCommand command = new SQLiteCommand(updateSqlStatement, connection);
                command.Parameters.Add("@name", DbType.String);
                command.Prepare();
                foreach (MetadataEntry entry in entries)
                {
                    command.Parameters["@name"].Value = entry.Name;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                result = false;
            }

            return result;
        }

        public override bool Insert(IEnumerable<Entry> entries)
        {
            bool result = true;
            string insertSqlStatement = $"INSERT INTO {TableName} (name, value) VALUES (@name, @value);";
            SQLiteCommand command = new SQLiteCommand(insertSqlStatement, connection);
            command.Parameters.Add("@name", DbType.String);
            command.Parameters.Add("@value", DbType.String);
            command.Prepare();
            IDbTransaction dbTransaction = connection.BeginTransaction();
            try
            {
                foreach (MetadataEntry entry in entries)
                {
                    command.Parameters["@name"].Value = entry.Name;
                    command.Parameters["@value"].Value = entry.Value;
                    command.ExecuteNonQuery();
                }

                dbTransaction.Commit();
            }
            catch
            {
                dbTransaction.Rollback();
                result = false;
            }

            return result;
        }

        public override bool NextPage()
        {
            bool result = true;
            SQLiteDataReader dataReader = null;
            try
            {
                Entries.Clear();

                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = $"SELECT * FROM {TableName} LIMIT {Cursor},{Cursor + EntryLimit}";
                dataReader = command.ExecuteReader();
                if (!dataReader.HasRows)
                    result = false;
                while (dataReader.Read())
                {
                    string name = (string)dataReader["name"];
                    string value = (string)dataReader["value"];
                    Entries.Add(new MetadataEntry() { Name = name, Value = value });
                    Cursor++;
                }

                if (Cursor >= EntryCount)
                    result = false;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public override List<Entry> Query(string sqlString)
        {
            List<Entry> result = new List<Entry>();
            SQLiteDataReader dataReader = null;

            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = sqlString;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                string name = (string)dataReader["name"];
                string value = (string)dataReader["value"];
                result.Add(new MetadataEntry() { Name = name, Value = value });
            }

            return result;
        }

        public override bool Update(IEnumerable<Entry> entries)
        {
            bool result = true;
            try
            {
                string updateSqlStatement = $"UPDATE {TableName} SET value = @value WHERE name = @name";
                SQLiteCommand command = new SQLiteCommand(updateSqlStatement, connection);
                command.Parameters.Add("@name", DbType.String);
                command.Parameters.Add("@value", DbType.String);
                command.Prepare();
                foreach (MetadataEntry entry in entries)
                {
                    command.Parameters["@name"].Value = entry.Name;
                    command.Parameters["@value"].Value = entry.Value;
                    command.ExecuteNonQuery();
                }
            }
            finally
            {
                result = false;
            }

            return result;
        }
    }

    public class MetadataEntry : Entry
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
