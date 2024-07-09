## EasySeries.Post

Easy邮寄，Easy系列的第二个应用，现支持快递100,京东物流。

### 使用说明:

#### 预先配置模式
```json
//appsettings.json
  "JDLSecurityOptions": {
    "Appkey":"xxx",
    "AppSecret":"xxx",
    "Token":"xxx",
    "CustomerCode":"xxx"
  },
  "KuaiDi100SecurityOptions": {
    "Customer":"xxx",
    "Key":"xxx",
    "Secret":"xxx"
  },
  "SFYJTSecurityOptions": {
    "SecretKey": "xxx", //密钥.
    "HospitalCode": "xx", //医院编号.
    "MonthlyCard": "xxx", //月结账号.
    "BankCardNo": "xxx" //银行卡号,代收货款用.
  }
```

```c#
//注册
builder.Services.AddEasyPostService();

//使用
public PostController(IEasyPostKuaiDi100 easyPostKuaiDi100, IEasyPostJDL easyPostJDL, IEasyPostSF easyPostSF)
{
    _easyPostKuaiDi100 = easyPostKuaiDi100;
    _easyPostJDL = easyPostJDL;
    _easyPostSF = easyPostSF;
}



```

#### 即时配置模式

```c#
//注册
builder.Services.AddEasyPostService();

//使用
public PostController(IEasyPostKuaiDi100 easyPostKuaiDi100, IEasyPostJDL easyPostJDL, IEasyPostSF easyPostSF)
{
    _easyPostKuaiDi100 = easyPostKuaiDi100;
    _easyPostJDL = easyPostJDL;
    _easyPostSF = easyPostSF;
}
    
[HttpGet("JDL")]
public async Task<dynamic> QueryAsync(string outTradeNo)
{
    var options = new JDLSecurityOptions
    {
        Appkey="xxx",
        AppSecret="xxx",
        Token="xxx",
        CustomerCode="xxx"
    };

    return await _easyPostJDL.JDLCheckPreOrderAsync(senderAddress,receiverAddress, options);
}
```
#### API列表
```c#
/// <summary>
/// 京东物流interface.
/// </summary>
public interface IEasyPostJDL
{
    /// <summary>
    /// JDL-取消寄件.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="cancelRequest">取消寄件信息.</param>
    /// <returns>请求结果.</returns>
    Task<JDLCancelOrderResponse> JDLCancelOrderAsync(JDLCancelOrderRequest cancelRequest, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-预检订单.
    /// </summary>
    /// <param name="senderAddress">发件地址.</param>
    /// <param name="receiverAddress">收件地址.</param>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <returns>预检结果.</returns>
    Task<JDLCheckPreOrderResponse> JDLCheckPreOrderAsync(string senderAddress, string receiverAddress, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-下单寄件.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="createRequest">寄件信息.</param>
    /// <returns>请求结果.</returns>
    Task<JDLCreateOrderResponse> JDLCreatOrderAsync(JDLCreateOrderRequest createRequest, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-获取云打印数据.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <param name="jdlOrders">订单号数组.</param>
    /// <returns>云打印数据.</returns>
    Task<JDLPullDataResponse> JDLGetPrintDataAsync(string[] jdlOrders, JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-获取运单模板.
    /// </summary>
    /// <param name="jdlSecurityOptions">京东物流安全信息.</param>
    /// <returns>运单模板.</returns>
    Task<JDLTemplateListResponse> JDLGetTemplateListAsync(JDLSecurityOptions? jdlSecurityOptions = null);

    /// <summary>
    /// JDL-查询.
    /// </summary>
    /// <param name="postNo">京东快递号.</param>
    /// <param name="jdlSecurityOptions">京东物流安全信息</param>
    /// <returns>查询结果.</returns>
    Task<JDLQueryOrderResult> JDLQueryOrderAsyncc(string postNo, JDLSecurityOptions? jdlSecurityOptions = null);
}

/// <summary>
/// 快递100interface.
/// </summary>
public interface IEasyPostKuaiDi100
{
    /// <summary>
    /// 快递100-取消寄件(电子面单).
    /// </summary>
    /// <param name="orderCancel"></param>
    /// <param name="kd100SecurityOptions"></param>
    /// <returns>取消寄件结果.</returns>
    Task<KuaidiCancelResponse> Kd100CancelOrderAsync(Kd100OrderCancel orderCancel, KuaiDi100SecurityOptions? kd100SecurityOptions = null);

    /// <summary>
    /// 快递100-寄件(电子面单).
    /// </summary>
    /// <param name="dianziParam"></param>
    /// <param name="kd100SecurityOptions">快递100安全信息.</param>
    /// <returns>请求结果.</returns>
    Task<DianzimiandanResponse> Kd100CreateOrderAsync(DianziParam dianziParam, KuaiDi100SecurityOptions? kd100SecurityOptions = null);

    /// <summary>
    /// 快递100-查询.
    /// </summary>
    /// <param name="query">查询信息.</param>
    /// <param name="kd100SecurityOptions">快递100安全信息.</param>
    /// <returns>查询结果.</returns>
    Task<KuaidiQueryResponse> Kd100QueryOrderAsync(Kd100OrderQuery query, KuaiDi100SecurityOptions? kd100SecurityOptions = null);
}


/// <summary>
/// 顺丰Interface.
/// </summary>
public interface IEasyPostSF
{
    /// <summary>
    /// 丰医寄通预检订单.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="securityOptions"></param>
    /// <returns>是否成功.</returns>
    Task<SFYJTPreCheckResponse> PreOrderAsync(SFYJTPreCheckRequest request, SFYJTSecurityOptions? securityOptions = null);

    /// <summary>
    /// 顺丰医寄通取消订单.
    /// </summary>
    /// <param name="merchantOrderNo">商户单号.</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>取消响应.</returns>
    Task<SFYJTCancelResponse> SFYJTCancelAsync(string merchantOrderNo, SFYJTSecurityOptions? securityOptions = null);

    /// <summary>
    /// 顺丰医寄通创建订单.
    /// </summary>
    /// <param name="post"></param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>创建订单响应.</returns>
    Task<SFYJTOrderResponse> SFYJTCreateOrderAsync(SFYJTPostInfo post, SFYJTSecurityOptions? securityOptions = null);

    /// <summary>
    /// 顺丰医寄通获取面单打印数据(76*130,不打印logo,默认脱敏项).
    /// </summary>
    /// <param name="merchantOrderNo">商户单号.</param>
    /// <param name="securityOptions">顺丰医寄通安全信息.</param>
    /// <returns>面单打印数据响应.</returns>
    Task<SFYJTPrintDataReponse> SFYJTGetPrintDataAsync(string merchantOrderNo, SFYJTSecurityOptions? securityOptions = null);
}

```
