using System.Xml.Serialization;

namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 中信银行查询退款响应.
/// </summary>
public class CiticBankQueryRefundResponse : ResponseBase
{
    /// <summary>
    /// 商户系统内部的订单号
    /// </summary>
    [JsonProperty("out_trade_no")]
    [XmlElement("out_trade_no")]
    public string OutTradeNo { get; set; }

    /// <summary>
    /// 商户退款单号
    /// </summary>
    [JsonProperty("out_refund_no")]
    [XmlElement("out_refund_no")]
    public string OutRefundNo { get; set; }

    /// <summary>
    /// 退款总金额,单位为分,可以做部分退款
    /// </summary>
    [JsonProperty("refund_fee")]
    [XmlElement("refund_fee")]
    public string RefundFee { get; set; }

    /// <summary>
    /// SUCCESS—退款成功
    /// FAIL—退款失败
    /// PROCESSING—退款处理中
    /// CHANGE—转入代发，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，资金回流到商户的现金帐号，需要商户人工干预，通过线下或者平台转账的方式进行退款。
    /// </summary>
    [JsonProperty("refund_status")]
    [XmlElement("refund_status")]
    public string RefundStatus { get; set; }

    /// <summary>
    /// ORIGINAL—原路退款，默认
    /// BALANCE—退回到余额
    /// </summary>

    [JsonProperty("refund_channel")]
    [XmlElement("refund_channel")]
    public string RefundChannel { get; set; }

    /// <summary>
    /// 第三方平台交易号
    /// </summary>
    [JsonProperty("transaction_id")]
    [XmlElement("transaction_id")]
    public string TransactionId { get; set; }

    /// <summary>
    /// 该笔退款所对应的交易的订单金额，单位为分，只能为整数
    /// </summary>
    [JsonProperty("cash_fee")]
    [XmlElement("cash_fee")]
    public int CashFee { get; set; }

    /// <summary>
    /// 代金券或立减优惠退款金额=订单金额-退款金额，单位为分，注意：满立减金额不会退回
    /// </summary>
    [JsonProperty("coupon_refund_fee")]
    [XmlElement("coupon_refund_fee")]
    public int CouponRefundFee { get; set; }

    /// <summary>
    /// yyyyMMddHHmmss
    /// </summary>
    [JsonProperty("refund_time")]
    [XmlElement("refund_time")]
    public string RefundTime { get; set; }

    /// <summary>
    /// 第三方退款单号
    /// </summary>
    [JsonProperty("refund_id")]
    [XmlElement("refund_id")]
    public string RefundId { get; set; }

    /// <summary>
    /// 原交易成功时返回，YYYYMMDD
    /// </summary>
    [JsonProperty("settleDate")]
    [XmlElement("settleDate")]
    public string SettleDate { get; set; }
}