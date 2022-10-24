using API_Challenge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace API_Challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController: ControllerBase
    {
        [HttpGet(Name = "GetCEP")]
        public async Task<ViaCep> GetAsync(string cepString)
        {
            var urlCep = new Uri($"https://viacep.com.br/ws/{cepString}/json/");
            using (HttpClient client = new() { BaseAddress = urlCep })
            {
                ViaCep? cep = await client.GetFromJsonAsync<ViaCep>("");
                return cep;
            }
        }
    }
}
