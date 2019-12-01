using System.Linq;
using Android.OS;
using DeliveriesApp.Model;

namespace DeliveriesApp.Droid
{
    public class DeliveriesFragment : Android.Support.V4.App.ListFragment
    {
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var deliveries = (await Delivery.GetDeliveries()).ToList();
            ListAdapter = new DeliveryAdapter(Activity, deliveries);
        }
    }
}