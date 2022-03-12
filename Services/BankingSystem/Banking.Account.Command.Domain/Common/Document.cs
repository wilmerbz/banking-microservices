using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Banking.Account.Command.Domain.Common
{
    /// <summary>
    /// Base class for Documents to insert on the event sourcing DB.
    /// </summary>
    public class Document : IDocument
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
