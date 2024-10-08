﻿using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using static BuildingBlocksClient.Shared.DTOs.ServiceResponses;

namespace Clients.BlazorWASM.Services
{
    public class UserService(CustomHttpClient _httpClient) : IUserService
    {
        public const string baseUrl = "identity/users";
        public async Task<List<GetUserDTO>> GetAllUsers()
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var result = await http.GetAsync($"{baseUrl}");
            return await result.Content.ReadFromJsonAsync<List<GetUserDTO>>() ?? new List<GetUserDTO>();
        }

        public async Task<GetUserDTO> GetUserById(string userId)
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var result = await http.GetAsync($"{baseUrl}/{userId}");
            return await result.Content.ReadFromJsonAsync<GetUserDTO>() ?? new GetUserDTO();
        }

        public async Task<GeneralResponse> UpdateUser(GetUserDTO userDTO)
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var response = await http.PutAsJsonAsync(baseUrl, userDTO);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<GeneralResponse> DeleteUserById(string userId)
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var response = await http.DeleteAsync($"{baseUrl}/{userId}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public Task<GeneralResponse> CreateUser(CreateUserDTO user, IFormFileCollection files)
        {
            throw new NotImplementedException();
        }

        //public async Task<GeneralResponse> CreateAccount(CreateUserDTO user)
        //{
        //    var httpclient = _httpClient.GetPublicHttpClient();

        //    var response = await httpclient.PostAsJsonAsync($"{baseUrl}/register", user);
        //    var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        //    return result!;
        //}
    }


}

