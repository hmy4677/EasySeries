using System.Drawing.Printing;

namespace EasySeries.FileOpe.Models;

public class PrinterInfo
{
    public string PrinterName { get; set; } = string.Empty;
    public PaperSize PaperSize { get; set; } = new();
    public bool IsDefault { get; set; } = false;
}