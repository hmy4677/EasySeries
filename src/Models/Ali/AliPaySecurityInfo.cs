namespace EasySeries.Pay.Models.Ali;

/// <summary>
/// 阿里支付安全信息.
/// </summary>
public class AliPaySecurityInfo : PaySecurityInfo
{
    /// <summary>
    /// 阿里公钥文件路径.
    /// </summary>
    public string AliPublicKeyPath { get; set; } = string.Empty;
}
