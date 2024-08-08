using EasySeries.FileOpe.Models;
using EasySeries.FileOpe;
using Microsoft.AspNetCore.Mvc;
namespace EasySeries.Simple.Controllers;

public class FileController : Controller
{
    [HttpGet("file")]
    public async Task<FileContentResult> DownFileAsync(string filePath)
    {
        var fileBuffer = await EasyFile.GetFileBufferAsync(filePath);
        return File(fileBuffer.Buffer, fileBuffer.MIME, fileBuffer.FileName);
    }

    [HttpGet("printers")]
    public List<PrinterInfo> GetPrinterList()
    {
        return EasyFile.GetPrinterList();
    }
}