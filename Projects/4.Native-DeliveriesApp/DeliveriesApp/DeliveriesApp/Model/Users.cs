using System.Linq;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    public class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static async Task<bool> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var user = (await AzureHelper.MobileServiceClient.GetTable<Users>().Where(u => u.Email == email && u.Password == password).ToListAsync()).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> Register(string email, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) ||
                password != confirmPassword)
            {
                return false;
            }

            var user = new Users
            {
                Email = email,
                Password = password
            };

            await AzureHelper.MobileServiceClient.GetTable<Users>().InsertAsync(user);
            return true;
        }
    }
}
