﻿namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信查询退款响应.
/// </summary>
public class WechatQueryRefundResponse
{
    /// <summary>
    /// 微信支付退款单号
    /// </summary>
    [JsonProperty("refund_id")]
    public string RefundId { get; set; } = string.Empty;

    /// <summary>
    /// 商户退款单号
    /// </summary>
    [JsonProperty("out_refund_no")]
    public string OutRefundNo { get; set; } = string.Empty;

    /// <summary>
    /// 微信支付订单号
    /// </summary>
    [JsonProperty("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// 商户订单号
    /// </summary>
    [JsonProperty("out_trade_no")]
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 退款渠道
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; } = string.Empty;

    /// <summary>
    /// 退款入账账户
    /// </summary>
    [JsonProperty("user_received_account")]
    public string UserReceivedAccount { get; set; } = string.Empty;

    /// <summary>
    /// 退款成功时间
    /// </summary>
    [JsonProperty("success_time")]
    public string SuccessTime { get; set; } = string.Empty;

    /// <summary>
    /// 退款创建时间
    /// </summary>
    [JsonProperty("create_time")]
    public string CreateTime { get; set; } = string.Empty;

    /// <summary>
    /// 退款状态
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// 资金账户
    /// </summary>
    [JsonProperty("funds_account")]
    public string FundsAccount { get; set; } = string.Empty;

    /// <summary>
    /// 金额信息
    /// </summary>
    [JsonProperty("amount")]
    public WechatRefundAmount Amount { get; set; } = new();
}
