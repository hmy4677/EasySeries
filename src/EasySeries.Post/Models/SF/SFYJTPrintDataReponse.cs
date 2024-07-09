namespace EasySeries.Post.Models.SF;

/// <summary>
/// 顺丰医寄能打印数据响应.
/// </summary>
public class SFYJTPrintDataReponse : SFYJTResponseBase
{
    [JsonProperty("result")]
    public SFYJTPrintDataResult Result { get; set; }
}

public class SFYJTPrintDataResult
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; set; }

    [JsonProperty("errorCode")]
    public string ErrorCode { get; set; }

    [JsonProperty("requestId")]
    public string RequestId { get; set; }

    [JsonProperty("obj")]
    public SFYJTPrintDataResultObj Obj { get; set; }
}

public class SFYJTPrintDataResultObj
{
    [JsonProperty("sysCode")]
    public string SysCode { get; set; }

    [JsonProperty("templateCode")]
    public string TemplateCode { get; set; }

    [JsonProperty("files")]
    public List<SFYJTPrintDataResultObjFile> Files { get; set; }
}

public class SFYJTPrintDataResultObjFile
{
    [JsonProperty("pageCount")]
    public int PageCount { get; set; }

    [JsonProperty("seqNo")]
    public int SeqNo { get; set; }

    [JsonProperty("areaNo")]
    public int AreaNo { get; set; }

    [JsonProperty("pageNo")]
    public int PageNo { get; set; }

    [JsonProperty("documentSize")]
    public int DocumentSize { get; set; }

    [JsonProperty("waybillNo")]
    public string WaybillNo { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }
}