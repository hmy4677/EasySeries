namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 支付查询响应.
/// </summary>
public class PayQueryResponse
{
    [JsonProperty("appid")]
    public string AppId { get; set; } = string.Empty;

    [JsonProperty("mchid")]
    public string MchId { get; set; } = string.Empty;

    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;

    [JsonProperty("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    [JsonProperty("trade_type")]
    public string TradeType { get; set; } = string.Empty;

    [JsonProperty("trade_state")]
    public string TradeState { get; set; } = string.Empty;

    [JsonProperty("trade_state_desc")]
    public string TradeStateDesc { get; set; } = string.Empty;

    [JsonProperty("bank_type")]
    public string BankType { get; set; } = string.Empty;

    [JsonProperty("success_time")]
    public string SuccessTime { get; set; } = string.Empty;

    [JsonProperty("payer")]
    public PayerInfo Payer { get; set; } = new PayerInfo();

    [JsonProperty("amount")]
    public OrderAmountInfo Amount { get; set; } = new OrderAmountInfo();
}