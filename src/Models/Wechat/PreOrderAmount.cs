namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 预付订单金额.
/// </summary>
internal class PreOrderAmount
{
    /// <summary>
    /// 货币类型.
    /// </summary>
    [JsonProperty("currency")]
    internal string Currency { get; set; } = "CNY";

    /// <summary>
    /// 总金额(单位:分).
    /// </summary>
    [Required]
    [JsonProperty("total")]
    internal int Total { get; set; }
}