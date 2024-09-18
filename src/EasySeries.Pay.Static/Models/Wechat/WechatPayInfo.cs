namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信支付信息.
/// </summary>
public class WechatPayInfo
{
    /// <summary>
    /// 金额(单位:分).
    /// </summary>
    [Required]
    public int Amount { get; set; }

    /// <summary>
    /// 订单号(商户单号).
    /// </summary>
    [Required]
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// OpenId,JsApi要用.
    /// </summary>
    public string? OpenId { get; set; }

    /// <summary>
    /// 商品描述.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}