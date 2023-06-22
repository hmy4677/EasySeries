namespace EasySerise.Post.Models.JDL;

/// <summary>
/// 响应基类.
/// </summary>
public class JDLResponseBase
{
    /// <summary>
    /// 响应编号.
    /// </summary>
    public int code { get; set; }

    /// <summary>
    /// 是否成功.
    /// </summary>
    public bool success { get; set; }

    /// <summary>
    /// 响应描述.
    /// </summary>
    public string msg { get; set; }

    /// <summary>
    /// 响应子描述.
    /// </summary>
    public string subMsg { get; set; }

    /// <summary>
    /// 耗时.
    /// </summary>
    public long mills { get; set; }

    /// <summary>
    /// 请求id.
    /// </summary>
    public string requestId { get; set; }
}