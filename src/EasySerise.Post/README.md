## EasySerise.Post

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
  }
```

```c#
//注册
builder.Services.AddEasyPostService();

//使用
public YourController(IEasyPostJDL easyPostJDL)
{
  _easyPostJDL = easyPostJDL;
}
```

#### 即时配置模式

```c#
//注册
builder.Services.AddEasyPostService();

//使用
public YourController(IEasyPostJDL easyPostJDL)
{
  _easyPostJDL = easyPostJDL;
}}
    
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
