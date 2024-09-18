namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信订单金额.
/// </summary>
public class WechatOrderAmount : OrderAmount
{
    /// <summary>
    /// 支付者支付的金额.
    /// </summary>
    [JsonProperty("payer_total")]
    public int PayerTotal { get; set; }

    /// <summary>
    /// 用户支付货币.
    /// </summary>
    [JsonProperty("payer_currency")]
    public string PayerCurrency { get; set; } = "CNY";
}
