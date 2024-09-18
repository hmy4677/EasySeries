namespace EasySeries.Pay.Static.Models.Ali;

/// <summary>
/// 支付宝配置.
/// </summary>
public class AliPayConfig
{
    /// <summary>
    /// APP ID.
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 是否验签.
    /// </summary>
    public bool IsVerifySign { get; set; } = true;

    /// <summary>
    /// 商户私钥文件路径.
    /// </summary>
    public string PrivateKeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 支付宝公钥文件路径.
    /// </summary>
    public string AliPublicKeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 支付宝公钥证书文件路径.
    /// </summary>
    public string AliPublicCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 支付宝应用公钥证书文件路径.
    /// </summary>
    public string AliAppPublicCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 支付宝根证书文件路径.
    /// </summary>
    public string AliRootCertPath { get; set; } = string.Empty;

    /// <summary>
    /// 支付通知回调url.
    /// </summary>
    public string PayNotifyUrl { get; set; } = string.Empty;

    /// <summary>
    /// 安全类型(KEY or CERT).
    /// </summary>
    public string SecurityType { get; set; } = "KEY";
}