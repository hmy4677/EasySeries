using EasySeries.MiniProgram.Models.Baidu;
using EasySeries.MiniProgram.Models.Wechat;
using System.Text.RegularExpressions;

namespace EasySeries.MiniProgram.Utils;

public class MobileData
{
    /// <summary>
    /// 解密手机号数据-抖音/快手.
    /// </summary>
    /// <param name="encryptedData"></param>
    /// <param name="sessionKey"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static PhoneInfo Decrypt(string encryptedData, string sessionKey, string iv)
    {
        var json = AES.Decrypt(encryptedData, sessionKey, iv);
        return JsonConvert.DeserializeObject<PhoneInfo>(json) ?? new();
    }

    /// <summary>
    /// 解密手机号数据-百度.
    /// </summary>
    /// <param name="encryptedData"></param>
    /// <param name="sessionKey"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static BaiduMobile DecryptForBaidu(string encryptedData, string sessionKey, string iv)
    {
        var text = AES.Decrypt(encryptedData, sessionKey, iv);
        var json = Regex.Match(text, "\\{([^}]*)\\}").Value;
        return JsonConvert.DeserializeObject<BaiduMobile>(json) ?? new();
    }
}
