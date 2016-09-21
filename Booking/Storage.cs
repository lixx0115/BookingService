using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager 
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
namespace Booking
{
    public class Storage
    {
        private readonly CloudTable table;

        public Storage(string tableName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=bookingit;AccountKey=Zvpues/XTk7iNW9dFczvLSEsHsbeyo/XmXniOn0xoG3jfS+LGJIZ7NcFubHTG/qhlXKutxRVr6cgoJEj7pBRbA==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            this.table =  tableClient.GetTableReference("tableName");
            this.table.CreateIfNotExists();
        }

        public void Save(Guid id, string payload)
        {
            var storeEntity = new StoreEntity(id.ToString());
            storeEntity.PayLoad = payload;
            TableOperation updateOperation = TableOperation.InsertOrReplace(storeEntity);
            table.Execute(updateOperation);
            
        }

        public string Get( Guid id)
        {
            var sid = id.ToString();
            var pk = sid.Substring(0, 3);
            var retrieveOperation = TableOperation.Retrieve<StoreEntity>(pk, sid);
            TableResult retrievedResult = table.Execute(retrieveOperation);
            if (retrievedResult.Result != null)
                return ((StoreEntity)retrievedResult.Result).PayLoad;

            return string.Empty;
        }

        private class StoreEntity : TableEntity
        {
            public StoreEntity()
            {

            }
            public StoreEntity(string id) { this.PartitionKey = id.Substring(0, 3); ; this.RowKey = id; }

            public string PayLoad { get; set; }

        }
    }


}
