namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 订单金额.
/// </summary>
public class OrderAmount
{
    /// <summary>
    /// 货币类型.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = "CNY";

    /// <summary>
    /// 总金额(单位:分).
    /// </summary>
    [JsonProperty("total")]
    public int Total { get; set; }
}