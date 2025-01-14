namespace EasySeries.MiniProgram.Models.Kuaishou;

public class KuaishouSessionResponse
{
    [JsonProperty("result")]
    public int Result { get; set; }

    [JsonProperty("session_key")]
    public string SessionKey { get; set; }

    [JsonProperty("open_id")]
    public string OpenId { get; set; }

    [JsonProperty("error")]
    public string? Error { get; set; }

    [JsonProperty("error_msg")]
    public string? ErrorMsg { get; set; }
}