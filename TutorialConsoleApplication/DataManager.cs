using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorialConsoleApplication
{
    public class DataManager
    {
        private CloudStorageAccount storageAccount = null;
        private CloudTableClient tableClient = null;
        private CloudTable table = null;
                

        public DataManager()
        {
            this.storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            this.tableClient = storageAccount.CreateCloudTableClient();            
        }

        public bool CreateTable(string tableName)
        {
            this.table = tableClient.GetTableReference(tableName);
            if (table == null)           
                this.table.Create();              
                return true;            
        }
        
        public bool GetTableReferences(string tableName)
        {            
            this.table = tableClient.GetTableReference(tableName);
            if (table == null)
                return false;
            else
                return true;
        }

        public void AddEntity(CustomerEntity entity)
        {
            TableOperation insertOperation = TableOperation.Insert(entity);
            this.table.Execute(insertOperation);
        }

        public void AddEntities(IList<CustomerEntity> entities)
        {
            TableBatchOperation batchOperation = new TableBatchOperation();
            foreach (var item in entities)
            {
                batchOperation.Insert(item);
            }
            this.table.ExecuteBatch(batchOperation);
        }

        public void GetPartition(string partitionName)
        {
            TableQuery<CustomerEntity> query = new TableQuery<CustomerEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionName));
            foreach (CustomerEntity entity in table.ExecuteQuery(query))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}", entity.PartitionKey, entity.RowKey,
                    entity.Email, entity.PhoneNumber);
            }
        }

        public void GetRangePartition(string partitionName, string val)
        {
            TableQuery<CustomerEntity> rangeQuery = new TableQuery<CustomerEntity>().Where(
            TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionName),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, val)));

            foreach (var item in table.ExecuteQuery(rangeQuery))
            {
                Console.WriteLine("{0}, {1}\t{2}\t{3}", item.PartitionKey, item.RowKey,
                item.Email, item.PhoneNumber);
            }
        }

        public void GetEntityPhone(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(partitionKey, rowKey);
            TableResult retrievedResult = table.Execute(retrieveOperation);

            if (retrievedResult.Result != null)
            {
                Console.WriteLine(((CustomerEntity)retrievedResult.Result).PhoneNumber);
            }
            else
            {
                Console.WriteLine("The phone number could not be retrieved.");
            }
        }

        public void ChangeEntityPhone(string partitionKey, string rowKey, string newPhone)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(partitionKey, rowKey);
            TableResult retrievedResult = table.Execute(retrieveOperation);

            CustomerEntity updateEntity = (CustomerEntity)retrievedResult.Result;
            if (updateEntity != null)
            {
                updateEntity.PhoneNumber = newPhone;
                TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(updateEntity);
                table.Execute(insertOrReplaceOperation);
                Console.WriteLine("Entity updated.");
            }
            else
            {
                Console.WriteLine("Entity could not be retrieved.");
            }
        }

        public void GetEntitiesProperty()
        {
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>().Select(new string[] { "Email" });
            EntityResolver<string> resolver = (pk, rk, ts, props, etag) => props.ContainsKey("Email") ? props["Email"].StringValue : null;
            foreach (string projectedEmail in table.ExecuteQuery(projectionQuery, resolver, null, null))
            {
                Console.WriteLine(projectedEmail);
            }
        }

        public void DeleteEntity(string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(partitionKey, rowKey);
            TableResult retrievedResult = table.Execute(retrieveOperation);
            CustomerEntity deleteEntity = (CustomerEntity)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);                                
                table.Execute(deleteOperation);
                Console.WriteLine("Entity deleted.");
            }
            else
            {
                Console.WriteLine("Could not retrieve the entity.");
            }
        }

        public void DeleteTable()
        {
            table.DeleteIfExists();
        }

        public async Task GetDataAsync()
        {
            TableQuery<CustomerEntity> tableQuery = new TableQuery<CustomerEntity>();
            TableContinuationToken continuationToken = null;
            do
            {
                TableQuerySegment<CustomerEntity> tableQueryResult = await table.ExecuteQuerySegmentedAsync(tableQuery, continuationToken);
                continuationToken = tableQueryResult.ContinuationToken;
                Console.WriteLine("Rows retrieved {0}", tableQueryResult.Results.Count);
            } while (continuationToken != null);
        }
    }
}
