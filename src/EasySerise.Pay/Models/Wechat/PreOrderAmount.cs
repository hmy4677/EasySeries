namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 预付订单金额.
/// </summary>
public class PreOrderAmount
{
    /// <summary>
    /// 货币类型.
    /// </summary>
    [JsonProperty("currency")]
    public string Currency { get; set; } = "CNY";

    /// <summary>
    /// 总金额(单位:分).
    /// </summary>
    [Required]
    [JsonProperty("total")]
    public int Total { get; set; }
}