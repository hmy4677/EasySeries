using System.Xml.Serialization;

namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 统一响应结果.
/// </summary>
public class ResponseBase : SignBase
{
    /// <summary>
    /// 返回状态码.
    /// </summary>
    [JsonProperty("return_code")]
    [XmlElement("return_code")]
    public string ReturnCode { get; set; } = string.Empty;

    /// <summary>
    /// 返回信息.
    /// </summary>
    [JsonProperty("return_msg")]
    [XmlElement("return_msg")]
    public string ReturnMsg { get; set; } = string.Empty;

    /// <summary>
    /// 业务结果(SUCCESS表示成功，FAIL表示失败).
    /// </summary>
    [JsonProperty("result_code")]
    [XmlElement("result_code")]
    public string ResultCode { get; set; } = string.Empty;

    /// <summary>
    /// 商户号.
    /// </summary>
    [JsonProperty("mch_id")]
    [XmlElement("mch_id")]
    public string MchId { get; set; } = string.Empty;

    /// <summary>
    /// 错误代码.
    /// </summary>
    [JsonProperty("err_code")]
    [XmlElement("err_code")]
    public string ErrCode { get; set; } = string.Empty;

    /// <summary>
    /// 错误代码描述.
    /// </summary>
    [JsonProperty("err_msg")]
    [XmlElement("err_msg")]
    public string ErrMsg { get; set; } = string.Empty;
}
