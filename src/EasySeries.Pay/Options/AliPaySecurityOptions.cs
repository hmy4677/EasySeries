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
    /// 阿里公钥文件路径.
    /// </summary>
    public string AliPublicKeyPath { get; set; } = string.Empty;
}
