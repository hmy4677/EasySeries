namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信支付平台证书(内容可自存文件.pem).
/// </summary>
public class WechatPayPlatformCert
{
    /// <summary>
    /// 证书序列号.
    /// </summary>
    public string SerialNo { get; set; } = string.Empty;

    /// <summary>
    /// 申请时间.
    /// </summary>
    public string EffectiveTime { get; set; } = string.Empty;

    /// <summary>
    /// 有效时间.
    /// </summary>
    public string ExpireTime { get; set; } = string.Empty;

    /// <summary>
    /// 内容.
    /// </summary>
    public string DecryptText { get; set; } = string.Empty;
}
