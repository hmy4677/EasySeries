using EasySeries.Post.Models.JDL;
using EasySeries.Post.Options;

namespace EasySeries.Post.Implement;

/// <summary>
/// 京东物流服务.
/// </summary>
public class EasyPostJDL : IEasyPostJDL
{
    private const string JDL_API = "https://api.jdl.com";
    private JDLSecurityOptions _jdlSecurityOptions;

    public EasyPostJDL(IOptions<JDLSecurityOptions> jdlSecurityOptions)
    {
        _jdlSecurityOptions = jdlSecurityOptions.Value;
    }

    /// <summary>
    /// JDL-查询.
    /// </summary>
    /// <param name="postNo">京东快递号.</param>
    /// <param name="jdlSecurityOptions">京东物流安全信息</param>
    /// <returns>查询结果.</returns>
    public async Task<JDLQueryOrderResult> JDLQueryOrderAsyncc(string postNo, JDLSecurityOptions? jdlSecurityOptions = null)
    {
        if(jdlSecurityOptions != null)
        {
            _jdlSecurityOptions = jdlSecurityOptions;
        }

        var request = new List<object>
        {
            new
            {
            waybillCode = postNo,
            orderOrigin = 0,
            customerCode = _jdlSecurityOptions.CustomerCode,
            }
        };

        const string METHOD = "/ecap/v1/orders/trace/query";
        return await RestRequestAsync<JDLQueryOrderResult>(METHOD, request);
    }

    /// <summary>
    /// JDL-预检订单.
    /// </summary>
    /// <param name="senderAddress">发件地址.</param>
    /// <param name="receiverAddress">收件地址.</param>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <returns>预检结果.</returns>
    public async Task<JDLCheckPreOrderResponse> JDLCheckPreOrderAsync(string senderAddress, string receiverAddress, JDLSecurityOptions? jdlSecurityOptions = null)
    {
        if(jdlSecurityOptions != null)
        {
            _jdlSecurityOptions = jdlSecurityOptions;
        }

        var request = new JDLCheckPreOrderRequest
        {
            customerCode = _jdlSecurityOptions.CustomerCode,
            orderOrigin = 1,
            receiverContact = new JDLContact { fullAddress = receiverAddress },
            senderContact = new JDLContact { fullAddress = senderAddress }
        };
        var list = new List<JDLCheckPreOrderRequest> { request };

        const string METHOD = "/ecap/v1/orders/precheck";
        return await RestRequestAsync<JDLCheckPreOrderResponse>(METHOD, list);
    }

    /// <summary>
    /// JDL-下单寄件.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="createRequest">寄件信息.</param>
    /// <returns>请求结果.</returns>
    public async Task<JDLCreateOrderResponse> JDLCreatOrderAsync(JDLCreateOrderRequest createRequest, JDLSecurityOptions? jdlSecurityOptions = null)
    {
        if(jdlSecurityOptions != null)
        {
            _jdlSecurityOptions = jdlSecurityOptions;
        }

        var list = new List<JDLCreateOrderRequest> { createRequest };

        const string METHOD = "/ecap/v1/orders/create";
        return await RestRequestAsync<JDLCreateOrderResponse>(METHOD, list);
    }

    /// <summary>
    /// JDL-取消寄件.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="cancelRequest">取消寄件信息.</param>
    /// <returns>请求结果.</returns>
    public async Task<JDLCancelOrderResponse> JDLCancelOrderAsync(JDLCancelOrderRequest cancelRequest, JDLSecurityOptions? jdlSecurityOptions = null)
    {
        if(jdlSecurityOptions != null)
        {
            _jdlSecurityOptions = jdlSecurityOptions;
        }

        var list = new List<JDLCancelOrderRequest> { cancelRequest };

        const string METHOD = "/ecap/v1/orders/cancel";
        return await RestRequestAsync<JDLCancelOrderResponse>(METHOD, list);
    }

    /// <summary>
    /// JDL-获取运单模板.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <returns>运单模板.</returns>
    public async Task<JDLTemplateListResponse> JDLGetTemplateListAsync(JDLSecurityOptions? jdlSecurityOptions = null)
    {
        if(jdlSecurityOptions != null)
        {
            _jdlSecurityOptions = jdlSecurityOptions;
        }

        var request = new List<JDLTemplateListRequest> { new JDLTemplateListRequest() };

        const string METHOD = "/PullDataService/getTemplateList";
        return await RestRequestAsync<JDLTemplateListResponse>(METHOD, request);
    }

    /// <summary>
    /// JDL-获取云打印数据.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="jdlOrders">订单号数组.</param>
    /// <returns>云打印数据.</returns>
    public async Task<JDLPullDataResponse> JDLGetPrintDataAsync(string[] jdlOrders, JDLSecurityOptions? jdlSecurityOptions = null)
    {
        if(jdlSecurityOptions != null)
        {
            _jdlSecurityOptions = jdlSecurityOptions;
        }

        var orderList = new List<JDLWayBillInfo>();
        foreach(var item in jdlOrders)
        {
            orderList.Add(new JDLWayBillInfo
            {
                jdWayBillCode = item
            });
        }

        var request = new JDLPullDataRequest
        {
            parameters = new Dictionary<string, string> { { "ewCustomerCode", _jdlSecurityOptions.CustomerCode } },
            wayBillInfos = orderList
        };
        var list = new List<JDLPullDataRequest> { request };

        const string METHOD = "/PullDataService/pullData";
        return await RestRequestAsync<JDLPullDataResponse>(METHOD, list);
    }

    //签名
    private string JDLSign(string method, string timestamp, string? requestJson)
    {
        var str = string.Concat(new string[]
        {
                _jdlSecurityOptions.AppSecret,
                "access_token", _jdlSecurityOptions.Token,
                "app_key", _jdlSecurityOptions.Appkey,
                "method", method,
                "param_json", requestJson ?? "",
                "timestamp", timestamp,
                "v", "2.0",
                _jdlSecurityOptions.AppSecret
        });
        var strData = Encoding.UTF8.GetBytes(str);
        return BitConverter.ToString(MD5.Create().ComputeHash(strData))
            .Replace("-", string.Empty, StringComparison.CurrentCulture)
            .ToLower();
    }

    //构建APIURL.
    private string BuildApiUrl(string method, string timestamp, string sign)
    {
        return string.Concat(new string[]
        {
                JDL_API, method,
                "?app_key=", _jdlSecurityOptions.Appkey,
                "&access_token=", _jdlSecurityOptions.Token,
                "&timestamp=", timestamp,
                "&v=", "2.0",
                "&LOP-DN=", "ECAP",
                "&sign=", sign
        });
    }

    //网络请求.
    private async Task<T> RestRequestAsync<T>(string method, object requestBody)
        where T : new()
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        var sign = JDLSign(method, timestamp, requestBodyJson);
        var apiURL = BuildApiUrl(method, timestamp, sign);

        var client = new RestClient(apiURL);
        var request = new RestRequest
        {
            Method = Method.Post
        };
        request.AddJsonBody(requestBodyJson);

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
}