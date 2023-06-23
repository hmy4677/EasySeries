namespace EasySeries.Post.Models.KuaiDi100;

/// <summary>
/// 寄件(下单)请求基类.
/// </summary>
public class SendRequestBase
{
    /// <summary>
    /// 业务类型.
    /// </summary>
    public string method { get; set; }

    /// <summary>
    /// 授权码，请到快递100页面申请企业版接口获取.
    /// </summary>
    public string key { get; set; }

    /// <summary>
    /// 32位大写签名.
    /// </summary>
    public string sign { get; set; }

    /// <summary>
    /// 时间戳如：1576123932000.
    /// </summary>
    public string t { get; set; }
}