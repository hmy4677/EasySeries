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
    private readonly IEasyPostSF _easyPostSF;

    public PostController(IEasyPostKuaiDi100 easyPostKuaiDi100, IEasyPostJDL easyPostJDL, IEasyPostSF easyPostSF)
    {
        _easyPostKuaiDi100 = easyPostKuaiDi100;
        _easyPostJDL = easyPostJDL;
        _easyPostSF = easyPostSF;
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

    [HttpGet("sf")]
    public async Task<dynamic> SFQueryAsync(string type, string no)
    {
        return await _easyPostSF.SFYJTQueryAsync(type,no);
    }
}
