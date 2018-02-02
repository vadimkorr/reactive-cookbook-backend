using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Reactive.DAL.Interfaces;
using System;

namespace Reactive.DAL.CosmosDb
{
    public class CosmosDbClient
    {
        DocumentClient _client;
        public DocumentClient get()
        {
            return _client;
        }
    
        public CosmosDbClient(string endpointUri, string primaryKey)
        { 
            try
            {
                _client = new DocumentClient(new Uri(endpointUri), primaryKey);
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
        }

        public ResourceResponse<Database> CreateDatabase(string db)
        {
            ResourceResponse<Database> result = _client
                .CreateDatabaseIfNotExistsAsync(new Database { Id = db }).Result;
            return result;
        }

        public ResourceResponse<DocumentCollection> CreateCollection(string db, string collection)
        {
            ResourceResponse<DocumentCollection> result = _client
                .CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(db), new DocumentCollection { Id = collection })
                .Result;
            return result;
        }
    }
}
