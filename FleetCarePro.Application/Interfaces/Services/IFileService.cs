namespace FleetCarePro.Application.Interfaces.Services;

public interface IFileService
{
    Task<string> UploadAsync(Stream fileStream,string fileName,string folderName);
    Task DeleteAsync(string filePath);
}