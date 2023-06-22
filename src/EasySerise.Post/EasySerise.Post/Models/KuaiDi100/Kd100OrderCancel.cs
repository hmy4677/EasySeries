namespace EasySerise.Post.Models.KuaiDi100;

/// <summary>
/// 快递100订单取消.
/// </summary>
public class Kd100OrderCancel
{
    /// <summary>
    /// 快递公司(小写拼音).
    /// </summary>
    public string Kuaidicom { get; set; }

    /// <summary>
    /// 快递单号.
    /// </summary>
    public string Kuaidinum { get; set; }

    /// <summary>
    /// 月结账号.
    /// </summary>
    public string PartnerId { get; set; }

    /// <summary>
    /// 快递公司订单号，对应下单时返回的kdComOrderNum，如果下单时有返回该字段，则取消时必填，否则可以不填.
    /// </summary>
    public string OrderId { get; set; }
}