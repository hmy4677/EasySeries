using EasySeries.Post;
using EasySeries.Post.Models.KuaiDi100;
using Microsoft.AspNetCore.Mvc;

namespace EasySeries.Simple.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IEasyPostKuaiDi100 _easyPostKuaiDi100;
    private readonly IEasyPostJDL _easyPostJDL;

    public PostController(IEasyPostKuaiDi100 easyPostKuaiDi100, IEasyPostJDL easyPostJDL)
    {
        _easyPostKuaiDi100 = easyPostKuaiDi100;
        _easyPostJDL = easyPostJDL;
    }

    [HttpPost("kd100")]
    public async Task<dynamic> QueryAsync(Kd100OrderQuery query)
    {
        return await _easyPostKuaiDi100.Kd100QueryOrderAsync(query);
    }

    [HttpGet("jdl")]
    public async Task<dynamic> QueryJDLAsync(string postNo)
    {
        return await _easyPostJDL.JDLQueryOrderAsyncc(postNo);
    }

    [HttpGet("pre")]
    public async Task<dynamic> PreCheckAsync(string sendAddr, string recAddr)
    {
        return await _easyPostJDL.JDLCheckPreOrderAsync(sendAddr, recAddr);
    }
}
