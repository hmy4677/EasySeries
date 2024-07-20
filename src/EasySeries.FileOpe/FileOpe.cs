using System.Drawing.Printing;
using CliWrap;
using EasySeries.FileOpe.Models;
using RestSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Syncfusion.PdfToImageConverter;
using Image = System.Drawing.Image;

namespace EasySeries.FileOpe;

/// <summary>
///     文件操作.
/// </summary>
public static class FileOpe
{
    private static string _pdfPath = string.Empty;
    private static int _weight;
    private static int _height;

    /// <summary>
    ///     保存文件至目标文件夹.
    /// </summary>
    /// <param name="stream">文件流.</param>
    /// <param name="directory">目标文件夹.</param>
    /// <param name="fileName">文件名.</param>
    /// <param name="allows">允许的扩展名-可选.</param>
    /// <exception cref="FileLoadException"></exception>
    public static async Task<string> SaveFileAsync(Stream stream, string directory, string fileName, string[]? allows = default)
    {
        if(allows is {Length: > 0} && !allows.Contains(Path.GetExtension(fileName))) throw new FileLoadException("不允许的文件类型");

        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        var filePath = Path.Combine(directory, fileName);
        await using var fs = File.OpenWrite(filePath);
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(fs);
        await stream.DisposeAsync();
        return filePath;
    }

    /// <summary>
    ///     获取文件Buffer.
    /// </summary>
    /// <param name="filePath">文件路径.</param>
    /// <returns>文件二进制,MIME类型.</returns>
    /// <exception cref="Exception">类型不存在.</exception>
    public static async Task<(byte[] Buffer, string MIME, string FileName)> GetFileBufferAsync(string filePath)
    {
        if(!File.Exists(filePath)) throw new FileNotFoundException("文件不存在", Path.GetFileName(filePath));

        return (await File.ReadAllBytesAsync(filePath), Mime(filePath), Path.GetFileName(filePath));
    }

    /// <summary>
    ///     获取文件Stream.
    /// </summary>
    /// <param name="filePath">文件路径.</param>
    /// <returns>文件流,MIME类型.</returns>
    /// <exception cref="Exception">类型不存在.</exception>
    public static (FileStream Stream, string MIME, string FileName) GetFileSteam(string filePath)
    {
        if(!File.Exists(filePath)) throw new Exception("文件不存在");

        return (File.OpenRead(filePath), Mime(filePath), Path.GetFileName(filePath));
    }

    /// <summary>
    ///     远程HTTP下载.
    /// </summary>
    /// <param name="url">文件url.</param>
    /// <param name="headerName">鉴权key(eg:X-Auth-token).</param>
    /// <param name="headerValue">鉴权value</param>
    /// <param name="cancellationToken">可取消.</param>
    /// <returns>文件流.</returns>
    /// <exception cref="Exception">下载异常.</exception>
    public static async Task<Stream> RemoteDownFileAsync(string url, string headerName, string headerValue, CancellationToken cancellationToken = default)
    {
        var client = new RestClient();
        var request = new RestRequest(url);
        request.AddHeader(headerName, headerValue);

        var stream = await client.DownloadStreamAsync(request, cancellationToken);
        if(stream is null) throw new Exception("下载失败:Steam为空");

        return stream;
    }

    /// <summary>
    ///     删除文件.
    /// </summary>
    /// <param name="filePath">文件路径.</param>
    public static void Delete(string filePath)
    {
        if(!File.Exists(filePath)) throw new FileLoadException("文件不存在");

        File.Delete(filePath);
    }

    /// <summary>
    ///     写txt文件日志.
    /// </summary>
    /// <param name="directory">日志目录.</param>
    /// <param name="type">消息类型:INF,ERR...</param>
    /// <param name="message">消息.</param>
    public static async Task TxtLogAsync(string directory, string type, string message)
    {
        if(!Directory.Exists(directory)) Directory.CreateDirectory(directory);

        var fileName = $"{DateTime.Today:yyyy-MM-dd}.txt";
        var filePath = Path.Combine(directory, fileName);
        var content = new List<string>
        {
            $"[{type}]-{DateTime.Now:HH:mm:ss:fff}-{message}"
        };

        await File.AppendAllLinesAsync(filePath, content);
    }

