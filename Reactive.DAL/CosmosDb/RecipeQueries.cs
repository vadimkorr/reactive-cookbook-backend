using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Reactive.Models.DbModels;
using Reactive.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Reactive.DAL.CosmosDb
{
    public class RecipeQueries : IRecipeQueries
    {
        private DocumentClient _client;
        private string _db;
        private string _collection;
        public RecipeQueries(DocumentClient client, string databaseName, string collectionName) {
            _client = client;
            _db = databaseName;
            _collection = collectionName;
        }

        public Task<IEnumerable<Recipe>> GetRecipesByUserId(Guid userId)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };
            IQueryable<Recipe> recipesQuery = _client.CreateDocumentQuery<Recipe>(
                UriFactory.CreateDocumentCollectionUri(_db, _collection), queryOptions)
                .Where(r => r.UserId == userId);
            return Task.FromResult(recipesQuery.AsEnumerable());
        }

        public Task<bool> Submit(Recipe recipe) {
            Task<ResourceResponse<Document>> result = _client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_db, _collection), recipe);
            return Task.FromResult(result.IsCompletedSuccessfully);
        }
    }
}
