namespace EasySeries.Pay.Models.UnifyTrade;

/// <summary>
/// 中信全付通查询响应.
/// </summary>
[XmlRoot(ElementName = "stream")]
public class UnifyTradeQueryResponse : UnifyTradeResponseBase
{
    /// <summary>
    /// 设备信息.
    /// </summary>
    [XmlElement("device_info")]
    public string DeviceInfo { get; set; } = string.Empty;

    /// <summary>
    /// 交易状态.
    /// </summary>
    [XmlElement("trade_state")]
    public string TradeState { get; set; } = string.Empty;

    /// <summary>
    /// 交易状态描述.
    /// </summary>
    [XmlElement("trade_state_desc")]
    public string TradeStateDesc { get; set; } = string.Empty;

    /// <summary>
    /// 交易类型.
    /// </summary>
    [XmlElement("trade_type")]
    public string TradeType { get; set; } = string.Empty;

    /// <summary>
    /// 商户单号.
    /// </summary>
    [XmlElement("out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 总金额.
    /// </summary>
    [XmlElement("total_fee")]
    public int? TotalFee { get; set; }

    /// <summary>
    /// 货币种类.
    /// </summary>
    [XmlElement("fee_type")]
    public string FeeType { get; set; } = string.Empty;

    /// <summary>
    /// 支付完成时间.
    /// </summary>
    [XmlElement("time_end")]
    public string TimeEnd { get; set; } = string.Empty;

    /// <summary>
    /// 银行类型.
    /// </summary>
    [XmlElement("bank_type")]
    public string BankType { get; set; } = string.Empty;

    /// <summary>
    /// Openid.
    /// </summary>
    [XmlElement("openid")]
    public string Openid { get; set; } = string.Empty;

    /// <summary>
    /// 用户子标识.
    /// </summary>
    [XmlElement("sub_openid")]
    public string SubOpenid { get; set; } = string.Empty;

    /// <summary>
    /// 第三方订单号.
    /// </summary>
    [XmlElement("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// 现金券支付.
    /// </summary>
    [XmlElement("coupon_fee")]
    public int? CouponFee { get; set; }
}