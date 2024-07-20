using Microsoft.AspNetCore.Mvc;
using EasySeries.FileOpe;

namespace EasySeries.Simple.Controllers;

public class FileController : Controller
{
    [HttpGet("file")]
    public async Task<FileContentResult> DownFileAsync(string filePath)
    {
        var fileBuffer = await FileOpe.FileOpe.GetFileBufferAsync(filePath);
        return File(fileBuffer.Buffer, fileBuffer.MIME, fileBuffer.FileName);
    }

    [HttpGet("printers")]
    public void GetPrinterList()
    {
        var list = FileOpe.FileOpe.GetPrinterList();
    }
}