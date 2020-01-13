using System;
﻿using ProjectC.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectC.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
       
        public SettingsPage()
        {
            InitializeComponent();
            muteCell.On = ConfigFile.soundIsOn ? false : true;
            keyboardCell.On = ConfigFile.keyboardSoundOn;
            otherSoundsCell.On = ConfigFile.otherSoundsOn;
            slider.Value = ConfigFile.Slider;
            if (BasePage.CurrentUserId.HasValue)
            {
                changePasswordButtonContainer.IsVisible = true;
            }
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            Slider slider1 = (Slider)sender;
            ConfigFile.Slider = slider1.Value;
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
        private void changePasswordButton_Clicked(object sender, EventArgs e)
        {
            ValidateData();
        }

        public async void ValidateData()
        {
            await Navigation.PushAsync(new ChangePasswordPage(false, BasePage.UserService.Get(BasePage.CurrentUserId.Value).UserName));
        }
    }
}