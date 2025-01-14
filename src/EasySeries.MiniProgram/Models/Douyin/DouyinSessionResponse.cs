namespace EasySeries.MiniProgram.Models.Douyin;

public class DouyinSessionResponse : DouyinResponse
{
    [JsonProperty("data")]
    public DouyinSessionData Data { get; set; }
}
public class DouyinSessionData
{
    /// <summary>
    /// 会话密钥，如果请求时有 code 参数才会返回
    /// </summary>
    [JsonProperty("session_key")]
    public string? SessionKey { get; set; }

    /// <summary>
    /// 用户在当前小程序的 ID，如果请求时有 code 参数才会返回
    /// </summary>
    [JsonProperty("openid")]
    public string? OpenId { get; set; }

    /// <summary>
    /// 匿名用户在当前小程序的 ID，如果请求时有 anonymous_code 参数才会返回
    /// </summary>
    [JsonProperty("anonymous_openid")]
    public string? AnonymousOpenId { get; set; }

    /// <summary>
    /// 用户在小程序平台的唯一标识符，请求时有 code 参数才会返回。如果开发者拥有多个小程序，可通过 unionid 来区分用户的唯一性。
    /// </summary>
    [JsonProperty("unionid")]
    public string? UnionId { get; set; }
}
