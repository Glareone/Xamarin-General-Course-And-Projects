using System;
using System.ComponentModel;
using TravelRecordApp.Model;
using Xamarin.Forms;

namespace TravelRecordApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            IconImage.Source = ImageSource.FromResource("TravelRecordApp.Assets.Images.plane.png", typeof(MainPage));
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var canLogin = await Users.Login(EmailEntry.Text, PasswordEntry.Text);

            if (canLogin)
            {
                await Navigation.PushAsync(new HomePage());
            }
            else
            {
                await DisplayAlert("Error", "Password or Email is incorrect", "Ok");
            }
        }

        private void NavigateToRegisterPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}