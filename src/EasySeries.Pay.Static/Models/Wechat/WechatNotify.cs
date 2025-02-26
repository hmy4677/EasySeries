namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信回调通知.
/// </summary>
public class WechatNotify
{
    /// <summary>
    /// 签名.
    /// [Http]Request.Headers["Wechatpay-Signature"];
    /// </summary>
    public string Signature { get; set; } = string.Empty;

    /// <summary>
    /// 时间戳.
    /// [Http]Request.Headers["Wechatpay-Timestamp"];
    /// </summary>
    public string Stamp { get; set; } = string.Empty;

    /// <summary>
    /// 随机串.
    /// [Http]Request.Headers["Wechatpay-Nonce"];
    /// </summary>
    public string Nonce { get; set; } = string.Empty;

    /// <summary>
    /// 请求体.
    /// var reader = new StreamReader(request.Body);
    /// var body = await reader.ReadToEndAsync();
    /// </summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// 公钥证书ID.
    /// [Http]Request.Headers["Wechatpay-Serial"];
    /// </summary>
    public string WechatpaySerial { get; set; }
}