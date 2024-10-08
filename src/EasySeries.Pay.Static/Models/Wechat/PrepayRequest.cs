﻿namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 预付请求.
/// </summary>
internal class PrepayRequest
{
    /// <summary>
    /// 应用id.
    /// </summary>
    [JsonProperty("appid")]
    internal string AppId { get; set; } = string.Empty;

    /// <summary>
    /// 直连商户号.
    /// </summary>
    [JsonProperty("mchid")]
    internal string Mchid { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述.
    /// </summary>
    [JsonProperty("description")]
    internal string Description { get; set; } = string.Empty;

    /// <summary>
    /// 商户订单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    internal string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 通知地址.
    /// </summary>
    [JsonProperty("notify_url")]
    internal string NotifyUrl { get; set; } = string.Empty;

    /// <summary>
    /// 订单金额信息.
    /// </summary>
    [JsonProperty("amount")]
    internal OrderAmount Amount { get; set; } = new();
}