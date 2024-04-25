using System.Collections.Generic;
using System.Threading.Tasks;
using CVBuilder.Application.Proposal.Responses;

namespace CVBuilder.Application.Proposal.Services.Interfaces;

public interface ICurrencyService
{
    Task<List<CurrencyResult>> GetAllCurrencyAsync();
    Task<CurrencyResult> GetCurrencyAsync(string currencyCode);
}