using MediatR;
using Microsoft.AspNetCore.Mvc;
using Starter.Application.ProductContract.Commands;

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

        [HttpGet("GetWithNone")]
        public async Task<ActionResult<string>> GetWithNone()
        {
            await Task.Delay(4000);
            var name = "";
            if (string.IsNullOrWhiteSpace(name)) { name = "KhanSaif"; }
            Console.WriteLine($"Hello, {name}!");
            return Ok(name);
        }
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand request)
        {
           // var result = await _sender.Send(request);
            return Ok(request);
        }

    }
}
