using EasySeries.Post.Models.SF;
using EasySeries.Post.Options;

namespace EasySeries.Post;

/// <summary>
/// 顺丰Interface.
/// </summary>
public interface IEasyPostSF
{
    /// <summary>
    /// 丰医寄通预检订单.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="securityOptions"></param>
    /// <returns>是否成功.</returns>
    Task<SFYJTPreCheckResponse> SFYJTPreCheckAsync(SFYJTPreCheckRequest request, SFYJTSecurityOptions? securityOptions = null);

    /// <summary>
    /// 顺丰医寄通取消订单.
    /// </summary>
    /// <param name="merchantOrderNo">商户单号.</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>取消响应.</returns>
    Task<SFYJTCancelResponse> SFYJTCancelAsync(string merchantOrderNo, SFYJTSecurityOptions? securityOptions = null);

    /// <summary>
    /// 顺丰医寄通创建订单.
    /// </summary>
    /// <param name="post"></param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>创建订单响应.</returns>
    Task<SFYJTOrderResponse> SFYJTCreateOrderAsync(SFYJTPostInfo post, SFYJTSecurityOptions? securityOptions = null);

    /// <summary>
    /// 顺丰医寄通获取面单打印数据(76*130,不打印logo,默认脱敏项).
    /// </summary>
    /// <param name="merchantOrderNo">商户单号.</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>面单打印数据响应.</returns>
    Task<SFYJTPrintDataReponse> SFYJTGetPrintDataAsync(string merchantOrderNo, SFYJTSecurityOptions? securityOptions = null);

    /// <summary>
    /// 顺丰医寄通查询订单.
    /// </summary>
    /// <param name="trackingType">查询号类型1：根据顺丰运单号查询;2：根据客户订单号查</param>
    /// <param name="trackingNumber">查询号:trackingType=1,则此值为顺丰运单号,如果trackingType=2,则此值为客户订单号</param>
    /// <param name="phone">收件人电话后四位（trackingType=1时如果查不到路由信息，请尝试传该字段）</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>顺丰医寄通查询订单响应</returns>
    Task<SFYJTQueryResponse> SFYJTQueryAsync(string trackingType, string trackingNumber, string? phone = null, SFYJTSecurityOptions? securityOptions = null);
}