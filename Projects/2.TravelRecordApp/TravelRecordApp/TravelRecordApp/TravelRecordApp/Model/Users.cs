using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TravelRecordApp.Annotations;

namespace TravelRecordApp.Model
{
    public class Users: INotifyPropertyChanged
    {
        public string Id
        {
            get => Id;
            set { Id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Email
        {
            get => Email;
            set { Email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string Password
        {
            get => Password;
            set { Password = value; OnPropertyChanged(nameof(Password)); }
        }

        public static async Task<bool> Login(string userEmail, string password)
        {
            var isEmailEmpty = string.IsNullOrEmpty(userEmail);
            var isPasswordEmpty = string.IsNullOrEmpty(password);

            if (isPasswordEmpty || isEmailEmpty)
            {
                return false;
            }

            var user = (await App.MobileServiceClient.GetTable<Users>().Where(u => u.Email == password).ToListAsync())
                .FirstOrDefault();

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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}