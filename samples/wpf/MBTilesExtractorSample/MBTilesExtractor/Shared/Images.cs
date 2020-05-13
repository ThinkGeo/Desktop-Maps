using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ThinkGeo.MapSuite.MbTiles
{
    public class Images : Table
    {
        public static readonly string TileDataColumnName = "tile_data";
        public static readonly string TileIdColumnName = "tile_id";

        public Images(SQLiteConnection connection)
            : base(connection)
        {
            TableName = "images";
        }

        public override bool Delete(IEnumerable<Entry> entries)
        {
            bool result = true;
            try
            {
                string updateSqlStatement = $"DELETE FROM {TableName} WHERE {TileIdColumnName} = @{TileIdColumnName}";
                SQLiteCommand command = new SQLiteCommand(updateSqlStatement, connection);
                command.Parameters.Add($"@{TileIdColumnName}", DbType.String);
                command.Prepare();
                foreach (ImageEntry entry in entries)
                {
                    command.Parameters[$"@{TileIdColumnName}"].Value = entry.TileId;
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

            string insertSqlStatement = $"REPLACE INTO {TableName} ({TileDataColumnName}, {TileIdColumnName}) VALUES (@{TileDataColumnName}, @{TileIdColumnName});";
            SQLiteCommand command = new SQLiteCommand(insertSqlStatement, connection);
            command.Parameters.Add($"@{TileDataColumnName}", DbType.Binary);
            command.Parameters.Add($"@{TileIdColumnName}", DbType.String);
            command.Prepare();

            IDbTransaction dbTransaction = connection.BeginTransaction();
            try
            {
                foreach (ImageEntry entry in entries)
                {
                    command.Parameters[$"@{TileDataColumnName}"].Value = entry.TileData;
                    command.Parameters[$"@{TileIdColumnName}"].Value = entry.TileId;
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
                while (dataReader.Read())
                {
                    Cursor++;
                    string tileId = (string)dataReader[TileIdColumnName];
                    byte[] tileData = (byte[])dataReader[TileDataColumnName];
                    Entries.Add(new ImageEntry() { TileId = tileId, TileData = tileData });
                }

                if (Cursor >= EntryCount) result = false;
            }
            finally
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
                Cursor++;
                string tileId = (string)dataReader[TileIdColumnName];
                byte[] tileData = (byte[])dataReader[TileDataColumnName];
                result.Add(new ImageEntry() { TileId = tileId, TileData = tileData });
            }

            return result;
        }

        public override bool Update(IEnumerable<Entry> entries)
        {
            bool result = true;
            try
            {
                string updateSqlStatement = $"UPDATE {TableName} SET {TileDataColumnName} = @{TileDataColumnName} WHERE {TileIdColumnName} = @{TileIdColumnName}";
                SQLiteCommand command = new SQLiteCommand(updateSqlStatement, connection);
                command.Parameters.Add($"@{TileDataColumnName}", DbType.Binary);
                command.Parameters.Add($"@{TileIdColumnName}", DbType.String);
                command.Prepare();
                foreach (ImageEntry entry in entries)
                {
                    command.Parameters[$"@{TileDataColumnName}"].Value = entry.TileData;
                    command.Parameters[$"@{TileIdColumnName}"].Value = entry.TileId;
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

    public class ImageEntry : Entry
    {
        public string TileId { get; set; }
        public byte[] TileData { get; set; }

        public ImageEntry()
        { }
    }
}
