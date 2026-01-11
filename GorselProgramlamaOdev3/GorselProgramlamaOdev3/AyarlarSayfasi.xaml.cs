using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace GorselProgramlamaOdev3
{
    public partial class AyarlarSayfasi : ContentPage
    {
        public AyarlarSayfasi()
        {
            InitializeComponent();

            // Baþlangýçta mevcut temaya göre switch durumunu ayarla
            TemaSwitch.IsToggled = Application.Current.UserAppTheme == AppTheme.Dark;
            UpdateTemaLabel(TemaSwitch.IsToggled);
        }

        private void TemaSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                // Koyu tema seçildi
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                // Açýk tema seçildi
                Application.Current.UserAppTheme = AppTheme.Light;
            }

            UpdateTemaLabel(e.Value);
        }

        private void UpdateTemaLabel(bool isDark)
        {
            TemaLabel.Text = isDark ? "Koyu Tema" : "Açýk Tema";
        }
    }
}


