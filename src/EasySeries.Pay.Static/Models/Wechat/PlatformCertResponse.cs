namespace EasySeries.Pay.Static.Models.Wechat;

internal class PlatformCertResponse
{
    [JsonProperty("data")]
    public List<PlatCertInfo> Data { get; set; } = new();
}

internal class PlatCertInfo
{
    [JsonProperty("serial_no")]
    public string SerialNo { get; set; } = string.Empty;

    [JsonProperty("effective_time")]
    public string EffectiveTime { get; set; } = string.Empty;

    [JsonProperty("expire_time")]
    public string ExpireTime { get; set; } = string.Empty;

    [JsonProperty("encrypt_certificate")]
    public EncryptCert EncryptCertificate { get; set; } = new EncryptCert();
}

internal class EncryptCert
{
    [JsonProperty("algorithm")]
    public string Algorithm { get; set; } = string.Empty;

    [JsonProperty("nonce")]
    public string Nonce { get; set; } = string.Empty;

    [JsonProperty("associated_data")]
    public string AssociatedData { get; set; } = string.Empty;

    [JsonProperty("ciphertext")]
    public string Ciphertext { get; set; } = string.Empty;
}
