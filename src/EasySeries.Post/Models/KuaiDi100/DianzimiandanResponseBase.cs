namespace EasySeries.Post.Models.KuaiDi100;

/// <summary>
/// 电子面单响应base.
/// </summary>
public class DianzimiandanResponseBase
{
    /// <summary>
    /// boolean 提交结果 true提交成功，false失败.
    /// </summary>
    public bool success { get; set; }

    /// <summary>
    /// int 返回编码 200为成功.
    /// </summary>
    public int code { get; set; }

    /// <summary>
    /// string 返回报文描述.
    /// </summary>
    public string message { get; set; }
}