using EasySeries.MiniProgram.Models.Baidu;
using EasySeries.MiniProgram.Utils;

namespace EasySeries.MiniProgram.Static;

public class Baidu
{
    /// <summary>
    /// 获取Session.
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static async Task<BaiduSessionResponse> GetSessionAsync(string accessToken, string code)
    {
        var api = $"https://openapi.baidu.com/rest/2.0/smartapp/getsessionkey?access_token={accessToken}&code={code}";
        var response = await HttpRequest.ExecuteGetAsync<BaiduSessionResponse>(api, null, null);
        if(response.ErrorCode != 0)
        {
            throw new Exception(response.ErrorMsg);
        }

        return response;
    }

    /// <summary>
    /// access token.
    /// </summary>
    /// <param name="appKey"></param>
    /// <param name="secret"></param>
    /// <returns></returns>
    public static async Task<BaiduAccessTokenResponse> GetAccessTokenAsync(string appKey, string secret)
    {
        var api = $"https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id={appKey}&client_secret={secret}&scope=smartapp_snsapi_base";
        var response = await HttpRequest.ExecuteGetAsync<BaiduAccessTokenResponse>(api, null, null);
        if(!string.IsNullOrEmpty(response.Error))
        {
            throw new Exception($"{response.Error},{response.ErrorDescription}");
        }

        return response;
    }
}
