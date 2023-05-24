namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 预支付响应.
/// </summary>
public class PrepayResponse
{
    /// <summary>
    /// 预支付id.
    /// </summary>
    [JsonProperty("prepay_id")]
    public string PrepayId { get; set; } = string.Empty;
}