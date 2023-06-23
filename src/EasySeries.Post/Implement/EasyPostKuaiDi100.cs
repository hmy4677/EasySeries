using EasySeries.Post.Models.KuaiDi100;
using EasySeries.Post.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace EasySeries.Post.Implement;

/// <summary>
/// 快递100服务.
/// </summary>
public class EasyPostKuaiDi100 : IEasyPostKuaiDi100
{
    //查询
    private const string QUERY_URL = "https://poll.kuaidi100.com/poll/query.do";

    //电子面单下单
    private const string SEND_URL = "https://api.kuaidi100.com/label/order";

    //电子面单取消
    private const string CANCEL_URL = "https://poll.kuaidi100.com/eorderapi.do";

    private KuaiDi100SecurityOptions _kd100SecurityOptions;

    public EasyPostKuaiDi100(IOptions<KuaiDi100SecurityOptions> kd100SecurityOptions)
    {
        _kd100SecurityOptions = kd100SecurityOptions.Value;
    }

    /// <summary>
    /// 快递100-查询.
    /// </summary>
    /// <param name="query">查询信息.</param>
    /// <param name="kd100SecurityOptions">快递100安全信息.</param>
    /// <returns>查询结果.</returns>
    public async Task<KuaidiQueryResponse> Kd100QueryOrderAsync(Kd100OrderQuery query, KuaiDi100SecurityOptions? kd100SecurityOptions = null)
    {
        if(kd100SecurityOptions != null)
        {
            _kd100SecurityOptions = kd100SecurityOptions;
        }

        var param = new KuaidiQueryParam
        {
            com = query.PostCompany,
            num = query.PostNo,
            phone = query.Mobile,
            resultv2 = query.ResultType
        };
        var body = new KuaidiQueryRequest
        {
            customer = _kd100SecurityOptions.Customer,
            sign = KuaidiQuerySign(param, _kd100SecurityOptions.Key, _kd100SecurityOptions.Customer),
            param = JsonConvert.SerializeObject(param)
        };

        return await RestRequstAsync<KuaidiQueryResponse>(QUERY_URL, body);
    }

    /// <summary>
    /// 快递100-寄件(电子面单).
    /// </summary>
    /// <param name="dianziParam"></param>
    /// <param name="kd100SecurityOptions">快递100安全信息.</param>
    /// <returns>请求结果.</returns>
    public async Task<DianzimiandanResponse> Kd100CreateOrderAsync(DianziParam dianziParam, KuaiDi100SecurityOptions? kd100SecurityOptions = null)
    {
        if(kd100SecurityOptions != null)
        {
            _kd100SecurityOptions = kd100SecurityOptions;
        }

        var stamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var dianzimiandan = new DianzimiandanRequest
        {
            method = "order",
            key = _kd100SecurityOptions.Key,
            sign = KD100SendSign(dianziParam, stamp, _kd100SecurityOptions.Key, _kd100SecurityOptions.Secret),
            t = stamp,
            param = JsonConvert.SerializeObject(dianziParam)
        };

        return await RestRequstAsync<DianzimiandanResponse>(SEND_URL, dianzimiandan);
    }

    /// <summary>
    /// 快递100-取消寄件(电子面单).
    /// </summary>
    /// <param name="orderCancel"></param>
    /// <param name="kd100SecurityOptions"></param>
    /// <returns>取消寄件结果.</returns>
    public async Task<KuaidiCancelResponse> Kd100CancelOrderAsync(Kd100OrderCancel orderCancel, KuaiDi100SecurityOptions? kd100SecurityOptions = null)
    {
        if(kd100SecurityOptions != null)
        {
            _kd100SecurityOptions = kd100SecurityOptions;
        }

        var stamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var data = new
        {
            orderCancel.Kuaidicom,
            orderCancel.Kuaidinum,
            orderCancel.PartnerId,
            orderCancel.OrderId
        };
        var request = new DianzimiandanCancelRequest
        {
            method = "cancel",
            key = _kd100SecurityOptions.Key,
            t = stamp,
            sign = KD100SendSign(data, stamp, _kd100SecurityOptions.Key, _kd100SecurityOptions.Secret),
            param = JsonConvert.SerializeObject(data)
        };

        return await RestRequstAsync<KuaidiCancelResponse>(CANCEL_URL, request);
    }

    /// <summary>
    /// 快递100-签名(查询用).
    /// </summary>
    /// <param name="param">参数json.</param>
    /// <param name="key">快递100key.</param>
    /// <param name="customer">快递100customer.</param>
    /// <returns>签名结果.</returns>
    private static string KuaidiQuerySign(object param, string key, string customer)
    {
        var signStr = JsonConvert.SerializeObject(param) + key + customer;
        return Encrypt(signStr, true);
    }

    /// <summary>
    /// 快递100-签名(寄件用).
    /// </summary>
    /// <param name="param">参数json.</param>
    /// <param name="t">时间戳字符串13位.</param>
    /// <param name="key">快递100key.</param>
    /// <param name="secret">快递100secret.</param>
    /// <returns>签名结果.</returns>
    private static string KD100SendSign(object param, string t, string key, string secret)
    {
        var signStr = JsonConvert.SerializeObject(param) + t + key + secret;
        return Encrypt(signStr, true);
    }

    //网络请求.
    private async Task<T> RestRequstAsync<T>(string url, object body)
        where T : new()
    {
        var client = new RestClient(url);
        var request = new RestRequest
        {
            Method = Method.Post
        };
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonConvert.SerializeObject(body));
        _ = dictionary ?? throw new ArgumentNullException(nameof(body), "请求参数为空");
        foreach(var item in dictionary)
        {
            request.AddParameter(item.Key, item.Value);
        }

        var response = await client.ExecuteAsync(request);
        if(response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<T>(response?.Content ?? "") ?? new T();
        }
        else
        {
            throw new ArgumentException(response.Content);
        }
    }

    //加密.
    private static string Encrypt(string text, bool uppercase = false)
    {
        using var md5Hash = MD5.Create();
        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

        var stringBuilder = new StringBuilder();
        for(var i = 0; i < data.Length; i++)
        {
            stringBuilder.Append(data[i].ToString("x2"));
        }

        var hash = stringBuilder.ToString();
        return !uppercase ? hash : hash.ToUpper();
    }
}