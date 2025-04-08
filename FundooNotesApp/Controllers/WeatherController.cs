using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Apigen.Attributes;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {

        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public WeatherController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        { 
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }



        [HttpGet("GetWeatherData")]
        public async Task<IActionResult> GetWeatherData(string city)
        {
            try
            {

                var apiKey = configuration["WeatherApiKey"];
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

                var client = httpClientFactory.CreateClient();
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonDocument.Parse(json);

                    var temperature = data.RootElement.GetProperty("main").GetProperty("temp").GetDecimal();
                    var condition = data.RootElement.GetProperty("weather")[0].GetProperty("main").GetString();

                    return Ok(new
                    {
                        City = city,
                        Temperature = $"{temperature} °C",
                        Condition = condition
                    });
                }

                else
                {
                    return BadRequest("Failed to fetch weather data");

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
