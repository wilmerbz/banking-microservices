using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Banking.Account.Command.Domain.Common
{
    /// <summary>
    /// Represents the interface for Event Sourcing commands.
    /// </summary>
    public interface IDocument
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }

        DateTime CreatedDate { get; set; }

    }
}
