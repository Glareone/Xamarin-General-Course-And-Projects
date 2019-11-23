using System;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords don't match", "Ok");
                return;
            }

            var user = new Users
            {
                Email = EmailEntry.Text,
                Password = PasswordEntry.Text
            };

            try
            {
                await Users.Register(user);
                await Navigation.PushAsync(new HomePage());
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Oops! Something goes wrong", "Ok");
            }
        }
    }
}