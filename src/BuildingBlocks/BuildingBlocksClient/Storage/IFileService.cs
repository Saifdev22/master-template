using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocksClient.Storage
{
    public interface IFileService
    {
        Task<string> HandleFileUploads(IFormFileCollection files);
    }
}
