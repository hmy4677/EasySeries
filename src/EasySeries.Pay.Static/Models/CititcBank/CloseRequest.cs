namespace EasySeries.Pay.Static.Models.CititcBank;

internal class CloseRequest : SignBase
{
    /// <summary>
    /// 接口类型.
    /// </summary>
    [JsonProperty("service")]
    public string Service { get; } = "unified.trade.close";

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
}
