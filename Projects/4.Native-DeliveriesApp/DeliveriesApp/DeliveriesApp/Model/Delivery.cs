using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    public class Delivery
    {
        public string Id { get; set; }

        public static async Task<IEnumerable<Delivery>> GetDeliveries(string id)
        {
            return await AzureHelper.MobileServiceClient.GetTable<Delivery>().Where(d => d.Id == id).ToListAsync();
        }
    }
}
