## EasySeries.FileOpe

Easy文件操作，Easy系列的第三个应用，用于文件操作,PDF打印,视频截图,缩略图。

### 使用说明:
```c#
    //全 static 方法,直接调用即可.
    [HttpGet("printers")]
    public void GetPrinterList()
    {
        var list = FileOpe.FileOpe.GetPrinterList();
    }
    
```

### 一览
    //保存文件至目标文件夹.
    public static async Task<string> SaveFileAsync(Stream stream, string directory, string fileName, string[]? allows = default)