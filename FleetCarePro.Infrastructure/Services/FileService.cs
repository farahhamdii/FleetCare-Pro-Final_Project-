using FleetCarePro.Application.Interfaces.Services;

namespace FleetCarePro.Infrastructure.Services;

public class FileService : IFileService
{
    private readonly string _rootPath;

    public FileService()
    {
        _rootPath = Path.Combine( Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string folderName)
    {
        var folderPath = Path.Combine(_rootPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var extension = Path.GetExtension(fileName);
        var uniqueFileName=$"{Guid.NewGuid()}{extension}";
        var filePath=Path.Combine(folderPath, uniqueFileName);
        await using var stream=new FileStream(filePath,FileMode.Create);
        await fileStream.CopyToAsync(stream);
        return $"/uploads/{folderName}/{uniqueFileName}";
    }

    public async Task DeleteAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))return;

        var fullPath = Path.Combine(Directory.GetCurrentDirectory(),
            "wwwroot",
            filePath.TrimStart('/'));

        if (File.Exists(fullPath))
        {
            await Task.Run(() => File.Delete(fullPath));
        }
    }
}