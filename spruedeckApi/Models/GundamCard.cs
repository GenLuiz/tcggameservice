using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace spruedeckApi.Models;

[Keyless]
public class GundamCard
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? cardId { get; set; }

    [BsonElement("cardNumber")]
    [JsonPropertyName("cardNumber")]
    public required String CardNumber { get; set; }

    [BsonElement("cardName")]
    [JsonPropertyName("cardName")]
    public required String CardName { get; set; }

    [BsonElement("cardType")]
    [JsonPropertyName("cardType")]
    public required String CardType { get; set; }

    [BsonElement("cardImageLink")]
    [JsonPropertyName("cardImageLink")]
    public String? CardImageLink { get; set; }

    [BsonElement("isReleased")]
    [JsonPropertyName("isReleased")]
    public required bool IsReleased { get; set; }

    [BsonElement("isBanned")]
    [JsonPropertyName("isBanned")]
    public required bool IsBanned { get; set; }

    [BsonElement("isRestricted")]
    [JsonPropertyName("isRestricted")]
    public required bool IsRestricted { get; set; }

    [BsonElement("cardSubType")]
    [JsonPropertyName("cardSubType")]
    public String? CardSubType { get; set; }

    [BsonElement("cardSubtitle")]
    [JsonPropertyName("cardSubtitle")]
    public String? CardSubtitle { get; set; }

    [BsonElement("effectDescription")]
    [JsonPropertyName("effectDescription")]
    public String? EffectDescription { get; set; }

    [BsonElement("cardColor")]
    [JsonPropertyName("cardColor")]
    public required CardColor CardColor { get; set; }

    [BsonElement("cardRarity")]
    [JsonPropertyName("cardRarity")]
    public required CardRarity CardRarity { get; set; }
    
    [BsonElement("cardAlternateArt")]
    [JsonPropertyName("cardAlternateArt")]
    public List<String> CardAlternateArt { get; set; } = new List<String>();

    [BsonElement("cardLinks")]
    [JsonPropertyName("cardLinks")]
    public List<String> CardLinks { get; set; } = new List<String>();

    [BsonElement("cardSets")]
    [JsonPropertyName("cardSets")]
    public List<GundamSets> CardSets { get; set; } = new List<GundamSets>();

    [BsonElement("cardTraits")]
    [JsonPropertyName("cardTraits")]
    public List<String> CardTraits { get; set; } = new List<String>();

    [BsonElement("cardZones")]
    [JsonPropertyName("cardZones")]
    public List<String> CardZones { get; set; } = new List<String>();

    [BsonElement("cost")]
    [JsonPropertyName("cost")]
    public required int Cost { get; set; }

    [BsonElement("cardLevel")]
    [JsonPropertyName("cardLevel")]
    public required int CardLevel { get; set; }

    [BsonElement("attack")]
    [JsonPropertyName("attack")]
    public int Attack { get; set; }

    [BsonElement("health")]
    [JsonPropertyName("health")]
    public int Health { get; set; }
    
}