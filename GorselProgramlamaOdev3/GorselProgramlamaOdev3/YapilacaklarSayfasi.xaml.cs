using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using GorselProgramlamaOdev3.Model;

namespace GorselProgramlamaOdev3;

public partial class YapilacaklarSayfasi : ContentPage
{
    FirebaseClient client = new FirebaseClient("https://odev3-ac7af-default-rtdb.firebaseio.com/");
    public ObservableCollection<ToDoItem> Gorevler { get; set; } = new();

    // Düzenleme modunu takip etmek için deðiþken
    ToDoItem secilenDuzenlenecekItem = null;

    public YapilacaklarSayfasi()
    {
        InitializeComponent();
        TodoListe.ItemsSource = Gorevler;
        _ = VerileriYukle();
    }

    private async Task VerileriYukle()
    {
        if (Yukleyici.IsRunning) return;
        Yukleyici.IsRunning = true;
        try
        {
            var items = await client.Child("Todo").OnceAsync<ToDoItem>();
            Gorevler.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    var obj = item.Object;
                    obj.Id = item.Key;
                    Gorevler.Add(obj);
                }
            }
        }
        catch { }
        finally { Yukleyici.IsRunning = false; }
    }

    private async void OnEkleClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(BaslikGiris.Text))
        {
            await DisplayAlert("Uyarý", "Lütfen bir baþlýk girin", "Tamam");
            return;
        }

        var gorevData = new ToDoItem
        {
            Baslik = BaslikGiris.Text,
            Detay = DetayGiris.Text,
            TamamlandiMi = secilenDuzenlenecekItem?.TamamlandiMi ?? false,
            HedefTarih = TarihSecici.Date.ToString("dd.MM.yyyy"),
            HedefSaat = SaatSecici.Time.ToString(@"hh\:mm")
        };

        try
        {
            if (secilenDuzenlenecekItem == null)
            {
                // YENÝ EKLEME
                await client.Child("Todo").PostAsync(gorevData);
            }
            else
            {
                // GÜNCELLEME
                await client.Child("Todo").Child(secilenDuzenlenecekItem.Id).PutAsync(gorevData);
                FormuSifirla();
            }

            BaslikGiris.Text = DetayGiris.Text = string.Empty;
            await VerileriYukle();
        }
        catch { await DisplayAlert("Hata", "Ýþlem baþarýsýz.", "Tamam"); }
    }

    private void OnDuzenleClicked(object sender, EventArgs e)
    {
        var item = (sender as Button).CommandParameter as ToDoItem;
        if (item != null)
        {
            secilenDuzenlenecekItem = item;
            BaslikGiris.Text = item.Baslik;
            DetayGiris.Text = item.Detay;
            TarihSecici.Date = DateTime.ParseExact(item.HedefTarih, "dd.MM.yyyy", null);
            SaatSecici.Time = TimeSpan.Parse(item.HedefSaat);

            IslemButonu.Text = "DEÐÝÞÝKLÝKLERÝ KAYDET";
            IslemButonu.BackgroundColor = Colors.Orange;
            IptalButonu.IsVisible = true;
        }
    }

    private void OnIptalClicked(object sender, EventArgs e) => FormuSifirla();

    private void FormuSifirla()
    {
        secilenDuzenlenecekItem = null;
        BaslikGiris.Text = DetayGiris.Text = string.Empty;
        IslemButonu.Text = "GÖREVÝ PLANLA";
        IslemButonu.BackgroundColor = Colors.DarkGreen;
        IptalButonu.IsVisible = false;
    }

    private async void OnSilClicked(object sender, EventArgs e)
    {
        var item = (sender as Button).CommandParameter as ToDoItem;
        if (item != null && await DisplayAlert("Sil", "Bu görev silinsin mi?", "Evet", "Hayýr"))
        {
            await client.Child("Todo").Child(item.Id).DeleteAsync();
            await VerileriYukle();
        }
    }

    private async void OnTamamlandiDegisti(object sender, CheckedChangedEventArgs e)
    {
        var item = (sender as CheckBox).BindingContext as ToDoItem;
        if (item != null && !string.IsNullOrEmpty(item.Id))
        {
            await client.Child("Todo").Child(item.Id).PutAsync(item);
        }
    }
}