using System.Xml.Serialization;

namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 中信银行订单响应.
/// </summary>
public class CiticBankOrderResponse : ResponseBase
{
    /// <summary>
    /// 交易类型.
    /// </summary>
    [XmlElement("trade_type")]
    public string TradeType { get; set; } = string.Empty;

    /// <summary>
    /// 微信APP调用数据.
    /// </summary>
    [XmlElement("wc_pay_data")]
    public string WechatPayData { get; set; } = string.Empty;

    /// <summary>
    /// 支付宝交易号.
    /// </summary>
    [XmlElement("trade_no")]
    public string AlipayTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 二维码链接.
    /// </summary>
    [XmlElement("code_url")]
    public string QrCodeUrl { get; set; } = string.Empty;
}
