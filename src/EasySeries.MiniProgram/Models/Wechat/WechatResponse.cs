namespace EasySeries.MiniProgram.Models.Wechat;

/// <summary>
/// 微信响应.
/// </summary>
public class WechatResponse
{
    /// <summary>
    /// 错误码.
    /// </summary>
    [JsonProperty("errcode")]
    public int ErrCode { get; set; }

    /// <summary>
    /// 错误信息.
    /// </summary>
    [JsonProperty("errmsg")]
    public string ErrMsg { get; set; } = string.Empty;
}
