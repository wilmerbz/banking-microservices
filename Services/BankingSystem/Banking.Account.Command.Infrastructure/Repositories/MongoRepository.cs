using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.Account.Command.Application.Models;
using Banking.Account.Command.Domain.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Command.Infrastructure.Repositories
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {

        protected readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IOptions<MongoSettings> options)
        {

            var client = new MongoClient(options.Value.ConnectionString);
            var db = client.GetDatabase(options.Value.Database);

            _collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));

        }

        protected string GetCollectionName(Type type)
        {
            var collectionAttribute = type.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault() as BsonCollectionAttribute;
            return collectionAttribute.CollectionName;
        }

        public async Task DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(document => document.Id, id);

            await _collection.FindOneAndDeleteAsync(filter);

        }

        public async Task<IEnumerable<TDocument>> GetAll()
        {
            var allDocuments = await _collection.Find(p => true).ToListAsync();
            return allDocuments;
        }

        public async Task<TDocument> GetById(string id)
        {
            FilterDefinition<TDocument> filter = GetDocumentByIdFilter(id);
            var document = await _collection.Find(filter).SingleOrDefaultAsync();

            return document;

        }

        private static FilterDefinition<TDocument> GetDocumentByIdFilter(string id)
        {
            return Builders<TDocument>.Filter.Eq(document => document.Id, id);
        }

        public async Task InsertDocument(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }

        public async Task UpdateDocument(TDocument document)
        {
            FilterDefinition<TDocument> filter = GetDocumentByIdFilter(document.Id);

            await _collection.FindOneAndReplaceAsync(filter, document);
        }
    }
}
