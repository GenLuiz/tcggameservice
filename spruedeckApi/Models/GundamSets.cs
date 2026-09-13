using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace spruedeckApi.Models;
public class GundamSets
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? setId { get; set; }

    [BsonElement("setName")]
    [JsonPropertyName("setName")]
    public required String SetName { get; set; }

    [BsonElement("description")]
    [JsonPropertyName("description")]
    public String? Description { get; set; }

    [BsonElement("setCode")]
    [JsonPropertyName("setCode")]
    public String? SetCode { get; set; }
    
    [BsonElement("setReleaseDate")]
    [JsonPropertyName("setReleaseDate")]
    public String? SetReleaseDate { get; set; }
}