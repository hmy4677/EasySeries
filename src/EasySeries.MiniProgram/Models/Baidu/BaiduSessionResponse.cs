namespace EasySeries.MiniProgram.Models.Baidu;

public class BaiduSessionResponse
{
    [JsonProperty("error_code")]
    public int ErrorCode { get; set; }

    [JsonProperty("error_msg")]
    public string ErrorMsg { get; set; }

    [JsonProperty("errno")]
    public int ErrNo { get; set; }

    [JsonProperty("errmsg")]
    public string ErrMsg { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    [JsonProperty("data")]
    public BaiduSessionData Data { get; set; }
}

public class BaiduSessionData
{
    [JsonProperty("session_key")]
    public string SessionKey { get; set; }

    [JsonProperty("open_id")]
    public string OpenId { get; set; }
}
