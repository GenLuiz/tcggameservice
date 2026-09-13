using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace spruedeckApi.Models;

[Keyless]
public class GundamCardDTO
{
    public required String CardNumber { get; set; }
    public required String CardName { get; set; }
    public required String CardType { get; set; }
    public String? CardImageLink { get; set; }
    public required bool IsReleased { get; set; }
    public required bool IsBanned { get; set; }
    public required bool IsRestricted { get; set; }
    public required CardColor CardColor { get; set; }
    public required CardRarity CardRarity { get; set; }
    public List<String> CardLinks { get; set; } = new List<String>();
    public required List<GundamSets> CardSets { get; set; } = new List<GundamSets>();
    public List<String> CardTraits { get; set; } = new List<String>();
    public required int Cost { get; set; }
    public required int CardLevel { get; set; }
    public int Attack { get; set; }
    public int Health { get; set; }
    
    public GundamCardDTO()
    {
        
    }

    [SetsRequiredMembers]
    public GundamCardDTO(GundamCard card) =>
    (
        CardNumber,
        CardName,
        CardType,
        CardImageLink,
        IsReleased,
        IsBanned,
        IsRestricted,
        CardColor,
        CardRarity,
        CardLinks,
        CardSets,
        CardTraits,
        Cost,
        CardLevel,
        Attack,
        Health
    ) =
    (
        card.CardNumber,
        card.CardName,
        card.CardType,
        card.CardImageLink,
        card.IsReleased,
        card.IsBanned,
        card.IsRestricted,
        card.CardColor,
        card.CardRarity,
        card.CardLinks,
        card.CardSets,
        card.CardTraits,
        card.Cost,
        card.CardLevel,
        card.Attack,
        card.Health
    );
}