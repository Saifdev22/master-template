using Microsoft.AspNetCore.Http;

namespace BuildingBlocksClient.Shared.Interfaces
{
    public interface IFileService
    {
        Task<string> HandleFileUploads(IFormFileCollection files);
    }
}
