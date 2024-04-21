using EasySeries.Pay.Enums;

namespace EasySeries.Pay.Models.Wechat;

/// <summary>
/// 微信支付model.
/// </summary>
public class JSAPIPayModel : AppPayModel
{
    /// <summary>
    /// 支付人openid.
    /// </summary>
    [Required]
    public string OpenId { get; set; } = string.Empty;

    /// <summary>
    /// JSAPI类型(0:小程序,1:公众号).
    /// </summary>
    public JSAPIAppIdTypes AppIdType { get; set; } = JSAPIAppIdTypes.MiniAppId;
}