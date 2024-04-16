namespace EasySeries.Pay.Options;

/// <summary>
/// 微信支付安全信息.
/// </summary>
public class WechatPaySecurityOptions : PaySecurityOptions
{
    /// <summary>
    /// 微信支付配置名称.
    /// </summary>
    public static readonly string SettingKey = "WechatPaySecurityOptions";

    /// <summary>
    /// 微信商户号id.
    /// </summary>
    [Required]
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付证书序列号.
    /// </summary>
    [Required]
    public string CertSerialno { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付V3密码.
    /// </summary>
    [Required]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 是否验签.
    /// </summary>
    [Required]
    public bool IsVerifySign { get; set; } = true;

    /// <summary>
    /// 微信平台证书文件路径.
    /// </summary>
    [Required]
    public string PlatCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 退款通知回调url.
    /// </summary>
    public string RefundNotifyUrl { get; set; } = string.Empty;
}