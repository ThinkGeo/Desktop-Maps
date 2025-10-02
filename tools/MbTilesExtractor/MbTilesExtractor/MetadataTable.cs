using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace MBTilesExtractor
{
    public class MetadataTable
    {
        private readonly SqliteConnection _connection;

        public List<MetadataEntry> Entries { get; } = new List<MetadataEntry>();

        private const string TableName = "metadata";
        private const string ColName = "name";
        private const string ColValue = "value";

        public MetadataTable(SqliteConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        /// <summary>
        /// Inserts metadata entries into the metadata table within a single transaction.
        /// Returns true if successful, false if any error occurs.
        /// </summary>
        public bool Insert(IEnumerable<MetadataEntry> entries)
        {
            if (entries == null) return false;

            const string insertSql = $"INSERT INTO {TableName} ({ColName}, {ColValue}) VALUES (@name, @value);";

            using var transaction = _connection.BeginTransaction();
            using var command = new SqliteCommand(insertSql, _connection, transaction);
            var nameParam = command.Parameters.Add("@name", SqliteType.Text);
            var valueParam = command.Parameters.Add("@value", SqliteType.Text);

            try
            {
                foreach (var entry in entries)
                {
                    nameParam.Value = entry.Name ?? string.Empty;
                    valueParam.Value = entry.Value ?? string.Empty;
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Reads all metadata entries from the table into the Entries list.
        /// </summary>
        public void ReadAllEntries()
        {
            Entries.Clear();

            const string selectSql = $"SELECT {ColName}, {ColValue} FROM {TableName};";

            using var command = new SqliteCommand(selectSql, _connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string name = reader.GetString(0);
                string value = reader.GetString(1);
                Entries.Add(new MetadataEntry { Name = name, Value = value });
            }
        }
    }
}
