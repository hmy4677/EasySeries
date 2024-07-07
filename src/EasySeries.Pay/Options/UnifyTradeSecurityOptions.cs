namespace EasySeries.Pay.Options;

/// <summary>
/// 中信全付通安全配置.
/// </summary>
public class UnifyTradeSecurityOptions
{
    /// <summary>
    /// 配置名称.
    /// </summary>
    public const string SettingKey = "UnifyTradeSecurityOptions";

    /// <summary>
    /// 是否验签.
    /// </summary>
    [Required]
    public bool IsVerifySign { get; set; } = true;

    /// <summary>
    /// 证书文件路径.
    /// </summary>
    [Required]
    public string CerFilePath { get; set; } = string.Empty;

    /// <summary>
    /// 密钥文件路径(从平台下载后需要解密).
    /// </summary>
    [Required]
    public string KeyFilePath { get; set; } = string.Empty;

    /// <summary>
    /// 中信商户id.
    /// </summary>
    [Required]
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 微信小程序id.
    /// </summary>
    [Required]
    public string SubAppId { get; set; } = string.Empty;

    /// <summary>
    /// 回调通知url.
    /// </summary>
    [Required]
    public string NotifyUrl { get; set; } = string.Empty;
}