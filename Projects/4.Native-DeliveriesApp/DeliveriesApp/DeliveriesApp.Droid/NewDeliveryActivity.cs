using Android.App;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Model;
//using Android.Gsm.Map;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDeliveryActivity")]
    public class NewDeliveryActivity : Activity
    {
        private Button _saveButton;
        private EditText _packageEditText;
        //Android.MapFragment

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewDelivery);

            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _packageEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);

            //var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);

            _saveButton.Click += SaveButton_Click;
        }

        private async void SaveButton_Click(object sender, System.EventArgs e)
        {
            var delivery = new Delivery
            {
                Name = _packageEditText.Text,
                Status = 0,
            };

            await Delivery.InsertDelivery(delivery);
        }
    }
}