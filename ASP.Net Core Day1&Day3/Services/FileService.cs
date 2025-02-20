using System;
using System.IO;
using System.Threading.Tasks;

public interface IFileService
{
    Task WriteToFileAsync(String content);
    Task<String> ReadFromFileAsync();
}

public class FileService
{
    private static readonly SemaphoreSlim _semaphore = new(1, 1); // Ensures proper async locking
    private readonly string _filePath;

    public FileService()
    {
        _filePath = "data.txt"; // Specify your file path
        if (!File.Exists(_filePath))
        {
            File.Create(_filePath).Close(); // Ensure the file exists
        }
    }

    public async Task WriteToFileAsync(string content)
    {
        await _semaphore.WaitAsync();
        try
        {
            await File.WriteAllTextAsync(_filePath, content + Environment.NewLine + Environment.NewLine); // Overwrites the file
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<string> ReadFromFileAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            return File.Exists(_filePath) ? await File.ReadAllTextAsync(_filePath) : string.Empty;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
