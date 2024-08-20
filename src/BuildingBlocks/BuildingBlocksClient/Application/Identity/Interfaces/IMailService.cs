using BuildingBlocksClient.Application.Identity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocksClient.Application.Identity.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request, CancellationToken ct);
    }
}
