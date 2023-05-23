namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 支付者信息.
/// </summary>
public class PayerInfo
{
    /// <summary>
    /// openid.
    /// </summary>
    [JsonProperty("openid")]
    public string Openid { get; set; } = string.Empty;
}