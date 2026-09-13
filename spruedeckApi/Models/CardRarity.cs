using System.Text.Json.Serialization;
namespace spruedeckApi.Models;
public enum CardRarity
{
    [JsonStringEnumMemberName("C")]
    C=1,
    [JsonStringEnumMemberName("U")]
    U=2,
    [JsonStringEnumMemberName("R")]
    R=3,
    [JsonStringEnumMemberName("LR")]
    LR=4,
    [JsonStringEnumMemberName("C+")]
    CPlus=5,
    [JsonStringEnumMemberName("U+")]
    UPlus=6,
    [JsonStringEnumMemberName("R+")]
    RPlus=7,
    [JsonStringEnumMemberName("LR+")]
    LRPlus=8,
    [JsonStringEnumMemberName("C++")]
    CPlusPlus=9,
    [JsonStringEnumMemberName("LR++")]
    LRPlusPlus=10,
    [JsonStringEnumMemberName("P")]
    P=11
}