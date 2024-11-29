using Aop.Api.Domain;
using EasySeries.Pay.Static.Models.Wechat;

namespace EasySeries.Pay.Static;

/// <summary>
/// 微信支付.
/// </summary>
public static class WechatPay
{
    //private static string _mchId = string.Empty;
    //private static string _certSerialNo = string.Empty;
    //private static string _privateKeyPath = string.Empty;
    //private static string _platformCertPath = string.Empty;
    //private static string _v3Key = string.Empty;

    /// <summary>
    /// App创建订单.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="payInfo">支付信息.</param>
    /// <returns>App签名信息.</returns>
    public static async Task<WechatAppSignInfo> AppOrderAsync(WechatPayConfig payConfig, WechatPayInfo payInfo)
    {
        var result = await AppPrepayAsync(payConfig, payInfo);
        return AppSign(payConfig.PrivateKeyPath, payConfig.MchId, payConfig.AppId, result.PrepayId);
    }

    /// <summary>
    /// JsApi创建订单.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="payInfo">支付信息.</param>
    /// <returns>JsApi签名信息.</returns>
    public static async Task<WechatJsApiSignInfo> JsApiOrderAsync(WechatPayConfig payConfig, WechatPayInfo payInfo)
    {
        var result = await JsApiPrepayAsync(payConfig, payInfo);
        return JsApiSign(payConfig.PrivateKeyPath, payConfig.AppId, result.PrepayId);
    }

    /// <summary>
    /// 退款.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="refundInfo">支付配置.</param>
    /// <returns>退款响应.</returns>
    public static async Task<WechatRefundResponse> RefundAsync(WechatPayConfig payConfig, WechatRefundInfo refundInfo)
    {
        const string API_URL = "https://api.mch.weixin.qq.com/v3/refund/domestic/refunds";
        var requestBody = new RefundRequest
        {
            OutRefundNo = refundInfo.RefundNo,
            OutTradeNo = refundInfo.OutTradeNo,
            Reason = refundInfo.Reason,
            NotifyUrl = payConfig.RefundNotifyUrl,
            Amount = new WechatRefundAmount
            {
                Total = refundInfo.TotalAmount,
                Refund = refundInfo.RefundAmount,
            }
        };

        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        return await ExecuteRequestAsync<WechatRefundResponse>(payConfig, API_URL, requestBodyJson);
    }

    /// <summary>
    /// 关闭订单.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="outTradeNo">订单号(商户单号).</param>
    /// <returns></returns>
    public static async Task<bool> CloseAsync(WechatPayConfig payConfig, string outTradeNo)
    {
        var apiUrl = $"https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{outTradeNo}/close";
        var requestBody = new { mchid = payConfig.MchId };
        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        _ = await ExecuteRequestAsync<dynamic>(payConfig, apiUrl, requestBodyJson);

        return true;
    }

    /// <summary>
    /// 查询支付.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="outTradeNo">订单号(商户单号).</param>
    /// <returns>微信查询支付响应.</returns>
    public static async Task<WechatQueryPayResponse> QueryPayAsync(WechatPayConfig payConfig, string outTradeNo)
    {
        var apiUrl = $"https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{outTradeNo}?mchid={payConfig.MchId}";
        return await ExecuteRequestAsync<WechatQueryPayResponse>(payConfig, apiUrl);
    }

    /// <summary>
    /// 查询退款.
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="refundNo">退款单号.</param>
    /// <returns>微信查询退款响应.</returns>
    public static async Task<WechatQueryRefundResponse> QueryRefundAsync(WechatPayConfig payConfig, string refundNo)
    {
        var apiUrl = $"https://api.mch.weixin.qq.com/v3/refund/domestic/refunds/{refundNo}";
        return await ExecuteRequestAsync<WechatQueryRefundResponse>(payConfig, apiUrl);
    }

    /// <summary>
    /// 支付回调通知处理.
    /// 回应:return Ok()/BadRequest("'code':'FAIL','message':'xxx'");
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="notify">通知内容.</param>
    /// <returns>支付查询结果.</returns>
    public static WechatQueryPayResponse PayNotifyHandel(WechatPayConfig payConfig, WechatNotify notify)
    {
        return NotifyHandle<WechatQueryPayResponse>(payConfig, notify);
    }

    /// <summary>
    /// 退款回调通知处理.
    /// 回应:return Ok()/BadRequest("'code':'FAIL','message':'xxx'");
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <param name="notify">通知内容.</param>
    /// <returns>退款查询结果.</returns>
    public static WechatQueryRefundResponse RefundNotifyHandle(WechatPayConfig payConfig, WechatNotify notify)
    {
        return NotifyHandle<WechatQueryRefundResponse>(payConfig, notify);
    }

