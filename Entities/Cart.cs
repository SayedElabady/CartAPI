using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<string> ProductIds { get; set; } = new List<string>();

        public double TotalPrice { get; set; } = 0.0;

        public CartStatus Status { get; set; } = CartStatus.Ready;
    }
}