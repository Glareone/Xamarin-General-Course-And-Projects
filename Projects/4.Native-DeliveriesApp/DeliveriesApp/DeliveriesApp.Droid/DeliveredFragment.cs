using System.Linq;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveriesApp.Droid
{
    public class DeliveredFragment : Android.Support.V4.App.ListFragment
    {
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var deliveries = (await Delivery.GetDelivered()).ToList();
            ListAdapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, deliveries);
        }
    }
}