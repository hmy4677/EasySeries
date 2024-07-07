namespace EasySeries.Pay.Models.UnifyTrade;

/// <summary>
/// 中信全付通退款响应.
/// </summary>
[XmlRoot(ElementName = "stream")]
public class UnifyTradeRefundResponse : UnifyTradeResponseBase
{
    /// <summary>
    /// 商户订单号.
    /// </summary>
    [XmlElement("out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 第三方订单号.
    /// </summary>
    [XmlElement("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// 商户退款单号.
    /// </summary>
    [XmlElement("out_refund_no")]
    public string OutRefundNo { get; set; } = string.Empty;

    /// <summary>
    /// 交易类型.
    /// </summary>
    [XmlElement("trade_type")]
    public string TradeType { get; set; } = string.Empty;

    /// <summary>
    /// 退款渠道.
    /// </summary>
    [XmlElement("refund_channel")]
    public string RefundChannel { get; set; } = string.Empty;

    /// <summary>
    /// 退款金额.
    /// </summary>
    [XmlElement("refund_fee")]
    public int? RefundFee { get; set; } = null;

    /// <summary>
    /// 退款支付时间.
    /// </summary>
    [XmlElement("gmt_refund_pay")]
    public string GmtRefundPay { get; set; } = string.Empty;

    /// <summary>
    /// 现金券退款金额.
    /// </summary>
    [XmlElement("coupon_refund_fee")]
    public int? CouponRefundFee { get; set; } = null;

    /// <summary>
    /// 第三方退款单号【微信】.
    /// </summary>
    [XmlElement("refund_id")]
    public string RefundId { get; set; } = string.Empty;
}