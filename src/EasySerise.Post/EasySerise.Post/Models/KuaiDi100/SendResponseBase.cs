namespace EasySerise.Post.Models.KuaiDi100;

/// <summary>
/// 邮件发送(下单)响应基类.
/// </summary>
public class SendResponseBase
{
    /// <summary>
    /// boolean 提交结果 true提交成功，false失败.
    /// </summary>
    public bool result { get; set; }

    /// <summary>
    /// string 返回编码.
    /// </summary>
    public string returnCode { get; set; }

    /// <summary>
    /// string 返回报文描述.
    /// </summary>
    public string message { get; set; }
}