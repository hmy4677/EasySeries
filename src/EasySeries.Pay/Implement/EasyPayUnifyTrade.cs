using EasySeries.Pay.Models.UnifyTrade;
using EasySeries.Pay.Options;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace EasySeries.Pay.Implement;

/// <summary>
/// 中信全付通.
/// </summary>
public class EasyPayUnifyTrade : IEasyPayUnifyTrade
{
    private const string API_URL = "https://totalpay.citicbank.com/totalpay/unifyTrade.do";
    private UnifyTradeSecurityOptions _securityOptions;

    /// <summary>
    /// 构造.
    /// </summary>
    /// <param name="options"></param>
    public EasyPayUnifyTrade(IOptions<UnifyTradeSecurityOptions> options)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        _securityOptions = options.Value;
    }

    /// <summary>
    /// 中信全付通查询.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>查询响应.</returns>
    public async Task<UnifyTradeQueryResponse> UnifyTradeQueryAsync(string outTradeNo, UnifyTradeSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new UnifyTradeQueryRequest
        {
            MchId = _securityOptions.MchId,
            OutTradeNo = outTradeNo
        };

        return await HttpRequestAsync<UnifyTradeQueryRequest, UnifyTradeQueryResponse>(request);
    }

    /// <summary>
    /// 中信全付通支付下单.
    /// </summary>
    /// <param name="native"></param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>支付下单响应.</returns>
    public async Task<dynamic> UnifyTradeNativeAsync(UnifyTradeNative native, UnifyTradeSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new UnifyTradeNativeRequest
        {
            Body = native.Body,
            BuyerId = native.BuyerId,
            BuyerLogonId = native.BuyerLogonId,
            MchCreateIp = native.MchCreateIp,
            Openid = native.Openid,
            OutTradeNo = native.OutTradeNo,
            SellerId = native.SellerId,
            SubAppid = native.SubAppid,
            Subject = native.Subject,
            SubOpenid = native.SubOpenid,
            TotalFee = native.TotalFee,
            TradeType = native.TradeType,
            MchId = _securityOptions.MchId,
            NotifyUrl = _securityOptions.NotifyUrl
        };

        return await HttpRequestAsync<UnifyTradeNativeRequest, UnifyTradeNativeResponse>(request);
    }

    /// <summary>
    /// 中信全付通退款.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="outRefundNo">商户退款单号.</param>
    /// <param name="totalFee">订单总额(单位:分).</param>
    /// <param name="refundFee">退款金额(单位:分).</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>退款响应.</returns>
    public async Task<UnifyTradeRefundResponse> UnifyTradeRefundAsync(string outTradeNo, string outRefundNo, int totalFee, int refundFee, UnifyTradeSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new UnifyTradeRefundRequest
        {
            MchId = _securityOptions.MchId,
            OutRefundNo = outRefundNo,
            OutTradeNo = outTradeNo,
            RefundFee = refundFee,
            TotalFee = totalFee
        };

        return await HttpRequestAsync<UnifyTradeRefundRequest, UnifyTradeRefundResponse>(request);
    }

    /// <summary>
    /// 中信全付通查询退款.
    /// </summary>
    /// <param name="outTradeNo">商户单号.</param>
    /// <param name="outRefundNo">退款单号.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>查询退款响应.</returns>
    public async Task<UnifyTradeQueryRefundResponse> UnifyTradeQueryRefundAsync(string outTradeNo, string outRefundNo, UnifyTradeSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var request = new UnifyTradeQueryRefundRequest
        {
            MchId = _securityOptions.MchId,
            OutRefundNo = outRefundNo,
            OutTradeNo = outTradeNo,
        };

        return await HttpRequestAsync<UnifyTradeQueryRefundRequest, UnifyTradeQueryRefundResponse>(request);
    }

    /// <summary>
    /// 中信全付通回调处理.回应:await Response.WriteAsync("SUCCESS/FAIL");
    /// </summary>
    /// <param name="request">网络请求.</param>
    /// <param name="securityOptions">中信全付安全配置.</param>
    /// <returns>回调通知处理.</returns>
    /// <exception cref="Exception"></exception>
    public async Task<dynamic> UnifyTradeCallbackHandleAsync(HttpRequest request, UnifyTradeSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var smr = new StreamReader(request.Body);
        var content = await smr.ReadToEndAsync();
        if(string.IsNullOrEmpty(content))
        {
            throw new Exception("FAIL");
        }

        return ReponseContentHandle<UnifyTradeNotifyResponse>(content);
    }

    //Http请求.
    private async Task<T2> HttpRequestAsync<T1, T2>(T1 requestObj)
        where T1 : UnifyTradeSign where T2 : UnifyTradeResponseBase
    {
        var sendSignStr = SortValueString(requestObj);
        requestObj.SetSign(RSASign, sendSignStr, _securityOptions.KeyFilePath);

        var requestJson = JsonConvert.SerializeObject(new { request = requestObj });
        var requestXml = JsonConvert.DeserializeXmlNode(requestJson) ?? throw new JsonException("请求对象转化XML失败");

        var restClient = new RestClient(API_URL);
        var restRequest = new RestRequest();
        restRequest.AddBody(requestXml.OuterXml, ContentType.Xml);
        var response = await restClient.ExecutePostAsync(restRequest);
        if(!response.IsSuccessful)
        {
            throw new HttpRequestException(response.ErrorMessage);
        }

        if(string.IsNullOrEmpty(response.Content) || response.Content == "\n")
        {
            throw new HttpRequestException("目标服务器响应为空");
        }

        return ReponseContentHandle<T2>(response.Content);
    }

    //响应处理.
    private T ReponseContentHandle<T>(string xmlContent)
        where T : UnifyTradeResponseBase
    {
        using var stringReader = new StringReader(xmlContent);
        var serializer = new XmlSerializer(typeof(T));
        var result = (T?)serializer.Deserialize(stringReader) ?? throw new Exception("响应内容解析失败");
        if(result.ReturnCode == "FAIL")
        {
            throw new Exception(result.ReturnMsg);
        }

        if(result.ResultCode == "FAIL")
        {
            throw new Exception(result.ErrMsg);
        }

        if(_securityOptions.IsVerifySign)
        {
            var recSignStr = SortValueString(result, true);
            if(!VerifySign(recSignStr, result.Sign))
            {
                throw new Exception("验签失败");
            }
        }

        return result;
    }

    //RSA签名.
    private string RSASign(string signStr, string keyFilePath)
    {
        if(!File.Exists(keyFilePath))
        {
            throw new FileNotFoundException("私钥文件未找到", keyFilePath);
        }

        var keyText = File.ReadAllText(keyFilePath);
        var keyBuffer = Convert.FromBase64String(keyText);
        using RSA rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(keyBuffer, out _);
        var signBuffer = Encoding.GetEncoding("GBK").GetBytes(signStr);
        var signedBytes = rsa.SignData(signBuffer, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        return Convert.ToBase64String(signedBytes);
    }

    //验签.
    private bool VerifySign(string text, string signBase64Str)
    {
        if(!File.Exists(_securityOptions.CerFilePath))
        {
            throw new FileNotFoundException("公钥文件未找到", _securityOptions.CerFilePath);
        }

        var cert = new X509Certificate2(_securityOptions.CerFilePath);
        var rsa = cert.GetRSAPublicKey();
        var data = Encoding.GetEncoding("GBK").GetBytes(text);
        var signature = Convert.FromBase64String(signBase64Str);

        return rsa!.VerifyData(data, signature, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
    }

    //对象排序.
    private static string SortValueString(object obj, bool isRemoveSignMode = false)
    {
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(obj));

        dictionary.Remove("sign");
        if(isRemoveSignMode)
        {
            dictionary.Remove("sign_mode");
        }

        var builder = new StringBuilder();
        foreach(var item in dictionary.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value))
        {
            if(item.Value != null && item.Value.ToString() != "")
            {
                Type targetInternalType = item.Value.GetType();
                if(targetInternalType.IsGenericType && targetInternalType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    targetInternalType = targetInternalType.GetGenericArguments()[0];
                }
                if(targetInternalType == typeof(string) || targetInternalType == typeof(Guid) || targetInternalType.IsPrimitive ||
                    targetInternalType.IsValueType)
                {
                    builder.Append(item.Value);
                }
            }
        }

        return builder.ToString();
    }
}