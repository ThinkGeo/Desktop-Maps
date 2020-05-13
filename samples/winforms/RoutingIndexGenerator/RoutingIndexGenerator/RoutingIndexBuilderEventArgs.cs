using System;

namespace RoutingIndexGenerator
{
    public class RoutingIndexBuilderEventArgs : EventArgs
    {
        private int totalRecordAccount;
        private int processedRecordAccount;

        public RoutingIndexBuilderEventArgs()
        { }

        public RoutingIndexBuilderEventArgs(int totalRecordAccount, int processedRecordAccount)
        {
            this.totalRecordAccount = totalRecordAccount;
            this.processedRecordAccount = processedRecordAccount;
        }

        public int TotalRecordCount
        {
            get { return totalRecordAccount; }
            set { totalRecordAccount = value; }
        }

        public int ProcessedRecordCount
        {
            get { return processedRecordAccount; }
            set { processedRecordAccount = value; }
        }
    }
}
