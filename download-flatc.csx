using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Runtime.InteropServices;

var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "bin");
var version = "25.12.19";

async Task DownloadFlatc()
{
    if (!Directory.Exists(outputPath))
        Directory.CreateDirectory(outputPath);

    string filename;
    string extension;

    if (OperatingSystem.IsWindows())
    {
        filename = "Windows.flatc.binary.zip";
        extension = ".exe";
    }
    else if (OperatingSystem.IsLinux())
    {
        filename = "Linux.flatc.binary.g++-13.zip";
        extension = "";
    }
    else if (OperatingSystem.IsMacOS())
    {
        filename = "Mac.flatc.binary.zip";
        extension = "";
    }
    else
        throw new NotSupportedException(
            $"Operative System {RuntimeInformation.OSDescription} not supported."
        );

    var targetFile = Path.Combine(outputPath, $"flatc{extension}");

    if (File.Exists(targetFile))
    {
        Console.WriteLine("flatc already installed.");
        return;
    }

    Console.WriteLine($"Downloading flatc version v{version}...");
    var url = $"https://github.com/google/flatbuffers/releases/download/v{version}/{filename}";
    using var client = new HttpClient();
    using var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
    response.EnsureSuccessStatusCode();
    await using (var httpStream = await response.Content.ReadAsStreamAsync())
    await using (var fileStream = File.Create(filename))
    {
        await httpStream.CopyToAsync(fileStream);
    }
    ZipFile.ExtractToDirectory(filename, outputPath);
    File.Delete(filename);

    Console.WriteLine($"flatc installed at: {Path.Combine(outputPath, filename + extension)}");
}

try
{
    await DownloadFlatc();
}
catch (Exception e)
{
    Console.WriteLine($"Error while downloading flatc: {e.Message}");
    Console.WriteLine(e.StackTrace);
}
