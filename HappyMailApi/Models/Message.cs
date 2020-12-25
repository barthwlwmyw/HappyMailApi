using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace HappyMailApi.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SenderUsername")]
        [Required]
        public string SenderUsername { get; set; }

        [BsonElement("RecipientUsername")]
        [Required]
        public string RecipientUsername { get; set; }

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Content")]
        [Required]
        public string Content { get; set; }

        [BsonElement("IsToxic")]
        [Required]
        public bool IsToxic { get; set; }
    }
}
