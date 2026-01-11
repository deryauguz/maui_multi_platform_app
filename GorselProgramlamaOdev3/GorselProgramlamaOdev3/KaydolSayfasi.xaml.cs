using GorselProgramlamaOdev3.Services;

namespace GorselProgramlamaOdev3;

public partial class KaydolSayfasi : ContentPage
{
    public KaydolSayfasi()
    {
        InitializeComponent();
    }

    private void GirisSayfasinaDon_ButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private void Iptal_ButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    private async void Kaydol_ButtonClicked(object sender, EventArgs e)
    {
        string message = "";
        // Kaydetme metodu:
        var res = await FirebaseServices.Register(KullaniciAdiEntry.Text, EmailEntry.Text, SifreEntry.Text, ref message);
        if (res == true)
        {
            await DisplayAlert("Kayit baiarili", "Hesabiniz olusturuldu.", "Tamam");
        }
        else
        {
            await DisplayAlert("Kayit basarisiz", message, "Tamam");
        }
    }
}
