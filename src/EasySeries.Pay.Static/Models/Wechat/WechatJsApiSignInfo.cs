namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// JSAPI签名.
/// </summary>
public class WechatJsApiSignInfo
{
    public string AppId { get; set; } = string.Empty;
    public int TimeStamp { get; set; }
    public string NonceStr { get; set; } = string.Empty;
    public string Package { get; set; } = string.Empty;
    public string SignType { get; set; } = "RSA";
    public string PaySign { get; set; } = string.Empty;
}
