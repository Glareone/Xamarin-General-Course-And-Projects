using System;
using System.ComponentModel;
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

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            var isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            var isPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (isPasswordEmpty || isEmailEmpty)
            {
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }

        private void NavigateToRegisterPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}