namespace EasySeries.MiniProgram.Utils;

internal class HttpRequest
{
    /// <summary>
    /// 执行HTTP Post.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="api"></param>
    /// <param name="requestBody"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    internal static async Task<T> ExecutePostAsync<T>(string api, object? requestBody = null)
    {
        var client = new RestClient(api);
        var request = new RestRequest();
        if(requestBody != null)
        {
            request.AddBody(requestBody, ContentType.Json);
        }

        var response = await client.ExecutePostAsync(request);
        if(response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<T>(response?.Content ?? "")!;
        }
        else
        {
            throw new ArgumentException(response.Content);
        }
    }

    /// <summary>
    /// 执行HTTP Get.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="api"></param>
    /// <param name="header"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    internal static async Task<T> ExecuteGetAsync<T>(string api, Dictionary<string, string>? header, Dictionary<string, string>? query)
    {
        var client = new RestClient(api);
        var request = new RestRequest();
        if(header != null)
        {
            foreach(var item in header)
            {
                request.AddHeader(item.Key, item.Value);
            }
        }

        if(query != null)
        {
            foreach(var item in query)
            {
                request.AddQueryParameter(item.Key, item.Value);
            }
        }

        var response = await client.ExecuteGetAsync(request);
        if(response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<T>(response?.Content ?? "")!;
        }
        else
        {
            throw new ArgumentException(response.Content);
        }
    }
}
