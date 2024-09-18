namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信支付者信息.
/// </summary>
public class WechatPayer
{
    /// <summary>
    /// openid.
    /// </summary>
    [JsonProperty("openid")]
    public string OpenId { get; set; } = string.Empty;
}
