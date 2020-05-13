namespace WorldMapKitDataExtractor
{
    public class View
    {
        public View(string name, string baseTable, string createStatement)
        {
            this.Name = name;
            this.BaseTable = baseTable;
            this.CreateStatement = createStatement;
        }

        public string Name { get; set; }

        public string BaseTable { get; set; }

        public string CreateStatement { get; set; }
    }
}