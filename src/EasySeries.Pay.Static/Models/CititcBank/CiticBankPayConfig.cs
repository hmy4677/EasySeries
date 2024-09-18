namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 中信银行支付配置.
/// </summary>
public class CiticBankPayConfig
{
    /// <summary>
    /// 中信商户id.
    /// </summary>
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 绑定的App Id.
    /// </summary>
    public string SubAppId { get; set; } = string.Empty;

    /// <summary>
    /// 证书文件路径.
    /// </summary>
    public string CertFilePath { get; set; } = string.Empty;

    /// <summary>
    /// 密钥文件路径(从平台下载后需要解密).
    /// </summary>
    public string KeyFilePath { get; set; } = string.Empty;

    /// <summary>
    /// 回调通知url.
    /// </summary>
    public string NotifyUrl { get; set; } = string.Empty;

    /// <summary>
    /// 是否验签(Linux中验签失败原因未知).
    /// </summary>
    public bool IsVerifySign { get; set; } = true;
}