namespace EasySeries.MiniProgram.Models.Baidu;

public class BaiduAccessTokenResponse
{
    [JsonProperty("error_description")]
    public string ErrorDescription { get; set; } //number  错误码

    [JsonProperty("error")]
    public string Error { get; set; } //string 错误信息

    /// <summary>
    /// 获取到的凭证.
    /// </summary>
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    /// 凭证有效时间，单位：秒。目前是7200秒之内的值.
    /// </summary>
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
}
