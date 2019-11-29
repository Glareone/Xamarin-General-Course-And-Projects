using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace DeliveriesApp.Model
{
    public class AzureHelper
    {
        public static MobileServiceClient MobileServiceClient = new MobileServiceClient("https://xamarindeliveriesappglareone.azurewebsites.net", new HttpClientHandler());
    }
}
