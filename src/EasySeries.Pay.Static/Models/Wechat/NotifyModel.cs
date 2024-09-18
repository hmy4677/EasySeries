namespace EasySeries.Pay.Static.Models.Wechat;

/// <summary>
/// 微信回调通知model.
/// </summary>
internal class NotifyModel
{
    /// <summary>
    /// 通知ID.
    /// </summary>
    [JsonProperty("id")]
    internal string Id { get; set; } = string.Empty;

    /// <summary>
    /// 通知创建时间.
    /// </summary>
    [JsonProperty("create_time")]
    internal string CreateTime { get; set; } = string.Empty;

    /// <summary>
    /// 通知类型.
    /// </summary>
    [JsonProperty("event_type")]
    internal string EventType { get; set; } = string.Empty;

    /// <summary>
    /// 通知数据类型.
    /// </summary>
    [JsonProperty("resource_type")]
    internal string EesourceType { get; set; } = string.Empty;

    /// <summary>
    /// 回调摘要.
    /// </summary>
    [JsonProperty("summary")]
    internal string Summary { get; set; } = string.Empty;

    /// <summary>
    /// 通知数据.
    /// </summary>
    [JsonProperty("resource")]
    internal ResourceInfo Resource { get; set; } = new ResourceInfo();
}

/// <summary>
/// 通知数据.
/// </summary>
internal class ResourceInfo
{
    /// <summary>
    /// 加密算法类型.
    /// </summary>
    [JsonProperty("algorithm")]
    internal string Algorithm { get; set; } = string.Empty;

    /// <summary>
    /// 数据密文.
    /// </summary>
    [JsonProperty("ciphertext")]
    internal string Ciphertext { get; set; } = string.Empty;

    /// <summary>
    /// 附加数据.
    /// </summary>
    [JsonProperty("associated_data")]
    internal string AssociatedData { get; set; } = string.Empty;

    /// <summary>
    /// 原始类型.
    /// </summary>
    [JsonProperty("original_type")]
    internal string OriginalType { get; set; } = string.Empty;

    /// <summary>
    /// 随机串.
    /// </summary>
    [JsonProperty("nonce")]
    internal string Nonce { get; set; } = string.Empty;
}
