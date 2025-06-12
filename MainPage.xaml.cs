using Barfas_1.Models;
using Barfas_1.Services;
using System.Collections.ObjectModel;

namespace Barfas_1;

public partial class MainPage : ContentPage
{
    private readonly RfidWebSocketService _rfidService;
    private readonly ApiService _apiService;
    private ObservableCollection<RfidTag> _tags;
    private int _tagCount = 0;
    private int _threshold = 0;
    private bool _thresholdReached = false;

    public MainPage(RfidWebSocketService rfidService, ApiService apiService)
    {
        InitializeComponent();
        _rfidService = rfidService;
        _apiService = apiService;
        _tags = new ObservableCollection<RfidTag>();
        TagListView.ItemsSource = _tags;

        _rfidService.OnTagReceived += OnTagReceived;
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        if (int.TryParse(SayacInput.Text, out int threshold))
        {
            _threshold = threshold;
            _thresholdReached = false;
            _tagCount = 0;
            _tags.Clear();
            TagCounter.Text = "0";
            ThresholdWarning.IsVisible = false;
            
            await _rfidService.ConnectAsync();
            StartButton.IsEnabled = false;
            SayacInput.IsEnabled = false;
        }
        else
        {
            await DisplayAlert("Hata", "Lütfen geçerli bir sayı girin", "Tamam");
        }
    }

    private async void OnTagReceived(object sender, RfidTag tag)
    {
        if (_thresholdReached) return;

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            _tags.Insert(0, tag);
            _tagCount++;
            TagCounter.Text = _tagCount.ToString();

            if (_tagCount >= _threshold && !_thresholdReached)
            {
                _thresholdReached = true;
                ThresholdWarning.IsVisible = true;
                await _rfidService.DisconnectAsync();
                await NotifyThresholdReached();
                
                // Kontrolleri aktif hale getir
                StartButton.IsEnabled = true;
                SayacInput.IsEnabled = true;
            }
        });
    }

    private async Task NotifyThresholdReached()
    {
        try
        {
            await _apiService.SendCounterReachedNotification();
            await DisplayAlert("Başarılı", "Sunucuya bildirim gönderildi", "Tamam");
        }
        catch (Exception ex)
        {
            bool tekrarDene = await DisplayAlert("API Hatası", 
                "Sunucuya bildirim gönderilemedi. Tekrar denemek ister misiniz?", 
                "Evet", "Hayır");
            
            if (tekrarDene)
            {
                await NotifyThresholdReached();
            }
        }
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        await _rfidService.DisconnectAsync();
    }
}
