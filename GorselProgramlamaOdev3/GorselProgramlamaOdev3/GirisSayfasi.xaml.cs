using GorselProgramlamaOdev3.Services;

namespace GorselProgramlamaOdev3;

public partial class GirisSayfasi : ContentPage
{
    public GirisSayfasi()
    {
        InitializeComponent();
    }

    private async void Giris_ButtonClicked(object sender, EventArgs e)
    {
        string message = "";
        var res = await FirebaseServices.Login(EmailEntry.Text, SifreEntry.Text, msg => message = msg);

        if (res)
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            await DisplayAlert("Giriþ Baþarýsýz", message, "Tamam");
        }
    }

    private void Kaydol_ButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new KaydolSayfasi());
    }
}
