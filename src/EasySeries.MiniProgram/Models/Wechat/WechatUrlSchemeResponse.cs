namespace EasySeries.MiniProgram.Models.Wechat;

/// <summary>
/// WechatUrlSchemeResponse.
/// </summary>
public class WechatUrlSchemeResponse : WechatResponse
{
    /// <summary>
    /// openlink
    /// </summary>
    [JsonProperty("openlink")]
    public string OpenLink { get; set; } = string.Empty;
}
