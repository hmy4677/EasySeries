namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信查询支付响应.
/// </summary>
public class WechatQueryPayResponse
{
    /// <summary>
    /// app id.
    /// </summary>
    [JsonProperty("appid")]
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 商户id.
    /// </summary>
    [JsonProperty("mchid")]
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 商户订单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 微信交易单号.
    /// </summary>
    [JsonProperty("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// 交易类型.
    /// </summary>
    [JsonProperty("trade_type")]
    public string TradeType { get; set; } = string.Empty;

    /// <summary>
    /// 交易状态.
    /// </summary>
    [JsonProperty("trade_state")]
    public string TradeState { get; set; } = string.Empty;

    /// <summary>
    /// 交易状态描述.
    /// </summary>
    [JsonProperty("trade_state_desc")]
    public string TradeStateDesc { get; set; } = string.Empty;

    /// <summary>
    /// 银行类型.
    /// </summary>
    [JsonProperty("bank_type")]
    public string BankType { get; set; } = string.Empty;

    /// <summary>
    /// 成功时间.
    /// </summary>
    [JsonProperty("success_time")]
    public string SuccessTime { get; set; } = string.Empty;

    /// <summary>
    /// 支付者信息.
    /// </summary>
    [JsonProperty("payer")]
    public WechatPayer Payer { get; set; } = new();

    /// <summary>
    /// 金额信息.
    /// </summary>
    [JsonProperty("amount")]
    public WechatOrderAmount Amount { get; set; } = new();
}