    /// <summary>
    /// 获取平台证书公钥(验签用).
    /// </summary>
    /// <param name="payConfig">支付配置.</param>
    /// <returns></returns>
    public static async Task<List<WechatPayPlatformCert>> GetPlatformCertAsync(WechatPayConfig payConfig)
    {
        const string API_URL = "https://api.mch.weixin.qq.com/v3/certificates";
        var result = await ExecuteRequestAsync<PlatformCertResponse>(payConfig, API_URL);
        return result.Data.ConvertAll(p => new WechatPayPlatformCert
        {
            SerialNo = p.SerialNo,
            EffectiveTime = p.EffectiveTime,
            ExpireTime = p.ExpireTime,
            DecryptText = AesGcmDecrypt(payConfig.V3Key, p.EncryptCertificate.AssociatedData, p.EncryptCertificate.Nonce, p.EncryptCertificate.Ciphertext)
        });
    }

    //App签名.
    private static WechatAppSignInfo AppSign(string privateKeyPath, string mchId, string appId, string prepayId)
    {
        var timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        var nonceStr = Path.GetRandomFileName();
        var signStr = $"{appId}\n{timeStamp}\n{nonceStr}\n{prepayId}\n";
        var paySign = SHA256WithRSASign(privateKeyPath, signStr);
        return new WechatAppSignInfo
        {
            Appid = appId,
            Noncestr = nonceStr,
            Package = "Sign=WXPay",
            Sign = paySign,
            Timestamp = timeStamp,
            Prepayid = prepayId,
            Partnerid = mchId
        };
    }

    //App创建预付.
    private static async Task<PrepayResponse> AppPrepayAsync(WechatPayConfig payConfig, WechatPayInfo payInfo)
    {
        const string API_URL = "https://api.mch.weixin.qq.com/v3/pay/transactions/app";
        var requestBody = new PrepayRequest
        {
            AppId = payConfig.AppId,
            Mchid = payConfig.MchId,
            NotifyUrl = payConfig.PayNotifyUrl,

            Description = payInfo.Description,
            OutTradeNo = payInfo.OutTradeNo,
            Amount = new OrderAmount { Total = payInfo.Amount }
        };

        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        return await ExecuteRequestAsync<PrepayResponse>(payConfig, API_URL, requestBodyJson);
    }

    //JsApi签名.
    private static WechatJsApiSignInfo JsApiSign(string privateKeyPath, string appId, string prepayId)
    {
        var timeStamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var nonceStr = Path.GetRandomFileName();
        var package = $"prepay_id={prepayId}";
        var signStr = $"{appId}\n{timeStamp}\n{nonceStr}\n{package}\n";
        var paySign = SHA256WithRSASign(privateKeyPath, signStr);

        return new WechatJsApiSignInfo
        {
            AppId = appId,
            NonceStr = nonceStr,
            Package = package,
            PaySign = paySign,
            TimeStamp = timeStamp,
        };
    }

    //JsApi创建预付.
    private static async Task<PrepayResponse> JsApiPrepayAsync(WechatPayConfig payConfig, WechatPayInfo payInfo)
    {
        if(string.IsNullOrEmpty(payInfo.OpenId))
        {
            throw new ArgumentNullException(nameof(payInfo.OpenId), "参数为空");
        }

        const string API_URL = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";
        var requestBody = new JsApiPrepayRequest
        {
            AppId = payConfig.AppId,
            Mchid = payConfig.MchId,
            NotifyUrl = payConfig.PayNotifyUrl,

            Description = payInfo.Description,
            OutTradeNo = payInfo.OutTradeNo,
            Amount = new OrderAmount { Total = payInfo.Amount },
            Payer = new WechatPayer { OpenId = payInfo.OpenId }
        };

        var requestBodyJson = JsonConvert.SerializeObject(requestBody);
        return await ExecuteRequestAsync<PrepayResponse>(payConfig, API_URL, requestBodyJson);
    }

    //通知验签解密.
    private static T NotifyHandle<T>(WechatPayConfig payConfig, WechatNotify notify)
    {
        if(notify.IsVerifySign)
        {
            var verify = VerifySign(payConfig.PlatformCertPath, notify.Signature, notify.Stamp, notify.Nonce, notify.Body);
            if(!verify)
            {
                throw new ArgumentException("回调通知处理出错:验签失败");
            }
        }

        var model = JsonConvert.DeserializeObject<NotifyModel>(notify.Body)
                    ?? throw new Exception("回调通知处理出错:Body转化失败");
        var decrypt = AesGcmDecrypt(payConfig.V3Key, model.Resource.AssociatedData, model.Resource.Nonce, model.Resource.Ciphertext);
        return JsonConvert.DeserializeObject<T>(decrypt);
    }

