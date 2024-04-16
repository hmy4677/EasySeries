namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 微信退款通知.
/// </summary>
public class RefundNotify
{
    /// <summary>
    /// 商户id.
    /// </summary>
    [JsonProperty("mchid")]
    public string Mchid { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付订单号.
    /// </summary>
    [JsonProperty("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// 商户订单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    public string OutTradeNO { get; set; } = string.Empty;

    /// <summary>
    /// 微信退款单号.
    /// </summary>
    [JsonProperty("refund_id")]
    public string RefundId { get; set; } = string.Empty;

    /// <summary>
    /// 商户退款单号.
    /// </summary>
    [JsonProperty("out_refund_no")]
    public string OutRefundNO { get; set; } = string.Empty;

    /// <summary>
    /// 退款状态(SUCCESS：退款成功 CLOSED：退款关闭 ABNORMAL：退款异常).
    /// </summary>
    [JsonProperty("refund_status")]
    public string RefundStatus { get; set; } = string.Empty;

    /// <summary>
    /// 退款成功时间.
    /// </summary>
    [JsonProperty("success_time")]
    public string SuccessTime { get; set; } = string.Empty;

    /// <summary>
    /// 退款入账账户.
    /// </summary>
    [JsonProperty("user_received_account")]
    public string UserReceivedAccount { get; set; } = string.Empty;

    /// <summary>
    /// 金额信息.
    /// </summary>
    [JsonProperty("amount")]
    public RefundNotifyAmount Amount { get; set; } = new RefundNotifyAmount();
}

/// <summary>
/// 微信退款通知金额.
/// </summary>
public class RefundNotifyAmount
{
    /// <summary>
    /// 订单总金额.
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }

    /// <summary>
    /// 退款金额.
    /// </summary>
    [JsonProperty("refund")]
    public int Refund { get; set; }

    /// <summary>
    /// 用户支付金额.
    /// </summary>
    [JsonProperty("payer_total")]
    public int PayerTotal { get; set; }

    /// <summary>
    /// 用户退款金额.
    /// </summary>
    [JsonProperty("payer_refund")]
    public int PayerRefund { get; set; }
}