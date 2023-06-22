using EasySerise.Post.Models.JDL;
using EasySerise.Post.Options;

namespace EasySerise.Post;

/// <summary>
/// 京东物流interface.
/// </summary>
public interface IEasyPostJDL
{
    /// <summary>
    /// JDL-取消寄件.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="cancelRequest">取消寄件信息.</param>
    /// <returns>请求结果.</returns>
    Task<JDLCancelOrderResponse> JDLCancelOrderAsync(JDLCancelOrderRequest cancelRequest, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-预检订单.
    /// </summary>
    /// <param name="senderAddress">发件地址.</param>
    /// <param name="receiverAddress">收件地址.</param>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <returns>预检结果.</returns>
    Task<JDLCheckPreOrderResponse> JDLCheckPreOrderAsync(string senderAddress, string receiverAddress, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-下单寄件.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="createRequest">寄件信息.</param>
    /// <returns>请求结果.</returns>
    Task<JDLCreateOrderResponse> JDLCreatOrderAsync(JDLCreateOrderRequest createRequest, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-获取云打印数据.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="jdlOrders">订单号数组.</param>
    /// <returns>云打印数据.</returns>
    Task<JDLPullDataResponse> JDLGetPrintDataAsync(string[] jdlOrders, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-获取运单模板.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <returns>运单模板.</returns>
    Task<JDLTemplateListResponse> JDLGetTemplateListAsync(JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-查询.
    /// </summary>
    /// <param name="postNo">京东快递号.</param>
    /// <param name="jdlSecurityOptions">京东物流安全信息</param>
    /// <returns>查询结果.</returns>
    Task<JDLQueryOrderResult> JDLQueryOrderAsyncc(string postNo, JDLSecurityOptions? jdlSecurityOptions = null);
}