namespace GorselProgramlamaOdev3;

public partial class HaberDetaySayfasi : ContentPage
{
    string _url;
    public HaberDetaySayfasi(string link, string baslik)
    {
        InitializeComponent();
        _url = link;
        Title = baslik;
        HaberWeb.Source = _url;
    }

    private async void Paylas_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest { Uri = _url, Title = Title });
    }
}