namespace EasySerise.Pay.Models.Wechat;

/// <summary>
/// 微信平台证书响应.
/// </summary>
internal class PlactCertResponse
{
    public List<PlatCertInfo> data { get; set; } = new List<PlatCertInfo>();
}

internal class PlatCertInfo
{
    public string serial_no { get; set; } = string.Empty;
    public string effective_time { get; set; } = string.Empty;
    public string expire_time { get; set; } = string.Empty;
    public EncryptCert encrypt_certificate { get; set; } = new EncryptCert();
}

internal class EncryptCert
{
    public string algorithm { get; set; } = string.Empty;
    public string nonce { get; set; } = string.Empty;
    public string associated_data { get; set; } = string.Empty;
    public string ciphertext { get; set; } = string.Empty;
}
