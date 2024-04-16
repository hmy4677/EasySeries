namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 微信小程序签名(用于拉起支付).
/// </summary>
public class MiniAppSignInfo
{
    public string AppId { get; set; }
    public int TimeStamp { get; set; }
    public string NonceStr { get; set; } = string.Empty;
    public string Package { get; set; } = string.Empty;
    public string SignType { get; set; } = "RSA";
    public string PaySign { get; set; } = string.Empty;
}