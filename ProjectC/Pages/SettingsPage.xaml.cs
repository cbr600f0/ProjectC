using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        bool IsEnabled = true;
        public SettingsPage()
        {
            InitializeComponent();
            // muteCell.On = ConfigFile.soundIsOn;
            muteCell.On = ConfigFile.soundIsOn ? false : true;
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {

        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private void muteCell_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell muteCell2 = (SwitchCell)sender;
            ConfigFile.soundIsOn = !muteCell2.On;
        }
    }
}