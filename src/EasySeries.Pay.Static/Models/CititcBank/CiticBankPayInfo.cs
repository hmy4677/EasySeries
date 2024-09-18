namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 中信银行支付信息.
/// </summary>
public class CiticBankPayInfo
{
    /// <summary>
    /// 商户订单号.
    /// </summary>
    public string OutTradeNo { get; set; } = string.Empty;

    /// <summary>
    /// 总金额(单位:分).
    /// </summary>
    public int TotalFee { get; set; }

    /// <summary>
    /// 终端ip.
    /// </summary>
    public string MchCreateIp { get; set; } = string.Empty;

    /// <summary>
    /// 商品描述.
    /// </summary>
    public string Body { get; set; } = string.Empty;

    /// <summary>
    /// 交易类型.
    /// ZXCOD：中信聚合码，支持微信、支付宝、银联APP扫码.
    /// JSAPI：微信公众号支付，支持微信浏览器调起支付.
    /// ALCRE：支付宝创建订单，支持支付宝浏览器调起支付.
    /// APP：支持APP端调起支付.
    /// ZXWLT：中信钱包码.
    /// </summary>
    public string TradeType { get; set; } = string.Empty;

    /// <summary>
    /// 订单标题【支付宝】.
    /// </summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>
    /// 买家支付宝帐号【支付宝】.
    /// </summary>
    public string BuyerLogonId { get; set; } = string.Empty;

    /// <summary>
    /// 买家支付宝唯一用户号【支付宝】(2088开头的16位纯数字).
    /// </summary>
    public string BuyerId { get; set; } = string.Empty;

    /// <summary>
    /// 卖家支付宝用户ID【支付宝】.
    /// </summary>
    public string SellerId { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识【微信】.
    /// </summary>
    public string Openid { get; set; } = string.Empty;

    /// <summary>
    /// 子商户公众账号ID【微信】
    /// </summary>
    public string SubAppid { get; set; } = string.Empty;

    /// <summary>
    /// 用户子标识【微信】
    /// </summary>
    public string SubOpenid { get; set; } = string.Empty;
}
