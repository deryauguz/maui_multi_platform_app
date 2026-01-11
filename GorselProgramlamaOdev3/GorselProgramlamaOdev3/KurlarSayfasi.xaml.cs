using System.Collections.ObjectModel;
using System.Text.Json;
using GorselProgramlamaOdev3.Model;

namespace GorselProgramlamaOdev3;

public partial class KurlarSayfasi : ContentPage
{
    public ObservableCollection<Doviz> Dovizler { get; set; } = new();

    public KurlarSayfasi()
    {
        InitializeComponent();
        KurlarListe.ItemsSource = Dovizler;

        // Sayfa ilk açıldığında verileri yükle
        _ = VerileriYukle();
    }

    private async void OnYenileClicked(object sender, EventArgs e)
    {
        await VerileriYukle();
    }

    // 2. Liste aşağı doğru çekildiğinde (RefreshView) tetiklenir
    private async void OnRefreshing(object sender, EventArgs e)
    {
        await VerileriYukle();
        // Yenileme animasyonunu (halkayı) kapatır
        YenilemeKontrolü.IsRefreshing = false;
    }

    private async Task VerileriYukle()
    {
        if (Yukleyici.IsRunning) return;

        Yukleyici.IsVisible = true;
        Yukleyici.IsRunning = true;

        try
        {
            using HttpClient client = new HttpClient();
            // Bazı API'ler bot engellemek için User-Agent bekler
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

            var json = await client.GetStringAsync("https://finans.truncgil.com/today.json");
            using JsonDocument doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            // Listeyi temizle ve yeni verileri ekle
            Dovizler.Clear();

            string[] kurlar = {
                "BIST 100", "USD", "EUR", "GBP",
                "gram-altin", "ceyrek-altin", "gram-gumus",
                "BTC", "ETH"
            };

            foreach (var kod in kurlar)
            {
                if (root.TryGetProperty(kod, out JsonElement detay))
                {
                    // Alış, Satış ve Değişim değerlerini güvenli çekiyoruz
                    string alis = GetVal(detay, "Alış", "Buying");
                    string satis = GetVal(detay, "Satış", "Selling");
                    string fark = GetVal(detay, "Değişim", "Change");

                    // BIST 100 verisinde Alış alanı boşsa Fiyat alanını kullan
                    if (alis == "0.00" && detay.TryGetProperty("Fiyat", out var f))
                        alis = f.ToString();

                    // Değişim içinde "-" varsa düşüş, yoksa yükseliş kabul edilir
                    bool dusus = fark.Contains("-");

                    Dovizler.Add(new Doviz
                    {
                        Tur = FormatAd(kod),
                        Alis = alis,
                        Satis = satis,
                        Fark = fark,
                        Renk = dusus ? Colors.Red : Colors.Green,
                        YonOk = dusus ? "↓" : "↑"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Veriler güncellenemedi: " + ex.Message, "Tamam");
        }
        finally
        {
            // İşlem bittiğinde yükleme göstergelerini kapat
            Yukleyici.IsVisible = false;
            Yukleyici.IsRunning = false;
        }
    }

    // API'den gelen verilerdeki farklı anahtar isimlerini kontrol eden yardımcı metod
    private string GetVal(JsonElement el, string tr, string en)
    {
        if (el.TryGetProperty(tr, out var v1)) return v1.ToString();
        if (el.TryGetProperty(en, out var v2)) return v2.ToString();
        return "0.00";
    }

    // API kodlarını kullanıcı dostu isimlere çeviren metod
    private string FormatAd(string kod) => kod switch
    {
        "gram-altin" => "Gram Altın",
        "ceyrek-altin" => "Çeyrek Altın",
        "gram-gumus" => "Gümüş (Gram)",
        "BTC" => "Bitcoin (USD)",
        "ETH" => "Ethereum (USD)",
        "USD" => "ABD Doları",
        "EUR" => "Euro",
        "GBP" => "İngiliz Sterlini",
        _ => kod
    };
}