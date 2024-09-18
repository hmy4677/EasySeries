using EasySeries.Pay.Static.Models.CititcBank;
using System.Xml.Serialization;

namespace EasySeries.Pay.Static;

/// <summary>
/// 中信银行支付.
/// </summary>
public static class CiticBankPay
{
    private const string API_URL = "https://totalpay.citicbank.com/totalpay/unifyTrade.do";

    /// <summary>
    /// 创建订单.
    /// </summary>
    /// <param name="payConfig">中信银行支付配置.</param>
    /// <param name="payInfo">中信银行订单信息.</param>
    /// <returns></returns>
    public static async Task<CiticBankOrderResponse> CreateOrderAsync(CiticBankPayConfig payConfig, CiticBankPayInfo payInfo)
    {
        var request = new OrderRequst
        {
            Body = payInfo.Body,
            BuyerId = payInfo.BuyerId,
            BuyerLogonId = payInfo.BuyerLogonId,
            MchCreateIp = payInfo.MchCreateIp,
            Openid = payInfo.Openid,
            OutTradeNo = payInfo.OutTradeNo,
            SellerId = payInfo.SellerId,
            SubAppid = payInfo.SubAppid,
            Subject = payInfo.Subject,
            SubOpenid = payInfo.SubOpenid,
            TotalFee = payInfo.TotalFee,
            TradeType = payInfo.TradeType,
            MchId = payConfig.MchId,
            NotifyUrl = payConfig.NotifyUrl
        };

        return await ExecuteRequestAsync<OrderRequst, CiticBankOrderResponse>(payConfig, request);
    }

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="payConfig">中信银行支付配置.</param>
    /// <param name="refundInfo">中信银行退款信息.</param>
    /// <returns></returns>
    public static async Task<CiticBankRefundResponse> RefundAsync(CiticBankPayConfig payConfig, CiticBankRefundInfo refundInfo)
    {

        var request = new RefundRequest
        {
            MchId = payConfig.MchId,
            OutRefundNo = refundInfo.OutRefundNo,
            OutTradeNo = refundInfo.OutTradeNo,
            RefundFee = refundInfo.RefundFee,
            TotalFee = refundInfo.TotalFee
        };

        return await ExecuteRequestAsync<RefundRequest, CiticBankRefundResponse>(payConfig, request);
    }

    /// <summary>
    /// 关闭订单.
    /// </summary>
    /// <param name="payConfig">中信银行支付配置.</param>
    /// <param name="outTradeNo">商户订单号.</param>
    /// <returns></returns>
    public static async Task<CiticBankCloseResponse> CloseAsync(CiticBankPayConfig payConfig, string outTradeNo)
    {
        var request = new CloseRequest
        {
            MchId = payConfig.MchId,
            OutTradeNo = outTradeNo
        };

        return await ExecuteRequestAsync<CloseRequest, CiticBankCloseResponse>(payConfig, request);
    }

    /// <summary>
    /// 查询支付.
    /// </summary>
    /// <param name="payConfig">中信银行支付配置.</param>
    /// <param name="outTradeNo">商户订单号.</param>
    /// <returns></returns>
    public static async Task<CiticBankQueryPayResponse> QueryPay(CiticBankPayConfig payConfig, string outTradeNo)
    {
        var request = new QueryPayRequest
        {
            MchId = payConfig.MchId,
            OutTradeNo = outTradeNo
        };

        return await ExecuteRequestAsync<QueryPayRequest, CiticBankQueryPayResponse>(payConfig, request);
    }

    /// <summary>
    /// 查询退款.
    /// </summary>
    /// <param name="payConfig">中信银行支付配置.</param>
    /// <param name="outTradeNo">商户订单号</param>
    /// <param name="OutRefundNo">退款单号.</param>
    /// <returns></returns>
    public static async Task<CiticBankQueryRefundResponse> QueryRefundAsync(CiticBankPayConfig payConfig, string outTradeNo, string OutRefundNo)
    {
        var request = new CiticBankQueryRefundRequest
        {
            MchId = payConfig.MchId,
            OutTradeNo = outTradeNo,
            OutRefundNo = OutRefundNo
        };

        return await ExecuteRequestAsync<CiticBankQueryRefundRequest, CiticBankQueryRefundResponse>(payConfig, request);
    }

    /// <summary>
    /// 回调通知处理.回应:await Response.WriteAsync("SUCCESS/FAIL");
    /// </summary>
    /// <param name="payConfig">中信银行支付配置.</param>
    /// <param name="body">请求body(var smr = new StreamReader(Request.Body);var content = await smr.ReadToEndAsync();)</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static CiticBankNotify NotifyHandle(CiticBankPayConfig payConfig, string body)
    {
        if(string.IsNullOrEmpty(body))
        {
            throw new Exception("FAIL");
        }

        return ReponseContentHandle<CiticBankNotify>(body, payConfig.IsVerifySign, payConfig.CertFilePath);
    }

    //执行请求.
    private static async Task<T2> ExecuteRequestAsync<T1, T2>(CiticBankPayConfig payConfig, T1 requestObj)
        where T1 : SignBase where T2 : ResponseBase
    {
        var sendSignStr = SortValueString(requestObj);
        requestObj.SetSign(RSASign, sendSignStr, payConfig.KeyFilePath);

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

        return ReponseContentHandle<T2>(response.Content, payConfig.IsVerifySign, payConfig.CertFilePath);
    }

    //响应处理.
    private static T ReponseContentHandle<T>(string xmlContent, bool isVerifySign, string? certFilePath = null)
        where T : ResponseBase
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

        if(isVerifySign)
        {
            if(string.IsNullOrEmpty(certFilePath))
            {
                throw new ArgumentNullException("缺少公钥证书文件路径");
            }

            if(!VerifySign(certFilePath, SortValueString(result, true), result.Sign))
            {
                throw new Exception("验签失败");
            }
        }

        return result;
    }

    //RSA签名.
    private static string RSASign(string signStr, string keyFilePath)
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
    private static bool VerifySign(string certFilePaht, string text, string signBase64Str)
    {
        if(!File.Exists(certFilePaht))
        {
            throw new FileNotFoundException("中信银行公钥文件未找到");
        }

        var cert = new X509Certificate2(certFilePaht);
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
