using System.ComponentModel;
using System.Runtime.CompilerServices;
using TravelRecordApp.Annotations;
using TravelRecordApp.Model;
using TravelRecordApp.ViewModel.Commands;

namespace TravelRecordApp.ViewModel
{
    public class RegisterViewModel: INotifyPropertyChanged
    {
        public Users user { get; set; }
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
                    Email = Email,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
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
                    Email = Email,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
                };
                OnPropertyChanged(nameof(Password));
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                User = new Users
                {
                    Email = Email,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
                };
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new RegisterCommand(this);
        }

        public async void Register(Users user)
        {
            await Users.Register(user);
            await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
