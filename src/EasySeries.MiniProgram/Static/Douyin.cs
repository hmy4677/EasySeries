using EasySeries.MiniProgram.Models.Douyin;
using EasySeries.MiniProgram.Utils;

namespace EasySeries.MiniProgram.Static;

public class Douyin
{
    /// <summary>
    /// 获取Session.
    /// </summary>
    /// <param name="code"></param>
    /// <param name="anonymousCode"></param>
    /// <param name="appId"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<DouyinSessionResponse> GetSessionAsync(string code, string anonymousCode, string appId, string secret)
    {
        const string API = "https://developer.toutiao.com/api/apps/v2/jscode2session";
        var request = new
        {
            appid = appId,
            secret,
            code,
            anonymous_code = anonymousCode
        };

        var response = await HttpRequest.ExecutePostAsync<DouyinSessionResponse>(API, request);
        if(response.ErrNo != 0)
        {
            throw new Exception($"[{response.ErrNo}]{response.ErrMsg}");
        }

        return response;
    }
}
