namespace EasySeries.Pay.Models.UnifyTrade;

/// <summary>
/// 中信全付通签名.
/// </summary>
public class UnifyTradeSign
{
    /// <summary>
    /// 签名-使用中信银行提供的证书进行签名.
    /// </summary>
    [JsonProperty("sign")]
    [XmlElement("sign")]
    public string Sign { get;  set; } = string.Empty;

    /// <summary>
    /// 加签模式.
    /// </summary>
    [JsonProperty("sign_mode")]
    public string SignMode { get; } = "RSA";

    /// <summary>
    /// Set签名.
    /// </summary>
    public void SetSign(Func<string, string, string> RSASign, string orgString, string keyFilePath)
    {
        Sign = RSASign(orgString, keyFilePath);
    }
}