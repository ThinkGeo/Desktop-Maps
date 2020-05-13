using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace ThinkGeo.MapSuite.MbTiles
{
    public class Map : Table
    {
        public static readonly string ZoomLevelColumnName = "zoom_level";
        public static readonly string TileColColumnName = "tile_column";
        public static readonly string TileRowColumnName = "tile_row";
        public static readonly string TileIdColumnName = "tile_id";
        public static readonly string GridIdColumnName = "grid_id";
        public static readonly string UpdatedAtColumnName = "updated_at";

        public Map(SQLiteConnection connection)
            : base(connection)
        {
            TableName = "map";
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
                foreach (MapEntry entry in entries)
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

            string insertSqlStatement = $"INSERT INTO {TableName} ({ZoomLevelColumnName},{TileColColumnName},{TileRowColumnName},{TileIdColumnName},{GridIdColumnName},{UpdatedAtColumnName}) VALUES (@{ZoomLevelColumnName}, @{TileColColumnName}, @{TileRowColumnName}, @{TileIdColumnName}, @{GridIdColumnName}, @{UpdatedAtColumnName});";

            SQLiteCommand command = new SQLiteCommand(insertSqlStatement, connection);
            command.Parameters.Add($"@{ZoomLevelColumnName}", DbType.Int32);
            command.Parameters.Add($"@{TileColColumnName}", DbType.Int32);
            command.Parameters.Add($"@{TileRowColumnName}", DbType.Int32);
            command.Parameters.Add($"@{TileIdColumnName}", DbType.String);
            command.Parameters.Add($"@{GridIdColumnName}", DbType.String);
            command.Parameters.Add($"@{UpdatedAtColumnName}", DbType.Int32);
            command.Prepare();
            IDbTransaction dbTransaction = connection.BeginTransaction();
            try
            {
                foreach (MapEntry entry in entries)
                {
                    command.Parameters[$"@{ZoomLevelColumnName}"].Value = entry.ZoomLevel;
                    command.Parameters[$"@{TileColColumnName}"].Value = entry.TileColumn;
                    command.Parameters[$"@{TileRowColumnName}"].Value = entry.TileRow;
                    command.Parameters[$"@{TileIdColumnName}"].Value = entry.TileId;
                    command.Parameters[$"@{GridIdColumnName}"].Value = entry.GridId;
                    if (entry.UpdatedAt != 0)
                        command.Parameters[$"@{UpdatedAtColumnName}"].Value = entry.UpdatedAt;
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
                    int zoomLevel = (int)dataReader[ZoomLevelColumnName];
                    int tileCol = (int)dataReader[TileColColumnName];
                    int tileRow = (int)dataReader[TileRowColumnName];
                    string tileId = (string)dataReader[TileIdColumnName];
                    string gridId = (string)dataReader[GridIdColumnName];
                    int updateDate = (int)dataReader[UpdatedAtColumnName];
                    Entries.Add(new MapEntry() { ZoomLevel = zoomLevel, TileColumn = tileCol, TileRow = tileRow, TileId = tileId, GridId = gridId, UpdatedAt = updateDate });
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

            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = sqlString;
            SQLiteDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                MapEntry newEntry = new MapEntry();
                newEntry.ZoomLevel = (long)dataReader[ZoomLevelColumnName];
                newEntry.TileColumn = (long)dataReader[TileColColumnName];
                newEntry.TileRow = (long)dataReader[TileRowColumnName];
                newEntry.TileId = (string)dataReader[TileIdColumnName];

                if (!dataReader.IsDBNull(dataReader.GetOrdinal(GridIdColumnName)))
                    newEntry.GridId = (string)dataReader[GridIdColumnName];

                if (!dataReader.IsDBNull(dataReader.GetOrdinal(UpdatedAtColumnName)))
                    newEntry.UpdatedAt = (long)dataReader[UpdatedAtColumnName];

                result.Add(newEntry);
                Cursor++;
            }

            return result;
        }

        public override bool Update(IEnumerable<Entry> entries)
        {
            bool result = true;
            try
            {
                string updateSqlStatement = $"UPDATE {TableName} SET {ZoomLevelColumnName} = @{ZoomLevelColumnName},{TileColColumnName} = @{TileColColumnName},{TileRowColumnName} = @{TileRowColumnName},{GridIdColumnName} = @{GridIdColumnName},{UpdatedAtColumnName} = @{UpdatedAtColumnName} WHERE {TileIdColumnName} = @{TileIdColumnName}";
                SQLiteCommand command = new SQLiteCommand(updateSqlStatement, connection);
                command.Parameters.Add($"@{ZoomLevelColumnName}", DbType.Int32);
                command.Parameters.Add($"@{TileColColumnName}", DbType.Int32);
                command.Parameters.Add($"@{TileRowColumnName}", DbType.Int32);
                command.Parameters.Add($"@{TileIdColumnName}", DbType.String);
                command.Parameters.Add($"@{GridIdColumnName}", DbType.String);
                command.Parameters.Add($"@{UpdatedAtColumnName}", DbType.Int32);
                command.Prepare();
                foreach (MapEntry entry in entries)
                {
                    command.Parameters[$"@{ZoomLevelColumnName}"].Value = entry.ZoomLevel;
                    command.Parameters[$"@{TileColColumnName}"].Value = entry.TileColumn;
                    command.Parameters[$"@{TileRowColumnName}"].Value = entry.TileRow;
                    command.Parameters[$"@{TileIdColumnName}"].Value = entry.TileId;
                    command.Parameters[$"@{GridIdColumnName}"].Value = entry.GridId;
                    command.Parameters[$"@{UpdatedAtColumnName}"].Value = entry.UpdatedAt;
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

    public class MapEntry : Entry
    {
        public long ZoomLevel { get; set; }
        public long TileColumn { get; set; }
        public long TileRow { get; set; }
        public string TileId { get; set; }
        public string GridId { get; set; }
        public long UpdatedAt { get; set; }

        public MapEntry()
        { }
    }
}
