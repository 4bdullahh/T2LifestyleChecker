using System.Text.Json;
using T2LifestyleChecker.Web.Models;
using T2LifestyleChecker.Web.Services.Interfaces;

namespace T2LifestyleChecker.Web.Services
{
    public class PatientService : IPatientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public PatientService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }


        public async Task<Patient> GetPatientAsync(string NHSNumber)
        {
            var client = _httpClientFactory.CreateClient("PatientApi");
            var subscriptionKey = _configuration["PatientApi:SubscriptionKey"];

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            var response = await client.GetAsync($"tech-test/t2/patients/{NHSNumber}");

            if(response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var patient = JsonSerializer.Deserialize<Patient>(json, jsonOptions);
            
            return patient;
        }
    }
}
