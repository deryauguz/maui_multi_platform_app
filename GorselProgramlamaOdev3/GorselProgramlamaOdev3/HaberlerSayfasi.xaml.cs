using GorselProgramlamaOdev3.Models;
using System.Text.Json;

namespace GorselProgramlamaOdev3;

public partial class HaberlerSayfasi : ContentPage
{
    public HaberlerSayfasi()
    {
        InitializeComponent();
        KategoriSecici.SelectedIndex = 0; // Baþlangýçta Manþetleri yükle
    }

    private async Task HaberleriGetir(string rssUrl)
    {
        if (HaberYukleyici.IsRunning) return;

        HaberYukleyici.IsRunning = true;
        HaberYukleyici.IsVisible = true;

        try
        {
            // Önbellek sorununu aþmak için sonuna zaman damgasý ekliyoruz
            string apiUrl = $"https://api.rss2json.com/v1/api.json?rss_url={rssUrl}&t={DateTime.Now.Ticks}";

            using HttpClient istemci = new HttpClient();
            var yanit = await istemci.GetStringAsync(apiUrl);

            var seçenekler = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var veri = JsonSerializer.Deserialize<HaberResponse>(yanit, seçenekler);

            if (veri?.items != null)
            {
                HaberListesi.ItemsSource = veri.items;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Hata", "Haberler þu an yüklenemedi. Lütfen internetinizi kontrol edin.", "Tamam");
        }
        finally
        {
            HaberYukleyici.IsRunning = false;
            HaberYukleyici.IsVisible = false;
        }
    }

    private void KategoriDegisti(object sender, EventArgs e)
    {
        if (KategoriSecici.SelectedItem == null) return;

        string secilen = KategoriSecici.SelectedItem.ToString();
        string rssLink = secilen switch
        {
            "Ekonomi" => "https://www.trthaber.com/ekonomi_articles.rss",
            "Spor" => "https://www.trthaber.com/spor_articles.rss",
            "Teknoloji" => "https://www.trthaber.com/bilim_teknoloji_articles.rss",
            "Dünya" => "https://www.trthaber.com/dunya_articles.rss",
            _ => "https://www.trthaber.com/manset_articles.rss"
        };

        _ = HaberleriGetir(rssLink);
    }

    private async void HaberSecildi(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is HaberItem secilen)
        {
            await Navigation.PushAsync(new HaberDetaySayfasi(secilen.link, secilen.title));
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}