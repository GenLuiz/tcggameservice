using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using spruedeckApi.Models;
using spruedeckApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<GundamCardDatabaseSettings>(builder.Configuration.GetSection("GundamCardDatabaseSettings"));
builder.Services.AddSingleton<GundamCardService>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

var gundamCards = app.MapGroup("/gundamcards");

gundamCards.MapGet("/", GetAllGundamCards);

static async Task<IResult> GetAllGundamCards(GundamCardService gundamCardService)
{
    return TypedResults.Ok(await gundamCardService.GetAsync());
}

gundamCards.MapGet("/released", GetReleasedGundamCards);

static async Task<IResult> GetReleasedGundamCards(GundamCardService gundamCardService)
{
    return TypedResults.Ok(await gundamCardService.GetReleasedAsync(true));
}

gundamCards.MapGet("/unreleased", GetUnreleasedGundamCards);

static async Task<IResult> GetUnreleasedGundamCards(GundamCardService gundamCardService)
{
    return TypedResults.Ok(await gundamCardService.GetReleasedAsync(false));
}

gundamCards.MapGet("/set/{setName}", GetGundamCardsBySetName);

static async Task<IResult> GetGundamCardsBySetName(GundamCardService gundamCardService, string setName)
{
    var cards = await gundamCardService.GetBySet(setName);
    return TypedResults.Ok(cards);
}

gundamCards.MapGet("/sets", GetAllGundamSetsList);

static async Task<IResult> GetAllGundamSetsList(GundamCardService gundamCardService)
{
    return TypedResults.Ok(await gundamCardService.GetAllSetsAsync());
}

gundamCards.MapPost("/sets", CreateGundamSet);

static async Task<IResult> CreateGundamSet(GundamCardService gundamCardService, GundamSets set)
{
    var setExists = await gundamCardService.GetSetByNameAsync(set.SetName) is not null;
    if (setExists)
    {
        return TypedResults.Conflict($"Set with SetName {set.SetName} already exists.");
    }
    else
    {
        await gundamCardService.CreateSetAsync(set);
    }

    return TypedResults.Created($"/sets/{set.SetName}", set);
}

gundamCards.MapGet("/cardnumber/{cardNumber}", GetGundamCardByCardNumber);

static async Task<IResult> GetGundamCardByCardNumber(GundamCardService gundamCardService, string cardNumber)
{
    var card = await gundamCardService.GetByCardNumberAsync(cardNumber);
    if (card is null)
    {
        return TypedResults.NotFound();
    }
    return TypedResults.Ok(card);
}

gundamCards.MapPost("/", CreateGundamCard);

static async Task<IResult> CreateGundamCard(GundamCardService gundamCardService, GundamCard card)
{
    var cardExists = await gundamCardService.CheckCardExistsAsync(card.CardNumber, card.CardRarity) is not null;
    if (cardExists)
    {
        return TypedResults.Conflict($"Card with CardNumber {card.CardNumber} already exists. and is of Rarity {card.CardRarity}");
    }else
    {
        await gundamCardService.CreateCardAsync(card);
    }
   
    return TypedResults.Created($"/cardId/{card.cardId}", card);
}

gundamCards.MapPut("/cardId/{cardId}", UpdateGundamCard);

static async Task<IResult> UpdateGundamCard(GundamCardService gundamCardService, string cardId, GundamCard updatedCard)
{
    var card = await gundamCardService.GetCardByIdAsync(cardId);
    if (card is null)
    {
        return TypedResults.NotFound();
    }
    else
    {
        card.CardName = updatedCard.CardName;
        card.CardType = updatedCard.CardType;
        card.IsReleased = updatedCard.IsReleased;
        card.IsBanned = updatedCard.IsBanned;
        card.IsRestricted = updatedCard.IsRestricted;
        card.CardSubType = updatedCard.CardSubType;
        card.CardSubtitle = updatedCard.CardSubtitle;
        card.EffectDescription = updatedCard.EffectDescription;
        card.CardColor = updatedCard.CardColor;
        card.CardRarity = updatedCard.CardRarity;
        card.CardAlternateArt = updatedCard.CardAlternateArt;
        card.CardLinks = updatedCard.CardLinks;
        card.CardSets = updatedCard.CardSets;
        card.CardTraits = updatedCard.CardTraits;
        card.CardZones = updatedCard.CardZones;
        card.Cost = updatedCard.Cost;
        card.CardLevel = updatedCard.CardLevel;
        card.Attack = updatedCard.Attack;
        card.Health = updatedCard.Health;

        await gundamCardService.UpdateAsync(card.cardId, card);
    }

    return TypedResults.NoContent();
}

gundamCards.MapDelete("/cardId/{cardId}", DeleteGundamCard);

static async Task<IResult> DeleteGundamCard(GundamCardService gundamCardService, string cardId)
{
    var card = await gundamCardService.GetCardByIdAsync(cardId);
    if (card is null)
    {
        return TypedResults.NotFound();
    }
    else
    {
        await gundamCardService.RemoveAsync(card.cardId);
        return TypedResults.NoContent();
    }
}


app.Run();