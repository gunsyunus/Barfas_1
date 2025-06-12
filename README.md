# RFID Tag Okuyucu MAUI Uygulaması

Bu uygulama, UHF RFID okuyucu cihazından gelen verileri TCP/IP WebSocket bağlantısı üzerinden dinleyen ve görselleştiren bir .NET MAUI uygulamasıdır.

## Özellikler

- Gerçek zamanlı RFID tag verisi okuma
- Tag verilerini liste görünümünde gösterme
- Okunan tag sayısı takibi
- Kullanıcı tanımlı eşik değeri kontrolü
- Eşik değerine ulaşıldığında otomatik durdurma
- REST API entegrasyonu

## Gereksinimler

- .NET 7.0 veya üzeri
- Visual Studio 2022 (17.4 veya üzeri) with .NET MAUI workload

## Kurulum

1. Repoyu klonlayın:
```bash
git clone https://github.com/[KULLANICI_ADI]/Barfas_1.git
```

2. Visual Studio'da projeyi açın
3. Gerekli NuGet paketlerinin yüklenmesini bekleyin
4. Projeyi derleyin ve çalıştırın

## Kullanım

1. Uygulamayı başlatın
2. Eşik değerini girin
3. "Başlat" butonuna tıklayın
4. Tag verileri otomatik olarak listelenecektir
5. Eşik değerine ulaşıldığında okuma otomatik olarak duracak ve sunucuya bildirim gönderilecektir

## Simülasyon Modu

Uygulama şu anda simülasyon modunda çalışmaktadır. Gerçek cihaz entegrasyonu için `RfidWebSocketService.cs` dosyasındaki WebSocket URL'sini ve bağlantı mantığını güncellemeniz gerekmektedir. 