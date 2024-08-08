## EasySeries.FileOpe

Easy文件操作，Easy系列的第三个应用，用于文件操作,PDF打印,视频截图,缩略图。

### 使用说明:
```c#

using EasySeries.FileOpe.Models;
using EasySeries.FileOpe;
using Microsoft.AspNetCore.Mvc;
namespace EasySeries.Simple.Controllers;

public class FileController : Controller
{
    //全static方法,直接调用即可.
    
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
    
```

### 一览
```c#

    //保存文件至目标文件夹.
    SaveFileAsync();
    
    //获取文件Buffer.
    GetFileBufferAsync();
    
    //获取文件Stream.
    GetFileSteam();

    //远程HTTP下载.
    RemoteDownFileAsync();

    //写txt文件日志.
    TxtLogAsync();

    //获取本机打印机列表(仅Windows 6.1及以上支持).
    GetPrinterList();
    
    //打印PDF文件(仅Windows 6.1及以上支持).
    PrintPdf();
    
    //PDF转Image.
    PdfToImageAsync();
    
    //FFmpeg截图命令(需要先安装FFmpeg,并加入环境变量,以供命令调用).
    FFmpegSnippingAsync();
    
    //FFmpeg截图命令(需要先安装FFmpeg,并加入环境变量,以供命令调用).
    FFmpegSnippingAsync();
    
    //缩略图.
    ThumbnailAsync();
    
```
