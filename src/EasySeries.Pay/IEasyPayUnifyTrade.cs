using EasySeries.Pay.Models.UnifyTrade;
using EasySeries.Pay.Options;

namespace EasySeries.Pay;

/// <summary>
/// 中信全付通Interface.
/// </summary>
public interface IEasyPayUnifyTrade
{
    /// <summary>
    /// 中信全付通回调处理.回应:await Response.WriteAsync("SUCCESS/FAIL");
    /// </summary>
    /// <param name="request">网络请求.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>回调通知处理.</returns>
    /// <exception cref="Exception"></exception>
    Task<dynamic> UnifyTradeCallbackHandleAsync(HttpRequest request, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通支付下单.
    /// </summary>
    /// <param name="native"></param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>支付下单响应.</returns>
    Task<dynamic> UnifyTradeNativeAsync(UnifyTradeNative native, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>查询响应.</returns>
    Task<UnifyTradeQueryResponse> UnifyTradeQueryAsync(string outTradeNo, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通查询退款.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="outRefundNo">退款单号.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>查询退款响应.</returns>
    Task<UnifyTradeQueryRefundResponse> UnifyTradeQueryRefundAsync(string outTradeNo, string outRefundNo, UnifyTradeSecurityOptions? securityOptions = null);

    /// <summary>
    /// 中信全付通退款.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="outRefundNo">商户退款单号.</param>
    /// <param name="totalFee">订单总额(单位:分).</param>
    /// <param name="refundFee">退款金额(单位:分).</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>退款响应.</returns>
    Task<UnifyTradeRefundResponse> UnifyTradeRefundAsync(string outTradeNo, string outRefundNo, int totalFee, int refundFee, UnifyTradeSecurityOptions? securityOptions = null);
}