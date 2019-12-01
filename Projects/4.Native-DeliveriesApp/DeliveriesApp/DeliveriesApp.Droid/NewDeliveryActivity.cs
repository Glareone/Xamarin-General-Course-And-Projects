using Android.App;
using Android.OS;
using Android.Widget;
using DeliveriesApp.Model;

namespace DeliveriesApp.Droid
{
    [Activity(Label = "NewDeliveryActivity")]
    public class NewDeliveryActivity : Activity
    {
        private Button _saveButton;
        private EditText _packageEditText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.NewDelivery);

            _saveButton = FindViewById<Button>(Resource.Id.saveButton);
            _packageEditText = FindViewById<EditText>(Resource.Id.packageNameEditText);

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