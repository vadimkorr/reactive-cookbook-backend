using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;

namespace StorageProviders.CosmosDb
{
    public class CosmosDbClient : IDbClient<DocumentClient> 
    {
        DocumentClient _client;
        public DocumentClient Client
        {
            get
            {
                return _client;
            }
        }

        public CosmosDbClient(string endpointUri, string primaryKey) { 
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
    }
}
