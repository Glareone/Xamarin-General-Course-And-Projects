using System.Linq;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    public class DeliveryPerson
    {
        public string Id { get; set; }

        public static async Task<DeliveryPerson> GetDeliveries(string id)
        {
            return (await AzureHelper.MobileServiceClient.GetTable<DeliveryPerson>().Where(p => p.Id == id).ToListAsync()).FirstOrDefault();
        }
    }
}
