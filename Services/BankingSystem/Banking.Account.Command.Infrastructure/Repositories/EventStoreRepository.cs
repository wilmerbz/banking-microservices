using Banking.Account.Command.Application.Contracts.Persistence;
using Banking.Account.Command.Application.Models;
using Banking.Account.Command.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Command.Infrastructure.Repositories
{
    public class EventStoreRepository : MongoRepository<EventModel>, IEventStoreRepository
    {
        public EventStoreRepository(IOptions<MongoSettings> options) : base(options)
        {
        }

        public async Task<IEnumerable<EventModel>> FindByAggregateId(string id)
        {
            var filter = Builders<EventModel>.Filter.Eq(document => document.AggregateIdentifier, id);
            var eventModels = await _collection.Find(filter).ToListAsync();
            return eventModels;
        }
    }
}
