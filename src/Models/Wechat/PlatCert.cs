namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 微信平台证书(得到内容后可自存文件使用).
/// </summary>
public class PlatCert
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