using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelRecordApp.Annotations;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private Users user;
        public Users User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;

                User = new Users
                {
                    Email = this.Email,
                    Password = this.Password
                };

                OnPropertyChanged(nameof(Email));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;

                User = new Users
                {
                    Email = this.Email,
                    Password = this.Password
                };

                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginCommand LoginCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public MainViewModel()
        {
            User = new Users();
            LoginCommand = new LoginCommand(this);
        }

        public async void Login()
        {
            var canLogin = await Users.Login(User.Email, User.Password);

            if (canLogin)
            {
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Password or Email is incorrect", "Ok");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
