namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 订单金额信息.
/// </summary>
public class OrderAmountInfo : PreOrderAmount
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