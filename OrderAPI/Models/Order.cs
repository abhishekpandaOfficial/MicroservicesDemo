using MongoDB.Bson.Serialization.Attributes;

namespace OrderAPI.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Order
    {
        [BsonId, BsonElement("order_id"), BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string OrderId { get; set; }
        [BsonElement("customer_id"), BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int CustomerId { get; set; }

        [BsonElement("ordered_on"), BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime OrderedOn { get; set; }

        [BsonElement("order_details")]
        public List<OrderDetails> orderDetails { get; set; }
    }
}
