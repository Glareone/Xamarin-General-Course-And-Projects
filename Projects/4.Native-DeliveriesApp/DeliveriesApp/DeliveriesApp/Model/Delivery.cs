﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeliveriesApp.Model
{
    public class Delivery
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double OriginLatitude { get; set; }

        public double OriginLongitude { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        // 0 = waiting delivery person
        // 1 = being delivered
        // 2 = delivered
        public int Status { get; set; }

        public static async Task<IEnumerable<Delivery>> GetDeliveries()
        {
            var deliveries = await AzureHelper.MobileServiceClient.GetTable<Delivery>().Where(d => d.Status != 2).ToListAsync();
            return deliveries;
        }

        public static async Task<IEnumerable<Delivery>> GetDelivered()
        {
            var deliveries = await AzureHelper.MobileServiceClient.GetTable<Delivery>().Where(d => d.Status == 2).ToListAsync();
            return deliveries;
        }

        public static async Task<bool> InsertDelivery(Delivery delivery)
        {
            await AzureHelper.MobileServiceClient.GetTable<Delivery>().InsertAsync(delivery);
            return true;
        }
    }
}
