using System.Net.Http;

namespace Barfas_1.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://81.213.79.71/barfas/rfid/sayac.php?=ok";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task SendCounterReachedNotification()
        {
            try
            {
                var response = await _httpClient.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"API çağrısı hatası: {ex.Message}");
                throw;
            }
        }
    }
} 