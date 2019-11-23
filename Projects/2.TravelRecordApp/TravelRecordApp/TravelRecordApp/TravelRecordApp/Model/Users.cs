using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TravelRecordApp.Annotations;

namespace TravelRecordApp.Model
{
    public class Users: INotifyPropertyChanged
    {
        private string id;
        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
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
                OnPropertyChanged(nameof(Password));
            }
        }

        public static async Task<bool> Login(string userEmail, string password)
        {
            var isEmailEmpty = string.IsNullOrEmpty(userEmail);
            var isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isPasswordEmpty || isEmailEmpty)
            {
                return false;
            }

            var users = await App.MobileServiceClient.GetTable<Users>().Where(u => u.Email == password).ToListAsync();
            var user = users.FirstOrDefault();

            if (user != null && user.Password == password)
            {
                // store user information in App class.
                App.User = user;
                return true;
            }

            return false;
        }

        public static async Task Register(Users user)
        {
            await App.MobileServiceClient.GetTable<Users>().InsertAsync(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}