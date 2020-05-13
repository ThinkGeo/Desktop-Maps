using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;

namespace ThinkGeo.MapSuite.MbTiles
{
    public class MbtilesDatabase
    {
        private string filePath;
        private SQLiteConnection connection;

        private Map map;
        private Images images;
        private Metadata metadata;

        private MbtilesDatabase(string filePath)
        {
            this.filePath = filePath;
            string connectionString = $"Data Source={filePath}";
            connection = new SQLiteConnection(connectionString);
        }

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public Images Images
        {
            get { return images; }
            set { images = value; }
        }

        public Metadata Metadata
        {
            get { return metadata; }
            set { metadata = value; }
        }

        public string FilePath
        {
            get { return filePath; }
        }

        public static MbtilesDatabase OpenDatabase(string fileName)
        {
            MbtilesDatabase database = new MbtilesDatabase(fileName);
            database.Open();

            return database;
        }

        public static void CreateFileDatabase(string filePath)
        {
            if (File.Exists(filePath))
                return;

            string connectionString = $"Data Source={filePath}";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            Collection<string> creatingeTableSqls = new Collection<string>();
            creatingeTableSqls.Add("CREATE TABLE grid_key ( grid_id TEXT, key_name TEXT )");
            creatingeTableSqls.Add("CREATE TABLE grid_utfgrid ( grid_id TEXT, grid_utfgrid BLOB )");
            creatingeTableSqls.Add("CREATE TABLE images(tile_data,tile_id TEXT)");
            creatingeTableSqls.Add("CREATE TABLE keymap ( key_name TEXT, key_json TEXT )");
            creatingeTableSqls.Add("CREATE TABLE map( zoom_level INTEGER, tile_column INTEGER, tile_row INTEGER, tile_id TEXT, grid_id TEXT, updated_at INTEGER )");
            creatingeTableSqls.Add("CREATE TABLE metadata ( name text, value text )");

            SQLiteCommand command = new SQLiteCommand(connection);
            foreach (string creatingTableSql in creatingeTableSqls)
            {
                command.CommandText = creatingTableSql;
                command.ExecuteNonQuery();
            }

            Collection<string> creatingIndexSqls = new Collection<string>();
            creatingIndexSqls.Add("CREATE UNIQUE INDEX grid_key_lookup ON grid_key (grid_id, key_name)");
            creatingIndexSqls.Add("CREATE UNIQUE INDEX grid_utfgrid_lookup ON grid_utfgrid (grid_id)");
            creatingIndexSqls.Add("CREATE UNIQUE INDEX keymap_lookup ON keymap (key_name)");
            creatingIndexSqls.Add("CREATE UNIQUE INDEX name ON metadata (name)");
            creatingIndexSqls.Add("CREATE INDEX map_index ON map(zoom_level, tile_column, tile_row, tile_id);");
            creatingIndexSqls.Add("CREATE UNIQUE INDEX images_id ON images(tile_id);");
            foreach (string creatingIndexSql in creatingIndexSqls)
            {
                command.CommandText = creatingIndexSql;
                command.ExecuteNonQuery();
            }

            Collection<string> creatingViewSqls = new Collection<string>();
            creatingViewSqls.Add("CREATE VIEW grid_data AS SELECT map.zoom_level AS zoom_level, map.tile_column AS tile_column, map.tile_row AS tile_row, keymap.key_name AS key_name, keymap.key_json AS key_json FROM map JOIN grid_key ON map.grid_id = grid_key.grid_id JOIN keymap ON grid_key.key_name = keymap.key_name");
            creatingViewSqls.Add("CREATE VIEW grids AS SELECT map.zoom_level AS zoom_level, map.tile_column AS tile_column, map.tile_row AS tile_row, grid_utfgrid.grid_utfgrid AS grid FROM map JOIN grid_utfgrid ON grid_utfgrid.grid_id = map.grid_id");
            creatingViewSqls.Add("CREATE VIEW tiles AS SELECT map.zoom_level AS zoom_level, map.tile_column AS tile_column, map.tile_row AS tile_row, images.tile_data AS tile_data, map.updated_at AS updated_at FROM map JOIN images ON images.tile_id = map.tile_id");
            foreach (string creatingViewSql in creatingViewSqls)
            {
                command.CommandText = creatingViewSql;
                command.ExecuteNonQuery();
            }

            command.Reset();
            command.Dispose();
        }

        public void Close()
        {
            connection.Close();
        }

        private void Open()
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);

            map = new Map(connection);
            images = new Images(connection);
            metadata = new Metadata(connection);
        }
    }
}
