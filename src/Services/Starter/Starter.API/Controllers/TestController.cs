using Azure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Starter.Application.Endpoints.Categories.Queries;
using System.Collections;
using System.Net.Http;

namespace Starter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(IHttpClientFactory _factory) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get(int productNo)
        {
            var client = _factory.CreateClient("testapi");

            var response = await client.GetAsync("/products" + $"/{productNo}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // var res = await httpClient.GetAsync<List<Car>>("/api/cars")
            // .GetFromJsonAsync<>($"/products/{productNo}");

            return Ok(responseBody);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int productNo)
        {
            var client = _factory.CreateClient("testapi");
            var response = await client.GetAsync($"/products/{productNo}");

            return Ok(client);
        }

    }
}
