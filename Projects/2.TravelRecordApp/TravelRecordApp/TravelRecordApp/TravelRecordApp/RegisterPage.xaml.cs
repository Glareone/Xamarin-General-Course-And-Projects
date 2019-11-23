using System;
using TravelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        Users user;

        public RegisterPage()
        {
            InitializeComponent();

            user = new Users();

            // Bind user to StackLayoutContext. After that you could use Two way binding.
            // And all your props could use {Binding *propName*} declaration.
            ContainerStackLayout.BindingContext = user;
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != ConfirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords don't match", "Ok");
                return;
            }

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