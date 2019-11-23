using System.Linq;
using System.Threading.Tasks;

namespace TravelRecordApp.Model
{
    public class Users
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

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
    }
}