namespace EasySeries.Post.Models.SF;

public abstract class SFYJTResponseBase
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("code")]
    public string Code { get; set; } = string.Empty;
}