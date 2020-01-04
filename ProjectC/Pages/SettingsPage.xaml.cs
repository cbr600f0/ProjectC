﻿using System;

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
            muteCell.On = ConfigFile.soundIsOn ? false : true;
            keyboardCell.On = ConfigFile.keyboardSoundOn;
            otherSoundsCell.On = ConfigFile.otherSoundsOn;
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

        private void keyboardCell_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell keyboardCell2 = (SwitchCell)sender;
            ConfigFile.keyboardSoundOn = keyboardCell2.On;
        }

        private void otherSoundsCell_OnChanged(object sender, ToggledEventArgs e)
        {
            SwitchCell otherSoundsCell2 = (SwitchCell)sender;
            ConfigFile.otherSoundsOn = otherSoundsCell2.On;
        }
    }
}