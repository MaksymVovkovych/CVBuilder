using System.Text.Json.Serialization;

namespace CVBuilder.Application.Proposal.Responses;

public class CurrencyResult
{
    [JsonPropertyName("ccy")]
    public string CurrencyCode { get; set; }
    [JsonPropertyName("base_ccy")]
    public string BaseCurrencyCode { get; set; }
    [JsonPropertyName("buy")]
    public string Buy { get; set; }
    [JsonPropertyName("sale")]
    public string Sale { get; set; }
}