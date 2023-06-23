using EasySeries.Post.Models.KuaiDi100;
using EasySeries.Post.Options;

namespace EasySeries.Post;

/// <summary>
/// 快递100interface.
/// </summary>
public interface IEasyPostKuaiDi100
{
    /// <summary>
    /// 快递100-取消寄件(电子面单).
    /// </summary>
    /// <param name="orderCancel"></param>
    /// <param name="kd100SecurityOptions"></param>
    /// <returns>取消寄件结果.</returns>
    Task<KuaidiCancelResponse> Kd100CancelOrderAsync(Kd100OrderCancel orderCancel, KuaiDi100SecurityOptions? kd100SecurityOptions = null);

    /// <summary>
    /// 快递100-寄件(电子面单).
    /// </summary>
    /// <param name="dianziParam"></param>
    /// <param name="kd100SecurityOptions">快递100安全信息.</param>
    /// <returns>请求结果.</returns>
    Task<DianzimiandanResponse> Kd100CreateOrderAsync(DianziParam dianziParam, KuaiDi100SecurityOptions? kd100SecurityOptions = null);

    /// <summary>
    /// 快递100-查询.
    /// </summary>
    /// <param name="query">查询信息.</param>
    /// <param name="kd100SecurityOptions">快递100安全信息.</param>
    /// <returns>查询结果.</returns>
    Task<KuaidiQueryResponse> Kd100QueryOrderAsync(Kd100OrderQuery query, KuaiDi100SecurityOptions? kd100SecurityOptions = null);
}