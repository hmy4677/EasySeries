namespace EasySeries.Post.Models.JDL;

/// <summary>
/// 京东物流云打印获取模板请求.
/// </summary>
public class JDLTemplateListRequest
{
    public string templateType { get; set; } = "1";
    public string cpCode { get; set; } = "JD";
    //public string wayTempleteType { get; set; }
    //public string templateId { get;set; }
    //public string isvResourceType { get; set; }
    //public string pin { get; set; }
}