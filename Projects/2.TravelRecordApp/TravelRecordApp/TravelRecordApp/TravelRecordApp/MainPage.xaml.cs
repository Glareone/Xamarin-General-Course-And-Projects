using System;
using System.ComponentModel;
using System.Linq;
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
            

            var isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            var isPasswordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if (isPasswordEmpty || isEmailEmpty)
            {
            }
            else
            {
                var user = (await App.MobileServiceClient.GetTable<Users>().Where(u => u.Email == EmailEntry.Text).ToListAsync())
                    .FirstOrDefault();

                if (user != null && user.Password == PasswordEntry.Text)
                {
                    // store user information in App class.
                    App.user = user;
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Error", "Username or password are incorrect", "Ok");
                }
            }
        }

        private void NavigateToRegisterPageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}