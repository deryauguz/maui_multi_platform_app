using System.Collections.ObjectModel;
using System.Text.Json;
using GorselProgramlamaOdev3.Models;

namespace GorselProgramlamaOdev3;

public partial class HavaDurumuSayfasi : ContentPage
{
    public ObservableCollection<HavaDurumuVerisi> SehirListesi { get; set; } = new();
    string dosyaYolu = Path.Combine(FileSystem.AppDataDirectory, "havadurumu_kayitlar.json");

    public HavaDurumuSayfasi()
    {
        InitializeComponent();
        SehirlerListesi.ItemsSource = SehirListesi;
        KayitlariYukle();
    }

    // Þehir Ekleme ve Kaydetme 
    private async void OnSehirEkleClicked(object sender, EventArgs e)
    {
        string sehir = SehirGiris.Text?.Trim().ToUpper();
        if (string.IsNullOrEmpty(sehir)) return;

        SehirListesi.Add(new HavaDurumuVerisi { Name = sehir });
        Kaydet();
        SehirGiris.Text = string.Empty;
    }

    // Silme Ýþlemi ve Dosya Güncelleme
    private async void OnSilClicked(object sender, EventArgs e)
    {
        var sehir = (sender as Button).CommandParameter as HavaDurumuVerisi;
        if (sehir != null && await DisplayAlert("Onay", $"{sehir.Name} silinsin mi?", "Evet", "Hayýr"))
        {
            SehirListesi.Remove(sehir);
            Kaydet();
        }
    }

    // Yenileme Ýþlemi (Aþaðý Çekince Çalýþýr)
    private async void OnYenileClicked(object sender, EventArgs e)
    {
        // Resimlerin tekrar yüklenmesini tetikleme
        var yedek = SehirListesi.ToList();
        SehirListesi.Clear();
        await Task.Delay(1000);
        foreach (var s in yedek) SehirListesi.Add(s);

        YenilemeKontrolu.IsRefreshing = false;
    }

    // JSON Kaydetme Fonksiyonu
    private void Kaydet()
    {
        string json = JsonSerializer.Serialize(SehirListesi);
        File.WriteAllText(dosyaYolu, json);
    }

    // Uygulama Açýlýþýnda JSON Okuma
    private void KayitlariYukle()
    {
        if (File.Exists(dosyaYolu))
        {
            string json = File.ReadAllText(dosyaYolu);
            var veriler = JsonSerializer.Deserialize<List<HavaDurumuVerisi>>(json);
            if (veriler != null)
                foreach (var v in veriler) SehirListesi.Add(v);
        }
    }
}