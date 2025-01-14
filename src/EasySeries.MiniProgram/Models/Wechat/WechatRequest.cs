namespace EasySeries.MiniProgram.Models.Wechat;

public class WechatRequest
{
    /// <summary>
    /// AccessToken.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 正式版为 "release"，体验版为 "trial"，开发版为 "develop".
    /// </summary>
    public string EnvVersion { get; set; } = "release";
}
