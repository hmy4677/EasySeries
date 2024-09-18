using System.Reflection.PortableExecutable;

namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信回调通知.
/// </summary>
public class WechatNotify
{
    /// <summary>
    /// 签名.
    /// HttpRequest.Headers["Wechatpay-Signature"];
    /// </summary>
    public string Signature { get; set; } = string.Empty;

    /// <summary>
    /// 时间戳.
    /// HttpRequest.Headers["Wechatpay-Timestamp"];
    /// </summary>
    public string Stamp { get; set; } = string.Empty;

    /// <summary>
    /// 随机串.
    /// HttpRequest.Headers["Wechatpay-Nonce"];
    /// </summary>
    public string Nonce { get; set; } = string.Empty;

    /// <summary>
    /// 请求体.
    /// var reader = new StreamReader(request.Body);
    /// var body = await reader.ReadToEndAsync();
    /// </summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// 是否验签.
    /// </summary>
    public bool IsVerifySign { get; set; } = true;
}