namespace EasySeries.MiniProgram.Models.Wechat;

public class WechatSendSubMsgRequest
{
    /// <summary>
    /// AccessToken.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// 所需下发的订阅模板id.
    /// </summary>
    public string TemplateId { get; set; }

    /// <summary>
    /// 接收者（用户）的 openid.
    /// </summary>
    public string OpenId { get; set; }

    /// <summary>
    /// Key数组与value对应.
    /// </summary>
    public string[] Keys { get; set; }

    /// <summary>
    /// Value数组与key对应.
    /// </summary>
    public string[] Values { get; set; }

    /// <summary>
    /// 点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar)
    /// </summary>
    public string Page { get; set; } = "index";

    /// <summary>
    /// 跳转小程序类型：developer为开发版；trial为体验版；formal为正式版；默认为正式版.
    /// </summary>
    public string EnvVersion { get; set; } = "formal为正式版";
}