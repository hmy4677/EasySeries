using EasySeries.MiniProgram.Models.Kuaishou;
using EasySeries.MiniProgram.Utils;

namespace EasySeries.MiniProgram.Static;

public class Kuaishou
{
    /// <summary>
    /// 获取Session.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="appId"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public static async Task<KuaishouSessionResponse> GetSessionAsync(string code, string appId, string secret)
    {
        const string API = "https://open.kuaishou.com/oauth2/mp/code2session";
        var header = new Dictionary<string, string>
        {
            { "content-type", "x-www-form-urlencoded" }
        };
        var query = new Dictionary<string, string>
        {
            { "app_id", appId },
            { "js_code", code },
            { "app_secret", secret }
        };

        var response = await HttpRequest.ExecuteGetAsync<KuaishouSessionResponse>(API, header, query);
        if(response.Result != 1)
        {
            throw new Exception($"登录失败({response.Error},{response.ErrorMsg})");
        }

        return response;
    }
}
