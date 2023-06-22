namespace EasySerise.Post.Models.KuaiDi100;

/// <summary>
/// 快递查询请求.
/// </summary>
public class KuaidiQueryRequest
{
    /// <summary>
    /// 授权码，请申请企业版获取.
    /// </summary>
    public string customer { get; set; }

    /// <summary>
    /// 签名， 用于验证身份，.
    /// </summary>
    public string sign { get; set; }

    /// <summary>
    /// 其它参数.
    /// </summary>
    public string param { get; set; }
}

public class KuaidiQueryParam
{
    /// <summary>
    /// 查询的快递公司的编码， 一律用小写字母.
    /// </summary>
    public string com { get; set; }

    /// <summary>
    /// 查询的快递单号.
    /// </summary>
    public string num { get; set; }

    /// <summary>
    /// 收、寄件人的电话号码.
    /// </summary>
    public string? phone { get; set; }

    /// <summary>
    /// 1：开通行政区域解析功能以及物流轨迹增加物流状态名称 4: 开通行政解析功能以及物流轨迹增加物流高级状态名称、状态值并且返回出发、目的及当前城市信息
    /// </summary>
    public int resultv2 { get; set; }
}