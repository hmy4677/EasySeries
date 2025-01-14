using EasySeries.MiniProgram.Models.Wechat;
using EasySeries.MiniProgram.Utils;

namespace EasySeries.MiniProgram.Static;

/// <summary>
/// 微信小程序.
/// </summary>
public class Wechat
{
    /// <summary>
    /// 获取Session.
    /// </summary>
    /// <param name="code">code.</param>
    /// <param name="appId">appid.</param>
    /// <param name="secret">app secret.</param>
    /// <returns>SessionResponse.</returns>
    public static async Task<WechatSessionResponse> GetSessionAsync(string code, string appId, string secret)
    {
        const string API = "https://api.weixin.qq.com/sns/jscode2session";
        var request = new
        {
            appid = appId,
            secret,
            js_code = code,
            grant_type = "authorization_code"
        };

        var response = await HttpRequest.ExecutePostAsync<WechatSessionResponse>(API, request);
        if(response.ErrCode != 0)
        {
            throw new Exception(response.ErrMsg);
        }

        return response;
    }

    /// <summary>
    /// 获取AccessToken.
    /// </summary>
    /// <param name="appId">appId.</param>
    /// <param name="secret">secret.</param>
    /// <returns>WechatAccessTokenResponse</returns>
    public static async Task<WechatAccessTokenResponse> GetWechatAccessTokenAsync(string appId, string secret)
    {
        var api = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appId}&secret={secret}";
        var response = await HttpRequest.ExecutePostAsync<WechatAccessTokenResponse>(api);
        if(response.ErrCode != 0)
        {
            throw new Exception(response.ErrMsg);
        }

        return response;
    }

    /// <summary>
    /// 获取用户手机号.
    /// </summary>
    /// <param name="accessToken">accessToken.</param>
    /// <param name="code">mobile code.</param>
    /// <returns>WechatMobileResponse.</returns>
    public static async Task<WechatMobileResponse?> GetWechatUserMobileAsync(string accessToken, string code)
    {
        var api = $"https://api.weixin.qq.com/wxa/business/getuserphonenumber?access_token={accessToken}";
        var response = await HttpRequest.ExecutePostAsync<WechatMobileResponse>(api, new { code });
        if(response.ErrCode != 0)
        {
            throw new Exception(response.ErrMsg);
        }

        return response;
    }

    /// <summary>
    /// 获取小程序码-无限.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<byte[]?> GetUnLimitQrCodeAsync(WechatUnLimitQrCodeRequest input)
    {
        var api = $"https://api.weixin.qq.com/wxa/getwxacodeunlimit?access_token={input.AccessToken}";
        var restClient = new RestClient(api);
        var restRequest = new RestRequest();
        restRequest.AddBody(new
        {
            path = input.Path,
            scene = input.Scene,
            env_version = input.EnvVersion,
            width = input.Width,
            auto_color = input.AutoColor,
            is_hyaline = input.IsHyaline
        }, ContentType.Json);
        var response = await restClient.ExecutePostAsync(restRequest);
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ErrorMessage);
        }

        if(response.ContentType == "image/jpeg")
        {
            return response.RawBytes;
        }

        var error = JsonConvert.DeserializeObject<WechatResponse>(response.Content ?? "");
        throw new Exception($"[{error!.ErrCode}]{error.ErrMsg}");
    }

    /// <summary>
    /// 获取微信小程序码-有限.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<byte[]?> GetLimitQrCodeAsync(WechatLimitQrCodeRequest input)
    {
        var api = $"https://api.weixin.qq.com/wxa/getwxacode?access_token={input.AccessToken}";
        var request = new
        {
            path = input.Path,
            env_version = input.EnvVersion,
            auto_color = input.AutoColor,
            width = input.Width,
            is_hyaline = input.IsHyaline
        };

        var restClient = new RestClient(api);
        var restRequest = new RestRequest();
        restRequest.AddBody(request, ContentType.Json);
        var response = await restClient.ExecutePostAsync(restRequest);
        if(!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ErrorMessage);
        }

        if(response.ContentType == "image/jpeg")
        {
            return response.RawBytes;
        }

        var error = JsonConvert.DeserializeObject<WechatResponse>(response.Content ?? "");
        throw new Exception($"[{error!.ErrCode}]{error.ErrMsg}");
    }

    /// <summary>
    /// 获取微信小程序URL SCHEME.
    /// </summary>
    /// <param name=""></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<WechatUrlSchemeResponse> GetUrlSchemeAsync(WechatUrlSchemeRequest input)
    {
        var api = $"https://api.weixin.qq.com/wxa/generatescheme?access_token={input.AccessToken}";
        var request = new
        {
            jump_wxa = new
            {
                path = input.Path,
                query = input.Query,
                env_version = input.EnvVersion
            },
            expire_type = 1,
            expire_interval = input.ExpireInterval
        };

        var response = await HttpRequest.ExecutePostAsync<WechatUrlSchemeResponse>(api, request);
        if(response.ErrCode != 0)
        {
            throw new Exception(response.ErrMsg);
        }

        return response;
    }

    /// <summary>
    /// 发送订阅消息.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<WechatSendSubMsgResponse> SendSubscribeMessageAsync(WechatSendSubMsgRequest input)
    {
        var api = $"https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token={input.AccessToken}";
        var request = new
        {
            page = input.Page,
            data = BuildMessageData(input.Keys, input.Values),
            template_id = input.TemplateId,
            touser = input.OpenId,
            miniprogram_state = input.EnvVersion,
            lang = "zh_CN"
        };

        var response = await HttpRequest.ExecutePostAsync<WechatSendSubMsgResponse>(api, request);

        if(response.ErrCode != 0)
        {
            throw new Exception(response.ErrMsg);
        }

        return response;
    }

    //构建消息data.
    private static Dictionary<string, object> BuildMessageData(string[] keys, string[] values)
    {
        var dictionary = new Dictionary<string, object>();
        for(int i = 0; i < keys.Length; i++)
        {
            dictionary.Add(keys[i], new { value = values[i] });
        }

        return dictionary;
    }
}
