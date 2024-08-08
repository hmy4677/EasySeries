using EasySeries.Post.Models.SF;
using EasySeries.Post.Options;

namespace EasySeries.Post.Implement;

/// <summary>
/// 顺丰.
/// </summary>
public class EasyPostSF : IEasyPostSF
{
    private const string API_URL_YJT = "https://mrds.sf-laas.com/api/";

    private SFYJTSecurityOptions _securityOptions;

    /// <summary>
    /// 构造.
    /// </summary>
    /// <param name="securityOptions"></param>
    public EasyPostSF(IOptions<SFYJTSecurityOptions> securityOptions)
    {
        _securityOptions = securityOptions.Value;
    }

    /// <summary>
    /// 丰医寄通预检订单.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="securityOptions"></param>
    /// <returns>是否成功.</returns>
    public async Task<SFYJTPreCheckResponse> SFYJTPreCheckAsync(SFYJTPreCheckRequest request, SFYJTSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        return await RequestAsync<SFYJTPreCheckResponse>(request, "open/api/v2/preOrder");
    }

    /// <summary>
    /// 顺丰医寄通创建订单.
    /// </summary>
    /// <param name="post"></param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>创建订单响应.</returns>
    public async Task<SFYJTOrderResponse> SFYJTCreateOrderAsync(SFYJTPostInfo post, SFYJTSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var body = new SFYJTCreateOrderRequest
        {
            MerchantOrderNo = post.MerchantOrderNo,
            ExpressType = post.ExpressType,
            ContactInfo = post.ContactInfo,
            PatientInfo = post.PatientInfo,
            PayMethod = post.PayMethod,
            Remark = post.Remark,
            MonthlyCard = post.PayMethod == 2 ? null : _securityOptions.MonthlyCard
        };

        body.SetCollectionMoney(post.CollectionMoney, _securityOptions.MonthlyCard);

        return await RequestAsync<SFYJTOrderResponse>(body, "open/api/v2/createOrder");
    }

    /// <summary>
    /// 顺丰医寄通获取面单打印数据(76*130,不打印logo,默认脱敏项).
    /// </summary>
    /// <param name="merchantOrderNo">商户单号.</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>面单打印数据响应.</returns>
    public async Task<SFYJTPrintDataReponse> SFYJTGetPrintDataAsync(string merchantOrderNo, SFYJTSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        var body = new
        {
            merchantOrderNo,
            isPrintLogo = false,
            maskFlag = 0,
            templateCode = "fm_76130_standard_inc-mrds-core"
        };

        return await RequestAsync<SFYJTPrintDataReponse>(body, "open/api/v2/printWaybills");
    }

    /// <summary>
    /// 顺丰医寄通取消订单.
    /// </summary>
    /// <param name="merchantOrderNo">商户单号.</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>取消响应.</returns>
    public async Task<SFYJTCancelResponse> SFYJTCancelAsync(string merchantOrderNo, SFYJTSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        return await RequestAsync<SFYJTCancelResponse>(new { merchantOrderNo }, "open/api/v2/cancelOrder");
    }

    /// <summary>
    /// 顺丰医寄通查询订单.
    /// </summary>
    /// <param name="trackingType">查询号类型1：根据顺丰运单号查询;2：根据客户订单号查</param>
    /// <param name="trackingNumber">查询号:trackingType=1,则此值为顺丰运单号,如果trackingType=2,则此值为客户订单号</param>
    /// <param name="phone">收件人电话后四位（trackingType=1时如果查不到路由信息，请尝试传该字段）</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>顺丰医寄通查询订单响应</returns>
    public async Task<SFYJTQueryResponse> SFYJTQueryAsync(string trackingType, string trackingNumber, string? phone = null, SFYJTSecurityOptions? securityOptions = null)
    {
        if(securityOptions != null)
        {
            _securityOptions = securityOptions;
        }

        return await RequestAsync<SFYJTQueryResponse>(new { trackingType, trackingNumber, phone }, "open/api/v2/queryRoutes");
    }

    //请求.
    private async Task<T> RequestAsync<T>(object body, string path)
        where T : SFYJTResponseBase
    {
        var api = API_URL_YJT + path;
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        var sign = Sign(body, _securityOptions.SecretKey, timestamp);

        var client = new RestClient(api);
        var request = new RestRequest();
        request.AddHeader("hospitalCode", _securityOptions.HospitalCode);
        request.AddHeader("timestamp", timestamp);
        request.AddHeader("sign", sign);
        request.AddBody(body, ContentType.Json);

        var response = await client.PostAsync<T>(request);
        if(response!.Success)
        {
            return response;
        }

        throw new Exception($"[{response.Code}]{response.Message}");
    }

    //签名.
    private static string Sign(object body, string secretKey, string timestamp)
    {
        var json = JsonConvert.SerializeObject(body);
        using var sha = SHA512.Create();
        var buffer = sha.ComputeHash(Encoding.UTF8.GetBytes(json + secretKey + timestamp));
        var builder = new StringBuilder();
        foreach(var item in buffer)
        {
            builder.Append(item.ToString("x2"));
        }

        return builder.ToString();
    }
}
