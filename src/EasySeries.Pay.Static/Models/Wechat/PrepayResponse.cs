namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 预付响应.
/// </summary>
internal class PrepayResponse
{
    /// <summary>
    /// 预付id.
    /// </summary>
    [JsonProperty("prepay_id")]
    internal string PrepayId { get; set; } = string.Empty;
}
