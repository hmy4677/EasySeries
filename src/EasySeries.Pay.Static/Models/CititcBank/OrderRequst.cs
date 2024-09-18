namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 订单请求.
/// </summary>
internal class OrderRequst : SignBase
{
    /// <summary>
    /// 接口类型.
    /// </summary>
    [JsonProperty("service")]
    internal string Service { get; } = "unified.trade.native";

    /// <summary>
    /// 版本号
    /// </summary>
    [JsonProperty("version")]
    internal string Version { get; } = "3.0.1";

    /// <summary>
    /// 通知地址.
    /// </summary>
    [JsonProperty("notify_url")]
    internal string NotifyUrl { get; set; } = string.Empty;

    /// <summary>
    /// 商户号.
    /// </summary>
    [JsonProperty("mch_id")]
    internal string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 商户订单号.
    /// </summary>
    [JsonProperty("out_trade_no")]
    internal string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 总金额(单位:分).
    /// </summary>
    [JsonProperty("total_fee")]
    internal int TotalFee { get; set; }

    /// <summary>
    /// 终端ip.
    /// </summary>
    [JsonProperty("mch_create_ip")]
    internal string MchCreateIp { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述.
    /// </summary>
    [JsonProperty("body")]
    internal string Body { get; set; } = string.Empty;

    /// <summary>
    /// 交易类型 ZXCOD：中信聚合码，支持微信、支付宝、银联APP扫码 JSAPI：微信公众号支付，支持微信浏览器调起支付
    /// ALCRE：支付宝创建订单，支持支付宝浏览器调起支付APP：支持APP端调起支付 ZXWLT：中信钱包码
    /// </summary>
    [JsonProperty("trade_type")]
    internal string TradeType { get; set; } = string.Empty;

    /// <summary>
    /// 订单标题【支付宝】.
    /// </summary>
    [JsonProperty("subject")]
    internal string Subject { get; set; } = string.Empty;

    /// <summary>
    /// 买家支付宝帐号【支付宝】.
    /// </summary>
    [JsonProperty("buyer_logon_id")]
    internal string BuyerLogonId { get; set; } = string.Empty;

    /// <summary>
    /// 买家支付宝唯一用户号【支付宝】(2088开头的16位纯数字).
    /// </summary>
    [JsonProperty("buyer_id")]
    internal string BuyerId { get; set; } = string.Empty;

    /// <summary>
    /// 卖家支付宝用户ID【支付宝】.
    /// </summary>
    [JsonProperty("sellerid")]
    internal string SellerId { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识【微信】.
    /// </summary>
    [JsonProperty("openid")]
    internal string Openid { get; set; } = string.Empty;

    /// <summary>
    /// 子商户公众账号ID【微信】
    /// </summary>
    [JsonProperty("sub_appid")]
    internal string SubAppid { get; set; } = string.Empty;

    /// <summary>
    /// 用户子标识【微信】
    /// </summary>
    [JsonProperty("sub_openid")]
    internal string SubOpenid { get; set; } = string.Empty;
}
