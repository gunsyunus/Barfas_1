using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Barfas_1.Models;

namespace Barfas_1.Services
{
    public class RfidWebSocketService
    {
        private ClientWebSocket webSocket;
        private bool isConnected;
        private readonly string webSocketUrl = "ws://localhost:8080"; // Simülasyon için örnek URL
        public event EventHandler<RfidTag> OnTagReceived;

        public RfidWebSocketService()
        {
            webSocket = new ClientWebSocket();
        }

        public async Task ConnectAsync()
        {
            try
            {
                if (!isConnected)
                {
                    // Simülasyon modunda gerçek bağlantı yapmıyoruz
                    isConnected = true;
                    _ = StartListening();
                    System.Diagnostics.Debug.WriteLine("Simülasyon başlatıldı");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Bağlantı hatası: {ex.Message}");
                throw;
            }
        }

        private async Task StartListening()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Tag üretimi başladı");
                while (isConnected)
                {
                    // Simülasyon için örnek veri oluşturma
                    var simulatedTag = new RfidTag
                    {
                        TagId = Guid.NewGuid().ToString("N").Substring(0, 12),
                        AntennaPort = "1",
                        Rssi = Random.Shared.Next(-70, -30),
                        ReadTime = DateTime.Now
                    };

                    System.Diagnostics.Debug.WriteLine($"Yeni tag üretildi: {simulatedTag.TagId}");
                    OnTagReceived?.Invoke(this, simulatedTag);
                    await Task.Delay(500); // Her yarım saniyede bir tag simüle et
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Dinleme hatası: {ex.Message}");
                isConnected = false;
                throw;
            }
        }

        public async Task DisconnectAsync()
        {
            if (isConnected)
            {
                isConnected = false;
                System.Diagnostics.Debug.WriteLine("Simülasyon durduruldu");
            }
        }
    }
} 