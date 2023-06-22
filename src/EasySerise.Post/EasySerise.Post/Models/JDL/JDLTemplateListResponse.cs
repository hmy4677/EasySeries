namespace EasySerise.Post.Models.JDL;

/// <summary>
/// 获取运单模板响应.
/// </summary>
public class JDLTemplateListResponse
{
    public string code { get; set; }
    public string message { get; set; }
    public JDLTemplateData datas { get; set; }
}

public class JDLTemplateData
{
    public List<JDLStandardTemplate> sDatas { get; set; }
    public List<JDLUserTemplate> uDatas { get; set; }
}

public class JDLStandardTemplate
{
    public string cpCode { get; set; }
    public List<JDLStandardTemplateDTO> standardTemplates { get; set; }
}

public class JDLStandardTemplateDTO
{
    public int standardTemplateId { get; set; }
    public string standardTemplateName { get; set; }
    public string standardTemplateUrl { get; set; }
    public string standardWaybillType { get; set; }
}

public class JDLUserTemplate
{
    public string cpCode { get; set; }
    public List<JDLUserTemplateDTO> userStdTemplates { get; set; }
}

public class JDLUserTemplateDTO
{
    public List<string> keys { get; set; }
    public string userStdTemplateUrl { get; set; }
    public int userStdTemplateId { get; set; }
    public string userStdTemplateName { get; set; }
}