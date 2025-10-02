using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace MBTilesExtractor
{
    public class TilesTable
    {
        private readonly SqliteConnection _connection;
        public string TableName { get; }

        public static readonly string ZoomLevelColumnName = "zoom_level";
        public static readonly string TileColColumnName = "tile_column";
        public static readonly string TileRowColumnName = "tile_row";
        public static readonly string TileDataColumnName = "tile_data";

        public TilesTable(SqliteConnection connection, string tableName = "tiles")
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            TableName = tableName;
        }

        /// <summary>
        /// Inserts a collection of tile records in batches (1000 rows per transaction).
        /// </summary>
        public bool Insert(IEnumerable<TilesEntry> entries, int batchSize = 1000)
        {
            // Prepare one command and reuse its parameters for speed.
            using var command = _connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO {TableName} ({ZoomLevelColumnName}, {TileColColumnName}, {TileRowColumnName}, {TileDataColumnName}) " +
                $"VALUES ($z, $x, $y, $d);";
            command.Parameters.Add("$z", SqliteType.Integer);
            command.Parameters.Add("$x", SqliteType.Integer);
            command.Parameters.Add("$y", SqliteType.Integer);
            command.Parameters.Add("$d", SqliteType.Blob);

            SqliteTransaction tx = null;
            int inTxCount = 0;

            try
            {
                // Start the first transaction and bind it to the command.
                tx = _connection.BeginTransaction();
                command.Transaction = tx;

                foreach (var e in entries)
                {
                    command.Parameters["$z"].Value = e.ZoomLevel;
                    command.Parameters["$x"].Value = e.TileColumn;
                    command.Parameters["$y"].Value = e.TileRow;
                    command.Parameters["$d"].Value = e.TileData;

                    command.ExecuteNonQuery();
                    inTxCount++;

                    // Commit per batch, then start a fresh transaction.
                    if (inTxCount >= batchSize)
                    {
                        tx.Commit();
                        tx.Dispose();

                        tx = _connection.BeginTransaction();
                        command.Transaction = tx;

                        inTxCount = 0;
                    }
                }

                // Commit the tail batch (if any).
                tx.Commit();
                tx.Dispose();
                tx = null; // mark as completed so catch/finally won't try to rollback

                return true;
            }
            catch
            {
                // Roll back only if the transaction is still active.
                if (tx?.Connection != null)
                {
                    try { tx.Rollback(); } catch { /* ignore to avoid masking original error */ }
                }
                return false;
            }
        }


        /// <summary>
        /// Executes a SELECT query and returns matching tile records.
        /// </summary>
        public List<TilesEntry> Query(string sql)
        {
            var results = new List<TilesEntry>();

            using var command = new SqliteCommand(sql, _connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                results.Add(new TilesEntry
                {
                    ZoomLevel = reader.GetInt64(reader.GetOrdinal(ZoomLevelColumnName)),
                    TileColumn = reader.GetInt64(reader.GetOrdinal(TileColColumnName)),
                    TileRow = reader.GetInt64(reader.GetOrdinal(TileRowColumnName)),
                    TileData = (byte[])reader[TileDataColumnName]
                });
            }

            return results;
        }
    }
}
    