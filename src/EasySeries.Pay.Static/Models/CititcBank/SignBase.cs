using System.Xml.Serialization;

namespace EasySeries.Pay.Static.Models.CititcBank;

/// <summary>
/// 签名基类.
/// </summary>
public class SignBase
{
    /// <summary>
    /// 签名-使用中信银行提供的证书进行签名.
    /// </summary>
    [JsonProperty("sign")]
    [XmlElement("sign")]
    internal string Sign { get; set; } = string.Empty;

    /// <summary>
    /// 加签模式.
    /// </summary>
    [JsonProperty("sign_mode")]
    internal string SignMode { get; } = "RSA";

    /// <summary>
    /// Set签名.
    /// </summary>
    internal void SetSign(Func<string, string, string> RSASign, string orgString, string keyFilePath)
    {
        Sign = RSASign(orgString, keyFilePath);
    }
}
