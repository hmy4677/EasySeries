using EasySerise.Pay.Models.Wechat;

namespace EasySerise.Pay;

/// <summary>
/// 微信支付Interface.
/// </summary>
public interface IEasyPayWechat
{
    /// <summary>
    /// 小程序支付签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="securityInfo">支付安全信息(即时模式用).</param>
    /// <returns>小程序支付签名包.</returns>
    WeAppSignInfo MiniAppSign(string prepayid, WechatPaySecurityInfo? securityInfo = null);

    /// <summary>
    /// 获取支付平台证书(验签用).
    /// </summary>
    /// <param name="securityInfo">支付安全信息(即时模式用).</param>
    /// <returns>支付平台证书.</returns>
    Task<List<PlatCert>> WechatGetCertificatesAsync(WechatPaySecurityInfo? securityInfo = null);

    /// <summary>
    /// 回调通知处理.
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityInfo">支付安全信息(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">处理异常.</exception>
    Task<PayQueryResponse> WechatNotifyHandleAsync(HttpRequest request, WechatPaySecurityInfo? securityInfo = null);

    /// <summary>
    /// 查询支付.
    /// </summary>
    /// <param name="outTradeNo">商户单号(2选1).</param>
    /// <param name="tradeNo">微信支付单号(2选1).</param>
    /// <param name="securityInfo">支付安全信息(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">单号为空.</exception>
    Task<PayQueryResponse> WechatQueryPayAsync(string outTradeNo, string tradeNo, WechatPaySecurityInfo? securityInfo = null);

    /// <summary>
    /// 查询退款.
    /// </summary>
    /// <param name="refundNo">退款单号.</param>
    /// <param name="securityInfo">支付安全信息(即时模式用).</param>
    /// <returns>查询结果.</returns>
    Task<RefundQueryResponse> WechatQueryRefundAsync(string refundNo, WechatPaySecurityInfo? securityInfo = null);

    /// <summary>
    /// 生成预付订单.
    /// </summary>
    /// <param name="payModel">支付信息model.</param>
    /// <param name="securityInfo">支付安全信息(即时模式用).</param>
    /// <returns>预付订单号.</returns>
    Task<string> WechatPrepayAsync(PayModel payModel, WechatPaySecurityInfo? securityInfo = null);

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="refundModel">退款信息.</param>
    /// <param name="securityInfo">支付安全信息(即时模式用).</param>
    /// <returns>结果信息.</returns>
    Task<RefundResponse> WechatRefundAsync(RefundModel refundModel, WechatPaySecurityInfo? securityInfo = null);
}