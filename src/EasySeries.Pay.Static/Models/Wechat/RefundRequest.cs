namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信退款请求信息.
/// </summary>
internal class RefundRequest
{
    /// <summary>
    /// 商户单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    internal string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 退款单号.
    /// </summary>
    [JsonProperty("out_refund_no")]
    internal string OutRefundNo { get; set; } = string.Empty;

    /// <summary>
    /// 退款原因.
    /// </summary>
    [JsonProperty("reason")]
    internal string Reason { get; set; } = string.Empty;

    /// <summary>
    /// 金额信息.
    /// </summary>
    [JsonProperty("amount")]
    internal WechatRefundAmount Amount { get; set; } = new();

    /// <summary>
    /// 退款结果回调url.
    /// </summary>
    [JsonProperty("notify_url")]
    internal string NotifyUrl { get; set; } = string.Empty;
}
