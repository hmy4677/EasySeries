namespace EasySeries.Post.Options;

/// <summary>
/// 京东物流Options.
/// </summary>
public class JDLSecurityOptions
{
    public static readonly string SettingKey = "JDLSecurityOptions";

    public string Appkey { get; set; } = string.Empty;

    public string AppSecret { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public string CustomerCode { get; set; } = string.Empty;
}
