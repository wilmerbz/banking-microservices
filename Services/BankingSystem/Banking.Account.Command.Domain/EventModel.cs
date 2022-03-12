using Banking.Account.Command.Domain.Common;
using Banking.CQRS.Core.Events;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Account.Command.Domain
{
    [BsonCollection("EventStore")]
    public class EventModel : Document
    {

        [BsonElement("Timespan")]
        public DateTime Timespan { get; set; }


        [BsonElement("AggregateIdentifier")]
        public string AggregateIdentifier { get; set; } = string.Empty;


        [BsonElement("AggregateType")]
        public string AggregateType { get; set; } = string.Empty;


        [BsonElement("Version")]
        public int Version { get; set; }


        [BsonElement("EventType")]
        public string EventType { get; set; } = string.Empty;


        [BsonElement("EventData")]
        public EventBase EventData { get; set; }

    }
}
