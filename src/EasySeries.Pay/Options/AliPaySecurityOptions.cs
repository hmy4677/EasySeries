namespace EasySeries.Pay.Options;

/// <summary>
/// 阿里支付安全信息.
/// </summary>
public class AliPaySecurityOptions : PaySecurityOptions
{
    /// <summary>
    /// 阿里支付配置名称.
    /// </summary>
    public static readonly string SettingKey = "AliPaySecurityOptions";

    /// <summary>
    /// 是否验签.
    /// </summary>
    [Required]
    public bool IsVerifySign { get; set; } = true;

    /// <summary>
    /// 安全类型(KEY:密钥/CERT:证书).
    /// </summary>
    [Required]
    public string SecurityType { get; set; } = "KEY";

    /// <summary>
    /// 阿里公钥文件路径.
    /// </summary>
    public string AliPublicKeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 应用公钥证书文件路径.
    /// </summary>
    public string AliAppPublicCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 阿里根证书文件路径.
    /// </summary>
    public string AliRootCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 阿里公钥证书文件路径.
    /// </summary>
    public string AliPublicCertPath { get; set; } = string.Empty;
}