    /// <summary>
    ///     MIME.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static string Mime(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return extension switch
        {
            ".zip" => "application/zip",
            ".rar" => "application/rar",
            ".jpg" => "image/jpeg",
            ".png" => "image/jpeg",
            ".mp3" => "audio/mpeg",
            ".mp4" => "video/mpeg",
            _ => "other"
        };
    }

    /// <summary>
    ///     获取本机打印机列表(仅Windows 6.1及以上支持).
    /// </summary>
    /// <returns>打印机列表.</returns>
    public static List<PrinterInfo> GetPrinterList()
    {
        var printDocument = new PrintDocument();
        var list = new List<PrinterInfo>
        {
            new()
            {
                PrinterName = printDocument.DefaultPageSettings.PrinterSettings.PrinterName,
                PaperSize = printDocument.DefaultPageSettings.PaperSize,
                IsDefault = true
            }
        };

        var installedPrinters = PrinterSettings.InstalledPrinters;
        foreach(string item in installedPrinters)
            if(item != list[0].PrinterName)
                list.Add(new PrinterInfo
                {
                    PrinterName = item
                });

        return list;
    }

    /// <summary>
    ///     打印PDF文件(仅Windows 6.1及以上支持).
    /// </summary>
    /// <param name="pdfPath">PDF文件路径.</param>
    /// <returns></returns>
    /// <exception cref="InvalidPrinterException"></exception>
    public static string PrintPdf(string pdfPath)
    {
        _pdfPath = pdfPath;

        var printDocument = new PrintDocument();
        var paperSize = printDocument.DefaultPageSettings.PaperSize;
        _weight = paperSize.Width;
        _height = paperSize.Height;

        printDocument.PrintPage += PrintPage_PDF;
        printDocument.BeginPrint += (sender, e) =>
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"info: 打印作业开始:{_pdfPath}");
        };

        printDocument.EndPrint += (sender, e) =>
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"info: 打印作业完成:{_pdfPath}");
        };

        if(printDocument.PrinterSettings.IsValid)
        {
            printDocument.Print();
            return printDocument.PrinterSettings.PrinterName;
        }

        throw new InvalidPrinterException(printDocument.PrinterSettings);
    }

    /// <summary>
    ///     PDF转Image.
    /// </summary>
    /// <param name="filePath">PDF文件路径.</param>
    /// <param name="saveToDirectory">图像保存文件夹-可选.</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public static async Task<Stream> PdfToImageAsync(string filePath, string? saveToDirectory = null)
    {
        if(!File.Exists(filePath)) throw new FileNotFoundException("文件不存在");

        var imageConverter = new PdfToImageConverter();
        using var streamPdf = File.OpenRead(filePath);
        imageConverter.ScaleFactor = 1.5f;
        imageConverter.Load(streamPdf);

        var streamImg = imageConverter.Convert(0, 2, 1, 1, 0, 0);

        if(string.IsNullOrEmpty(saveToDirectory)) return streamImg;

        await SaveFileAsync(streamImg, saveToDirectory, $"{DateTime.Now:yyyyMMddHHmmssfff}.png");

        return streamImg;
    }

    /// <summary>
    ///     打印事件_PDF.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static async void PrintPage_PDF(object sender, PrintPageEventArgs e)
    {
        var stream = await PdfToImageAsync(_pdfPath);
        var img = Image.FromStream(stream);
        e.Graphics!.DrawImage(img, 0, 0, _weight, _height);

        await stream.DisposeAsync();
    }

    /// <summary>
    ///     FFmpeg截图命令(需要先安装FFmpeg,并加入环境变量,以供命令调用).
    /// </summary>
    /// <param name="directory">视频文件夹.</param>
    /// <param name="fileName">视频文件名.</param>
    /// <returns>图片路径.</returns>
    /// <exception cref="FileNotFoundException">文件不存在异常.</exception>
    public static async Task<string> FFmpegSnippingAsync(string directory, string fileName)
    {
        if(!File.Exists(Path.Combine(directory, fileName))) throw new FileNotFoundException(fileName);

        //ffmpeg -i ./desk.mp4 -t 0.1 -vframes 1 -q:v 3 ./output.jpg
        var output = $"{Guid.NewGuid()}.jpg";
        await Cli.Wrap("ffmpeg").WithArguments($"-i ./{fileName} -t 0.1 -vframes 1 -q:v 3 ./{output}").WithWorkingDirectory(directory).ExecuteAsync();

        return Path.Combine(directory, output);
    }

    /// <summary>
    ///     FFmpeg截图命令(需要先安装FFmpeg,并加入环境变量,以供命令调用).
    /// </summary>
    /// <param name="directory">视频文件夹.</param>
    /// <param name="fileName">视频文件名.</param>
    /// <param name="commandParams">命令参数,参考官方文档(eg: -i ./desk.mp4 -t 0.1 -vframes 1 -q:v 3 ./output.jpg).</param>
    /// <param name="outputDirectory">输入文件夹(空则同目录).</param>
    /// <param name="outputName">输入文件名(空则随机).(eg: output.jpg).</param>
    /// <returns>图片路径.</returns>
    /// <exception cref="FileNotFoundException">文件不存在异常.</exception>
    public static async Task<string> FFmpegSnippingAsync(string directory, string fileName, string commandParams, string? outputDirectory = default, string? outputName = default)
    {
        if(!File.Exists(Path.Combine(directory, fileName))) throw new FileNotFoundException(fileName);

        outputDirectory ??= directory;
        outputName ??= Guid.NewGuid().ToString("N");

        if(!Directory.Exists(outputDirectory)) Directory.CreateDirectory(outputDirectory);

        await Cli.Wrap("ffmpeg").WithArguments(commandParams).WithWorkingDirectory(directory).ExecuteAsync();

        return Path.Combine(outputDirectory, outputName);
    }

    /// <summary>
    ///     缩略图.
    /// </summary>
    /// <param name="stream">图片文件stream.</param>
    /// <param name="width">宽度.</param>
    /// <param name="height">长度.</param>
    /// <param name="saveToDirectory">保存到文件夹-可选.</param>
    /// <returns>缩略图buffer.</returns>
    public static async Task<byte[]> ThumbnailAsync(Stream stream, int width = 100, int height = 100, string? saveToDirectory = default)
    {
        using var image = await SixLabors.ImageSharp.Image.LoadAsync(stream);
        image.Mutate(p => p.Resize(new ResizeOptions
        {
            Size = new Size(width, height),
            Mode = ResizeMode.Crop,
            Position = AnchorPositionMode.Center,
            Sampler = KnownResamplers.Box,
            Compand = true
        }));

        using var ms = new MemoryStream();
        await image.SaveAsPngAsync(ms);

        if(!string.IsNullOrEmpty(saveToDirectory)) await SaveFileAsync(ms, saveToDirectory, $"thumb_{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}.png");

        return ms.ToArray();
    }
}