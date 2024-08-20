using System.Net;
using BuildingBlocksClient.Application.Identity.DTOs;
using BuildingBlocksClient.Application.Identity.Interfaces;

namespace Clients.BlazorWASM.Helpers
{
    public class CustomHttpDelegate(CustomHttpClient _httpClient,
        LocalStorageService _localstorageService,
        IAccountService _accountService) : DelegatingHandler
    {

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
            bool refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");

            if (loginUrl || registerUrl || refreshTokenUrl) return await base.SendAsync(request, cancellationToken);

            var result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                var stringToken = await _localstorageService.GetToken();
                if (stringToken == null) return result;

                string token = string.Empty;
                try
                {
                    token = request.Headers.Authorization!.Parameter!;
                }
                catch { }

                var deserilaizedToken = Serialization.DeserializeJsonString<TokenSession>(stringToken);
                if (deserilaizedToken is null) return result;

                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserilaizedToken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }
            }
            return result;
        }

    }
}
