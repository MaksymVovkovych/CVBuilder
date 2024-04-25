#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Responses;
using CVBuilder.Application.Proposal.Services.Interfaces;

namespace CVBuilder.Application.Proposal.Services;

public class Privat24CurrencyService : ICurrencyService
{
    private readonly HttpClient _client;

    public Privat24CurrencyService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("https://api.privatbank.ua/p24api/");
    }


    public async Task<List<CurrencyResult>?> GetAllCurrencyAsync()
    {
        return await _client.GetFromJsonAsync<List<CurrencyResult>>("pubinfo?exchange&coursid=11");
    }

    public async Task<CurrencyResult?> GetCurrencyAsync(string currencyCode)
    {
        return (await GetAllCurrencyAsync())?.FirstOrDefault(x => x.CurrencyCode == currencyCode);
    }
}