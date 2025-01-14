namespace EasySeries.MiniProgram.Models.Wechat;

/// <summary>
/// 微信session响应.
/// </summary>
public class WechatSessionResponse : WechatResponse
{
    /// <summary>
    /// open id.
    /// </summary>
    [JsonProperty("openid")]
    public string OpenId { get; set; }

    /// <summary>
    /// session key.
    /// </summary>
    [JsonProperty("session_key")]
    public string SessionKey { get; set; }

    /// <summary>
    /// unionid.
    /// </summary>
    [JsonProperty("unionid")]
    public string? UnionId { get; set; }
}