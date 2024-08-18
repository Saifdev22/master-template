using Identity.API.Infrastructure.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocksClient.Identity.Interfaces
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request, CancellationToken ct);
    }
}
