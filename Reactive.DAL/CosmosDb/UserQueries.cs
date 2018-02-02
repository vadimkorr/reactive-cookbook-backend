using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Reactive.Models.DbModels;
using Reactive.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive.DAL.CosmosDb
{
    public class UserQueries : IUserQueries<ApplicationUser>
    {
        private DocumentClient _client;
        private string _db;
        private string _collection;
        public UserQueries(DocumentClient client, string databaseName, string collectionName) {
            _client = client;
            _db = databaseName;
            _collection = collectionName;
        }

        public Task<bool> CreateAsync(ApplicationUser user)
        {
            Task<ResourceResponse<Document>> result = _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(_db, _collection), user);
            return Task.FromResult(result.IsCompletedSuccessfully);
        }

        public Task<bool> DeleteAsync(ApplicationUser user)
        {
            Task<ResourceResponse<Document>> result = _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_db, _collection, user.Id.ToString()));
            return Task.FromResult(result.IsCompletedSuccessfully);
        }

        public Task<ApplicationUser> FindByEmailAsync(string normalizedEmail)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<ApplicationUser> userQuery = _client.CreateDocumentQuery<ApplicationUser>(
                UriFactory.CreateDocumentCollectionUri(_db, _collection), queryOptions)
                .Where(f => f.NormalizedEmail == normalizedEmail);

            ApplicationUser au = userQuery.AsEnumerable().FirstOrDefault();
            return Task.FromResult(au);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<ApplicationUser> userQuery = _client.CreateDocumentQuery<ApplicationUser>(
                UriFactory.CreateDocumentCollectionUri(_db, _collection), queryOptions)
                .Where(f => f.Id.ToString() == userId);

            ApplicationUser au = userQuery.AsEnumerable().FirstOrDefault();
            return Task.FromResult(au);
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            IQueryable<ApplicationUser> userQuery = _client.CreateDocumentQuery<ApplicationUser>(
                    UriFactory.CreateDocumentCollectionUri(_db, _collection), queryOptions)
                    .Where(f => f.NormalizedUserName == normalizedUserName);

            ApplicationUser au = userQuery.AsEnumerable().FirstOrDefault();
            return Task.FromResult(au);
        }

        public Task<bool> UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
