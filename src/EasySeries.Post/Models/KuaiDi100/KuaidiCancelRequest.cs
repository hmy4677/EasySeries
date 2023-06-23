namespace EasySeries.Post.Models.KuaiDi100;

public class KuaidiCancelRequest : SendRequestBase
{
    /// <summary>
    /// param 由其他字段拼接.
    /// </summary>
    public string param { get; set; }
}

public class CacelData
{
    public string taskId { get; set; }
    public string orderId { get; set; }
    public string cancelMsg { get; set; }
}