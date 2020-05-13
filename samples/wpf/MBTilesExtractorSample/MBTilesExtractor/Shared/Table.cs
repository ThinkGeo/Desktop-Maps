using System.Collections.Generic;
using System.Data.SQLite;

namespace ThinkGeo.MapSuite.MbTiles
{
    public abstract class Table
    {
        private int cursor;
        private int entryCount;
        private string tableName;
        private List<Entry> entries;
        protected SQLiteConnection connection;
        protected int EntryLimit = 1000;

        public Table(SQLiteConnection connection)
        {
            this.connection = connection;
            entries = new List<Entry>();
            entryCount = -1;
            cursor = 0;
        }

        public string TableName
        {
            get { return tableName; }
            protected set { tableName = value; }
        }

        public int Cursor
        {
            get { return cursor; }
            set { cursor = value; }
        }

        public int EntryCount
        {
            get
            {
                if (entryCount == -1 && string.IsNullOrEmpty(TableName))
                {
                    SQLiteCommand command = new SQLiteCommand(connection);
                    command.CommandText = $"SELECT COUNT(*) FROM {TableName}";
                    entryCount = command.ExecuteNonQuery();
                }
                return entryCount;
            }
        }

        public List<Entry> Entries
        {
            get { return entries; }
        }

        public abstract bool NextPage();

        public abstract bool Insert(IEnumerable<Entry> entries);

        public abstract bool Update(IEnumerable<Entry> entries);

        public abstract bool Delete(IEnumerable<Entry> entries);

        public abstract List<Entry> Query(string sqlString);
    }

    public abstract class Entry
    { }
}
