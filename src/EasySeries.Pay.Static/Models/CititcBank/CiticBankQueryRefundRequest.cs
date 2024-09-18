namespace EasySeries.Pay.Static.Models.CititcBank;

internal class CiticBankQueryRefundRequest : SignBase
{
    /// <summary>
    /// 服务.
    /// </summary>
    [JsonProperty("service")]
    public string Service { get; } = "unified.trade.refundquery";

    /// <summary>
    /// 商户号.
    /// </summary>
    [JsonProperty("mch_id")]
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 商户单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 退款单号.
    /// </summary>
    [JsonProperty("out_refund_no")]
    public string OutRefundNo { get; set; } = string.Empty;
}
