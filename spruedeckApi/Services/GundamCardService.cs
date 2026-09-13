using spruedeckApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace spruedeckApi.Services;

public class GundamCardService(IOptions<GundamCardDatabaseSettings> gundamCardDatabaseSettings)
{
    private readonly IMongoCollection<GundamCard> _gundamCardsCollection = new MongoClient(gundamCardDatabaseSettings.Value.ConnectionString)
        .GetDatabase(gundamCardDatabaseSettings.Value.DatabaseName)
        .GetCollection<GundamCard>(gundamCardDatabaseSettings.Value.GundamCardCollectionName);

    private readonly IMongoCollection<GundamSets> _gundamSetsCollection = new MongoClient(gundamCardDatabaseSettings.Value.ConnectionString)
        .GetDatabase(gundamCardDatabaseSettings.Value.DatabaseName)
        .GetCollection<GundamSets>(gundamCardDatabaseSettings.Value.GundamSetsCollectionName);

    public async Task<List<GundamCard>> GetAsync() =>
        await _gundamCardsCollection.Find(_ => true).ToListAsync();

    public async Task<List<GundamCard>> GetReleasedAsync(bool isReleased) =>
        await _gundamCardsCollection.Find(card => card.IsReleased == isReleased).ToListAsync();

    public async Task<GundamCard?> GetCardByIdAsync(string id) =>
        await _gundamCardsCollection.Find(x => x.cardId == id).FirstOrDefaultAsync();

    public async Task<GundamCard?> GetByCardNumberAsync(string cardNumber) =>
        await _gundamCardsCollection.Find(x => x.CardNumber == cardNumber).FirstOrDefaultAsync();

    public async Task<GundamCard?> CheckCardExistsAsync(string cardNumber, CardRarity cardRarity) =>
        await _gundamCardsCollection.Find(x => x.CardNumber == cardNumber && x.CardRarity == cardRarity).FirstOrDefaultAsync();

    public async Task<List<GundamSets>> GetAllSetsAsync() =>
        await _gundamSetsCollection.Find(_ => true).ToListAsync();

    public async Task<GundamSets?> GetSetByNameAsync(string setName) =>
        await _gundamSetsCollection.Find(x => x.SetName == setName).FirstOrDefaultAsync();

    public async Task<GundamCard?> GetBySet(string setName) =>
        await _gundamCardsCollection.Find(x => x.CardSets.Any(set => set.SetName == setName)).FirstOrDefaultAsync();
    public async Task CreateCardAsync(GundamCard newGundamCard) =>
        await _gundamCardsCollection.InsertOneAsync(newGundamCard);

    public async Task CreateSetAsync(GundamSets newGundamSet) =>
        await _gundamSetsCollection.InsertOneAsync(newGundamSet);

    public async Task UpdateAsync(string id, GundamCard updatedGundamCard) =>
        await _gundamCardsCollection.ReplaceOneAsync(x => x.cardId == id, updatedGundamCard);

    public async Task RemoveAsync(string id) =>
        await _gundamCardsCollection.DeleteOneAsync(x => x.cardId == id);
}