namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 微信回调通知model.
/// </summary>
internal class NotifyModel
{
    /// <summary>
    /// 通知ID.
    /// </summary>
    internal string id { get; set; } = string.Empty;

    /// <summary>
    /// 通知创建时间.
    /// </summary>
    internal string create_time { get; set; } = string.Empty;

    /// <summary>
    /// 通知类型.
    /// </summary>
    internal string event_type { get; set; } = string.Empty;

    /// <summary>
    /// 通知数据类型.
    /// </summary>
    internal string resource_type { get; set; } = string.Empty;

    /// <summary>
    /// 回调摘要.
    /// </summary>
    internal string summary { get; set; } = string.Empty;

    /// <summary>
    /// 通知数据.
    /// </summary>
    internal ResourceInfo resource { get; set; } = new ResourceInfo();
}

/// <summary>
/// 通知数据.
/// </summary>
internal class ResourceInfo
{
    /// <summary>
    /// 加密算法类型.
    /// </summary>
    internal string algorithm { get; set; } = string.Empty;

    /// <summary>
    /// 数据密文.
    /// </summary>
    internal string ciphertext { get; set; } = string.Empty;

    /// <summary>
    /// 附加数据.
    /// </summary>
    internal string associated_data { get; set; } = string.Empty;

    /// <summary>
    /// 原始类型.
    /// </summary>
    internal string original_type { get; set; } = string.Empty;

    /// <summary>
    /// 随机串.
    /// </summary>
    internal string nonce { get; set; } = string.Empty;
}