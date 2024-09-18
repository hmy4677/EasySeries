namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信退款金额.
/// </summary>
public class WechatRefundAmount
{
    /// <summary>
    /// 退款金额
    /// </summary>
    [JsonProperty("refund")]
    public int Refund { get; set; }

    /// <summary>
    /// 订单总金额
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// 币种
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = "CNY";
}