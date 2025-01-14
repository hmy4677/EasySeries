namespace EasySeries.MiniProgram.Models.Douyin;

public class DouyinResponse
{
    [JsonProperty("err_no")]
    public long ErrNo { get; set; }

    [JsonProperty("err_msg")]
    public string ErrMsg { get; set; }
}
