namespace EasySeries.Pay.Models.UnifyTrade;

/// <summary>
/// 中信全付通查询请求.
/// </summary>
public class UnifyTradeQueryRequest : UnifyTradeSign
{
    /// <summary>
    /// 接口类型.
    /// </summary>
    [JsonProperty("service")]
    public string Service { get; } = "unified.trade.query";

    /// <summary>
    /// 商户号.
    /// </summary>
    [JsonProperty("mch_id")]
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 商户订单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;
}