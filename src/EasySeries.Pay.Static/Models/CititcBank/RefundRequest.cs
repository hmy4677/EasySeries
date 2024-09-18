namespace EasySeries.Pay.Static.Models.CititcBank;

internal class RefundRequest : SignBase
{
    /// <summary>
    /// 接口类型.
    /// </summary>
    [JsonProperty("service")]
    public string Service { get; } = "unified.trade.refund";

    /// <summary>
    /// 商户号.
    /// </summary>
    [JsonProperty("mch_id")]
    public string MchId { get; set; }

    /// <summary>
    /// 商户订单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; }

    /// <summary>
    /// 商户退款单号.
    /// </summary>
    [JsonProperty("out_refund_no")]
    public string OutRefundNo { get; set; }

    /// <summary>
    /// 总金额.
    /// </summary>
    [JsonProperty("total_fee")]
    public int TotalFee { get; set; }

    /// <summary>
    /// 退款金额.
    /// </summary>
    [JsonProperty("refund_fee")]
    public int RefundFee { get; set; }
}