    //执行请求.
    private static async Task<T> ExecuteRequestAsync<T>(WechatPayConfig payConfig, string url, string? requestBodyJson = null)
    {
        var client = new RestClient(url);
        var request = new RestRequest();
        var method = "GET";
        if(requestBodyJson != null)
        {
            method = "POST";
            request.AddJsonBody(requestBodyJson);
            request.Method = Method.Post;
        }

        var urlTrim = url.Replace("https://api.mch.weixin.qq.com", string.Empty, StringComparison.CurrentCulture).TrimEnd('/');
        var authorization = BuildAuthorization(payConfig.MchId, payConfig.CertSerialNo, payConfig.PrivateKeyPath, method, urlTrim, requestBodyJson);
        request.AddHeader("Authorization", authorization);

        var response = await client.ExecuteAsync(request);
        if(response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<T>(response?.Content ?? "");
        }
        else
        {
            throw new ArgumentException(response.Content);
        }
    }

    //构建请求Authorization.
    private static string BuildAuthorization(string mchId, string certSerialNo, string privateKeyPath, string method, string url, string? requestBodyJson = null)
    {
        var timestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var nonce = Path.GetRandomFileName();
        var text = $"{method}\n{url}\n{timestamp}\n{nonce}\n{requestBodyJson ?? string.Empty}\n";
        var signature = SHA256WithRSASign(privateKeyPath, text);

        return $" WECHATPAY2-SHA256-RSA2048 mchid=\"{mchId}\",nonce_str=\"{nonce}\",signature=\"{signature}\",timestamp=\"{timestamp}\",serial_no=\"{certSerialNo}\"";
    }

    //SHA256 With RSA 签名.
    private static string SHA256WithRSASign(string privateKeyPath, string text)
    {
        if(!File.Exists(privateKeyPath))
        {
            throw new FileNotFoundException("微信私钥文件不存在");
        }

        var keyText = File.ReadAllText(privateKeyPath);
        keyText = keyText.Replace("-----BEGIN PRIVATE KEY-----", string.Empty)
                         .Replace("-----END PRIVATE KEY-----", string.Empty)
                         .Trim();
        var keyBuffer = Convert.FromBase64String(keyText);

        using var rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(keyBuffer, out _);
        var signBuffer = rsa.SignData(Encoding.UTF8.GetBytes(text), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        return Convert.ToBase64String(signBuffer);
    }

    //验签.
    private static bool VerifySign(string platformCertPath, string signature, string stamp, string nonce, string? body)
    {
        if(!File.Exists(platformCertPath))
        {
            throw new FileNotFoundException("微信支付平台公钥证书不存在");
        }

        //var cert = new X509Certificate(_platformCertPath);
        //var publicKey = cert.GetPublicKey();

        //using var rsa = RSA.Create();
        //rsa.ImportRSAPublicKey(publicKey, out _);

        using var cert = new X509Certificate2(platformCertPath);
        using var rsa = cert.GetRSAPublicKey();

        var textBuffer = Encoding.UTF8.GetBytes($"{stamp}\n{nonce}\n{body}\n");
        var signatureBuffer = Convert.FromBase64String(signature);
        return rsa!.VerifyData(textBuffer, signatureBuffer, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    //解密.
    private static string AesGcmDecrypt(string v3Key, string associatedData, string nonce, string ciphertext)
    {
        if(string.IsNullOrEmpty(associatedData) || string.IsNullOrEmpty(nonce) || string.IsNullOrEmpty(ciphertext))
        {
            throw new ArgumentNullException("回调通知处理出错:解密参数为空");
        }

        var gcmBlockCipher = new GcmBlockCipher(new AesEngine());
        var aeadParameters = new AeadParameters(
            new KeyParameter(Encoding.UTF8.GetBytes(v3Key)),
            128,
            Encoding.UTF8.GetBytes(nonce),
            Encoding.UTF8.GetBytes(associatedData));
        gcmBlockCipher.Init(false, aeadParameters);

        var data = Convert.FromBase64String(ciphertext);
        var plaintext = new byte[gcmBlockCipher.GetOutputSize(data.Length)];
        var length = gcmBlockCipher.ProcessBytes(data, 0, data.Length, plaintext, 0);
        gcmBlockCipher.DoFinal(plaintext, length);
        return Encoding.UTF8.GetString(plaintext);
    }
}