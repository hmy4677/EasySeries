namespace EasySeries.Pay.Models.UnifyTrade;

/// <summary>
/// 中信全付通回调响应.
/// </summary>
[XmlRoot(ElementName = "stream")]
public class UnifyTradeNotifyResponse : UnifyTradeResponseBase
{
    /// <summary>
    /// 交易类型.
    /// </summary>
    [XmlElement("trade_type")]
    public string TradeType { get; set; }

    /// <summary>
    /// 第三方订单号.
    /// </summary>
    [XmlElement("transaction_id")]
    public string TransactionId { get; set; }

    /// <summary>
    /// 商户订单号.
    /// </summary>
    [XmlElement("out_trade_no")]
    public string OutTradeNo { get; set; }

    /// <summary>
    /// 总金额.
    /// </summary>
    [XmlElement("total_fee")]
    public int TotalFee { get; set; }

    /// <summary>
    /// 货币种类.
    /// </summary>
    [XmlElement("fee_type")]
    public string FeeType { get; set; }

    /// <summary>
    /// 支付完成时间.
    /// </summary>
    [XmlElement("time_end")]
    public string TimeEnd { get; set; }

    /// <summary>
    /// 用户标识.
    /// </summary>
    [XmlElement("openid")]
    public string Openid { get; set; }

    /// <summary>
    /// 付款银行.
    /// </summary>
    [XmlElement("bank_type")]
    public string BankType { get; set; }

    /// <summary>
    /// 商户appid.
    /// </summary>
    [XmlElement("sub_appid")]
    public string SubAppid { get; set; }

    /// <summary>
    /// 用户openid.
    /// </summary>
    [XmlElement("sub_openid")]
    public string SubOpenid { get; set; }

    [XmlElement("cash_fee")]
    public int CashFee { get; set; }

    [XmlElement("coupon_fee")]
    public int CouponFee { get; set; }
}