using Aop.Api.Response;
using EasySerise.Pay.Models.Ali;

namespace EasySerise.Pay;

/// <summary>
/// 阿里支付Interface.
/// </summary>
public interface IEasyPayAli
{
    /// <summary>
    /// 关闭订单.
    /// </summary>
    /// <param name="outTradeNO">商户单号.</param>
    /// <returns>关闭退款响应结果.</returns>
    AlipayTradeCloseResponse AlipayClose(string outTradeNO);

    /// <summary>
    /// 回调通知处理.
    /// </summary>
    /// <param name="request">回调通知请求.</param>
    /// <returns>通知内容.</returns>
    /// <exception cref="Exception">请求异常.</exception>
    NofityModel AlipayNotifyHandle(HttpRequest request);

    /// <summary>
    /// 账单查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <returns>查询响应结果.</returns>
    AlipayTradeQueryResponse AlipayQuery(string outTradeNo);

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="refundModel">退款模型.</param>
    /// <returns>退款响应结果.</returns>
    AlipayTradeRefundResponse AlipayRefund(AliPayRefundModel refundModel);

    /// <summary>
    /// 支付(手机网页).
    /// </summary>
    /// <param name="payModel">支付model.</param>
    /// <returns>支付响应结果.</returns>
    AlipayTradeWapPayResponse AlipayWap(AliPayModel payModel);
}