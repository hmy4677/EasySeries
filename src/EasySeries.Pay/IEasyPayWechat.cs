﻿using EasySeries.Pay.Enums;
using EasySeries.Pay.Models.Wechat;
using EasySeries.Pay.Options;

namespace EasySeries.Pay;

/// <summary>
/// 微信支付Interface.
/// </summary>
public interface IEasyPayWechat
{
    /// <summary>
    /// 生成预付订单(JSAPI).
    /// </summary>
    /// <param name="payModel">支付信息model.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>预付订单号.</returns>
    Task<string> WechatPrepayAsync(JSAPIPayModel payModel, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 生成预付订单(APP).
    /// </summary>
    /// <param name="payModel">支付信息model.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>预付订单号.</returns>
    Task<string> WechatPrepayAsync(AppPayModel payModel, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 支付查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号(2选1).</param>
    /// <param name="tradeNo">微信支付单号(2选1).</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="ArgumentNullException">单号为空.</exception>
    Task<PayQueryResponse> WechatQueryPayAsync(string outTradeNo, string tradeNo, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="refundModel">退款信息.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>结果信息.</returns>
    Task<RefundResponse> WechatRefundAsync(RefundModel refundModel, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 查询退款.
    /// </summary>
    /// <param name="refundNo">退款单号.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>查询结果.</returns>
    Task<RefundQueryResponse> WechatQueryRefundAsync(string refundNo, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 回调通知处理.
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">处理异常.</exception>
    Task<PayQueryResponse> WechatNotifyHandleAsync(HttpRequest request, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 退款回调通知处理.
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付查询结果.</returns>
    /// <exception cref="Exception">处理异常.</exception>
    Task<RefundNotify> WechatRefundNotifyHandleAsync(HttpRequest request, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// JSAPI签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="appIdType">appId类型.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>JSAPI签名信息.</returns>
    JSAPISignInfo JSAPISign(string prepayid, JSAPIAppIdTypes appIdType, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// APP支付签名.
    /// </summary>
    /// <param name="prepayid">预付订单id.</param>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>APP支付签名包.</returns>
    AppSignInfo AppSign(string prepayid, WechatPaySecurityOptions? securityOptions = null);

    /// <summary>
    /// 获取支付平台证书(验签用).
    /// </summary>
    /// <param name="securityOptions">支付安全(即时模式用).</param>
    /// <returns>支付平台证书.</returns>
    Task<List<PlatCert>> WechatGetCertificatesAsync(WechatPaySecurityOptions? securityOptions = null);
}